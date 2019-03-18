using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using org.BTR.YandexShop.InnerClases;

namespace org.BTR.YandexShop
{
    internal interface IYRequestSender
    {
        string YShopId { get; set; }

        string YShopSecretKey { get; set; }

        ShopPayment Execute(string url, string dataString);
    }

    internal class YRequestSender : IYRequestSender
    {
        string _userAgent = "Yandex.Checkout.V3 .NET Client";

        public string YShopId { get; set; }

        public string YShopSecretKey { get; set; }

        public ShopPayment Execute(string url, string dataString)
        {
            var request = CreateRequest(url, dataString);

            var result = GetResponse(request);

            return result;
        }
        
        internal HttpWebRequest CreateRequest(string url, string dataString)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var request = (HttpWebRequest)
                WebRequest.Create(url);

            

            if (!string.IsNullOrWhiteSpace(dataString))
            {
                request.Method = "POST";

                request.ContentType = "application/json";
            }

            var _authorization = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(YShopId + ":" + YShopSecretKey));

            request.Headers.Add("Authorization", _authorization);
            
            request.Headers.Add("Idempotence-Key", Guid.NewGuid().ToString().ToLower());

            if (!string.IsNullOrWhiteSpace(dataString))
            {
                var postBytes = Encoding.UTF8.GetBytes(dataString);

                request.ContentLength = postBytes.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(postBytes, 0, postBytes.Length);
                }
            }

            return request;
        }

        internal virtual ShopPayment GetResponse(HttpWebRequest request)
        {
            using (var response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream == null)
                    {
                        throw new InvalidOperationException("Response stream is null.");
                    }

                    using (var streamReader = new StreamReader(responseStream))
                    {
                        var json = streamReader.ReadToEnd();

                        var paymentResponce = YPaymentParcer.Deserialize(json);

                        return paymentResponce;
                    }
                }
            }
        }
    }
}