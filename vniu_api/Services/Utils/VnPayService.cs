using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using vniu_api.Configuration;
using vniu_api.Exceptions;
using vniu_api.Helpers;
using vniu_api.Models.EF.Payments;
using vniu_api.Repositories;
using vniu_api.Repositories.Utils;
using vniu_api.ViewModels.PaymentsViewModels;
using vniu_api.ViewModels.RequestsViewModels;

namespace vniu_api.Services.Utils
{
    public class VnPayService : IVnPayService
    {
        private readonly VnPayConfiguration _vnPayConfiguration;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public VnPayService(IOptions<VnPayConfiguration> options, IMapper mapper, DataContext context)
        {
            _vnPayConfiguration = options.Value;
            _mapper = mapper;
            _context = context;
        }

        public async Task<string> CreatePaymentUrl(PaymentRequest paymentRequest, HttpContext httpContext)
        {
            const string FASHION_ORDER_CODE = "200000";
            paymentRequest.OrderType = FASHION_ORDER_CODE;

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == paymentRequest.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_vnPayConfiguration.TimeZoneId);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var vnPayLibrary = new VnPayLibrary();
            var urlCallBack = _vnPayConfiguration.ReturnUrl;

            vnPayLibrary.AddRequestData("vnp_Version", _vnPayConfiguration.Version);
            vnPayLibrary.AddRequestData("vnp_Command", _vnPayConfiguration.Command);
            vnPayLibrary.AddRequestData("vnp_TmnCode", _vnPayConfiguration.TmnCode);
            vnPayLibrary.AddRequestData("vnp_Amount", ((int)paymentRequest.OrderTotal * 100).ToString());
            vnPayLibrary.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            vnPayLibrary.AddRequestData("vnp_CurrCode", _vnPayConfiguration.CurrCode);
            vnPayLibrary.AddRequestData("vnp_IpAddr", vnPayLibrary.GetIpAddress(httpContext));
            vnPayLibrary.AddRequestData("vnp_Locale", _vnPayConfiguration.Locale);
            vnPayLibrary.AddRequestData("vnp_OrderInfo", $"{user.UserName} {paymentRequest.OrderDescription} {paymentRequest.OrderTotal}");
            vnPayLibrary.AddRequestData("vnp_OrderType", paymentRequest.OrderType);
            vnPayLibrary.AddRequestData("vnp_ReturnUrl", urlCallBack);
            vnPayLibrary.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                vnPayLibrary.CreateRequestUrl(_vnPayConfiguration.BaseUrl, _vnPayConfiguration.HashSecret);

            return paymentUrl;
        }
        
        public async Task<PaymentMethodVM> PaymentExecuteAsync(IQueryCollection collections)
        {
            var vnPayLibrary = new VnPayLibrary();

            var dataResponse = vnPayLibrary.GetFullResponseData(collections, _vnPayConfiguration.HashSecret);

            var paymentMethod = _mapper.Map<PaymentMethod>(dataResponse);

            _context.PaymentMethods.Add(paymentMethod);

            await _context.SaveChangesAsync();

            var paymentMethodVM = _mapper.Map<PaymentMethodVM>(paymentMethod);

            return paymentMethodVM;
        }
    }
}
