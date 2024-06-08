using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using vniu_api.Constants;
using vniu_api.Exceptions;
using vniu_api.Models.EF.Payments;
using vniu_api.Repositories;
using vniu_api.ViewModels.PaymentsViewModels;

namespace vniu_api.Helpers
{
    public class VnPayLibrary
    {
        private readonly SortedList<string, string> _requestData = new SortedList<string, string>(new VnPayCompare());
        private readonly SortedList<string, string> _responseData = new SortedList<string, string>(new VnPayCompare());

        public PaymentMethodVM GetFullResponseData(PaymentMethodVM paymentMethodVM, IQueryCollection collection, string hashSecret)
        {
            var vnPayLibrary = new VnPayLibrary();

            foreach (var (key, value) in collection)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnPayLibrary.AddResponseData(key, value);
                }
            }

            var vnPayTranId = Convert.ToInt32(vnPayLibrary.GetResponseData("vnp_TransactionNo"));

            var vnPayCartType = vnPayLibrary.GetResponseData("vnp_CardType");
            var vnPayBankCode = vnPayLibrary.GetResponseData("vnp_BankCode");

            var vnPayDate = vnPayLibrary.GetResponseData("vnp_PayDate");
            string formatDate = "yyyyMMddHHmmss";
            DateTime createdDate = DateTime.ParseExact(vnPayDate, formatDate, CultureInfo.InvariantCulture);

            var vnpResponseCode = vnPayLibrary.GetResponseData("vnp_ResponseCode");
            var vnpTransactionStatus = vnPayLibrary.GetResponseData("vnp_TransactionStatus");

            var vnpOrderInfo = vnPayLibrary.GetResponseData("vnp_OrderInfo");

            // hash of data response
            var vnpSecureHash =
                collection.FirstOrDefault(k => k.Key == "vnp_SecureHash").Value;

            // check signature
            var checkSignature = vnPayLibrary.ValidateSignature(vnpSecureHash, hashSecret);

            if (!checkSignature)
                throw new PaymentException("Invalid Signature");

            // initial payment status = 0 (unpaid)
            int paymentStatus = 0;

            // intial payment type = 2 (Bank transfer)

            // Get order

            // Check order status

            // order status != "0" => Order already confirm

            // order status == "0" when it runs until this line

            // not(vnp_ResponseCode == "00" && vnp_TransactionStatus == "00") => Update Payment Status == "2" (error transaction)

            // It runs until this line => success => Update Payment Status == "1" (success transaction)

            paymentMethodVM.PaymentTransactionNo = vnPayTranId.ToString();
            paymentMethodVM.PaymentProvider = vnPayBankCode;
            paymentMethodVM.PaymentCartType = vnPayCartType;
            paymentMethodVM.PaymentDate = createdDate;
            paymentMethodVM.PaymentStatus = paymentStatus;
            paymentMethodVM.PaymentDescription = vnpOrderInfo;
            //var paymentMethodVM = new PaymentMethodVM()
            //{
            //    PaymentTransactionNo = vnPayTranId.ToString(),
            //    PaymentProvider = vnPayBankCode,
            //    PaymentCartType = vnPayCartType,
            //    PaymentDate = createdDate,
            //    PaymentStatus = paymentStatus,
            //    PaymentTypeId = AppPaymentType.BANK_TRANSFER,
            //    PaymentDescription = vnpOrderInfo,
            //};

            return paymentMethodVM;
        }
        public string GetIpAddress(HttpContext context)
        {
            var ipAddress = string.Empty;
            try
            {
                var remoteIpAddress = context.Connection.RemoteIpAddress;

                if (remoteIpAddress != null)
                {
                    if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList
                            .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
                    }

                    if (remoteIpAddress != null) ipAddress = remoteIpAddress.ToString();

                    return ipAddress;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "127.0.0.1";
        }
        public void AddRequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _requestData.Add(key, value);
            }
        }

        public void AddResponseData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _responseData.Add(key, value);
            }
        }

        public string GetResponseData(string key)
        {
            return _responseData.TryGetValue(key, out var retValue) ? retValue : string.Empty;
        }

        public string CreateRequestUrl(string baseUrl, string vnpHashSecret)
        {
            var data = new StringBuilder();

            foreach (var (key, value) in _requestData.Where(kv => !string.IsNullOrEmpty(kv.Value)))
            {
                data.Append(WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(value) + "&");
            }

            var querystring = data.ToString();

            baseUrl += "?" + querystring;
            var signData = querystring;
            if (signData.Length > 0)
            {
                signData = signData.Remove(data.Length - 1, 1);
            }

            var vnpSecureHash = HmacSha512(vnpHashSecret, signData);
            baseUrl += "vnp_SecureHash=" + vnpSecureHash;

            return baseUrl;
        }

        public bool ValidateSignature(string inputHash, string secretKey)
        {
            var rspRaw = GetResponseData();
            var myChecksum = HmacSha512(secretKey, rspRaw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }

        private string HmacSha512(string key, string inputData)
        {
            var hash = new StringBuilder();
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                var hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }

        private string GetResponseData()
        {
            var data = new StringBuilder();
            if (_responseData.ContainsKey("vnp_SecureHashType"))
            {
                _responseData.Remove("vnp_SecureHashType");
            }

            if (_responseData.ContainsKey("vnp_SecureHash"))
            {
                _responseData.Remove("vnp_SecureHash");
            }

            foreach (var (key, value) in _responseData.Where(kv => !string.IsNullOrEmpty(kv.Value)))
            {
                data.Append(WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(value) + "&");
            }

            //remove last '&'
            if (data.Length > 0)
            {
                data.Remove(data.Length - 1, 1);
            }

            return data.ToString();
        }
    }

    public class VnPayCompare : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            var vnpCompare = CompareInfo.GetCompareInfo("en-US");
            return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
        }
    }
}
