using AutoMapper;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using PayPal.Core;
using PayPal.v1.Payments;
using System.Net;
using vniu_api.Configuration;
using vniu_api.Constants;
using vniu_api.Models.EF.Payments;
using vniu_api.Repositories;
using vniu_api.Repositories.Utils;
using vniu_api.ViewModels.PaymentsViewModels;
using vniu_api.ViewModels.RequestsViewModels;

namespace vniu_api.Services.Utils
{
    public class PayPalService : IPayPalService
    {
        private readonly PayPalConfiguration _payPalConfiguration;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public PayPalService(IOptions<PayPalConfiguration> options, IMapper mapper, DataContext context)
        {
            _payPalConfiguration = options.Value;
            _mapper = mapper;
            _context = context;
        }


        public async Task<string> CreatePaymentUrl(PaymentRequest paymentRequest, HttpContext httpContext)
        {
            var envSandbox =
                new SandboxEnvironment(_payPalConfiguration.ClientId, _payPalConfiguration.SecretKey);
            var client = new PayPalHttpClient(envSandbox);
            var paypalOrderId = DateTime.Now.Ticks;
            var urlCallBack = _payPalConfiguration.ReturnUrl;
            var payment = new PayPal.v1.Payments.Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = paymentRequest.OrderTotal.ToString(),
                            Currency = "USD",
                            Details = new AmountDetails
                            {
                                Tax = "0",
                                Shipping = "0",
                                Subtotal = paymentRequest.OrderTotal.ToString(),
                            }
                        },
                        ItemList = new ItemList()
                        {
                            Items = new List<Item>()
                            {
                                new Item()
                                {
                                    Name = " | Order: " + paymentRequest.OrderDescription,
                                    Currency = "USD",
                                    Price = paymentRequest.OrderTotal.ToString(),
                                    Quantity = 1.ToString(),
                                    Sku = "sku",
                                    Tax = "0",
                                    //Url = "https://www.code-mega.com" // Url detail of Item
                                }
                            }
                        },
                        Description = $"Invoice #{paymentRequest.OrderDescription}",
                        InvoiceNumber = paypalOrderId.ToString()
                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    ReturnUrl =
                        $"{urlCallBack}?payment_method=PayPal&success=1&order_id={paypalOrderId}",
                    CancelUrl =
                        $"{urlCallBack}?payment_method=PayPal&success=0&order_id={paypalOrderId}"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };

            var request = new PaymentCreateRequest();
            request.RequestBody(payment);

            var paymentUrl = "";
            var response = await client.Execute(request);
            var statusCode = response.StatusCode;

            if (statusCode is not (HttpStatusCode.Accepted or HttpStatusCode.OK or HttpStatusCode.Created))
                return paymentUrl;

            var result = response.Result<Payment>();
            using var links = result.Links.GetEnumerator();

            while (links.MoveNext())
            {
                var lnk = links.Current;
                if (lnk == null) continue;
                if (!lnk.Rel.ToLower().Trim().Equals("approval_url")) continue;
                paymentUrl = lnk.Href;
            }

            return paymentUrl;
        }

        public async Task<PaymentMethodVM> PaymentExecuteAsync(IQueryCollection collections)
        {
            var dataResponse = new PaymentMethodVM();

            foreach (var (key, value) in collections)
            {
                if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("order_description"))
                {
                    dataResponse.PaymentDescription = value;
                }

                if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("paymentid"))
                {
                    dataResponse.PaymentTransactionNo = value;
                }

                if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("success"))
                {
                    dataResponse.PaymentStatus = int.Parse(value);
                }
            }

            dataResponse.PaymentProvider = "PayPal";
            dataResponse.PaymentTypeId = AppPaymentType.PAYPAL;
            dataResponse.PaymentDate = DateTime.Now;

            var paymentMethod = _mapper.Map<PaymentMethod>(dataResponse);

            _context.PaymentMethods.Add(paymentMethod);

            await _context.SaveChangesAsync();

            var paymentMethodVM = _mapper.Map<PaymentMethodVM>(paymentMethod);

            return paymentMethodVM;
        }
    }
}
