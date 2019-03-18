using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using org.BTR.YandexShop.InnerClases;

[assembly: InternalsVisibleTo("YandexShopTests")]

namespace org.BTR.YandexShop
{
    public interface IYShopManager
    {
        YShopAnswer CreatePayment(YShopRequestBuilder builder);
        Task<YShopAnswer> CreatePaymentAsync(YShopRequestBuilder builder);

        YShopAnswer CapturePayment(YShopPayment payment, decimal? captureSum = null);
        Task<YShopAnswer> CapturePaymentAsync(YShopPayment payment, decimal? captureSum = null);

        YShopAnswer CancelPayment(YShopPayment payment);
        Task<YShopAnswer> CancelPaymentAsync(YShopPayment payment);

        YShopAnswer RequestPayment(Guid paymentId);
        Task<YShopAnswer> RequestPaymentAsync(Guid paymentId);

        YShopAnswer RefundPayment(Guid paymentId, decimal? refundSum = null);
        Task<YShopAnswer> RefundPaymentAsync(Guid paymentId, decimal? refundSum = null);
    }

    public class YShopManager : IYShopManager
    {
        private const string RequestUrl = "https://payment.yandex.net/api/v3/payments";
        private const string RefundUrl = "https://payment.yandex.net/api/v3/refunds";
        private const string ConfirmUrlPart = "/{0}/capture";
        private const string CancelUrlPart = "/{0}/cancel";

        internal readonly IYRequestSender RequestSender;

        public YShopManager(string yShopId,
            string yShopSecretKey,
            bool isYShopTestMode)
            : this(yShopId, yShopSecretKey, isYShopTestMode, new YRequestSender())
        { }

        internal YShopManager(string yShopId,
            string yShopSecretKey,
            bool isYShopTestMode,
            IYRequestSender requestSender)
        {
            RequestSender = requestSender;

            requestSender.YShopId = yShopId;

            requestSender.YShopSecretKey = yShopSecretKey;

            RequestSender = requestSender;

            if (RequestSender == null)
            {
                throw new Exception("Пустой RequestSender, проверьте в секции appSettings Web.config'a настройки Яндекс магазина (значения ShopId и SecretKey)");
            }

            var isStartTest = RequestSender.YShopSecretKey.StartsWith("test_");

            if (isYShopTestMode == isStartTest)
            {
                return;
            }

            const string message = "Секретный ключ не соответствует режиму работы Яндекс кассы";

            throw new Exception(message);
        }

        public Task<YShopAnswer> CreatePaymentAsync(YShopRequestBuilder builder)
        {
            return Task.Run(() => CreatePayment(builder));
        }

        public YShopAnswer CreatePayment(YShopRequestBuilder builder)
        {
            var dataString = builder.Build();

            var result = Execute(RequestUrl, dataString);

            return result;
        }

        public Task<YShopAnswer> CapturePaymentAsync(YShopPayment payment, decimal? captureSum = null)
        {
            return Task.Run(() => CapturePayment(payment, captureSum));
        }

        public YShopAnswer CapturePayment(YShopPayment payment, decimal? captureSum = null)
        {
            if (payment == null || payment.Status != YPaymentStatus.waiting_for_capture)
            {
                return new YShopAnswer
                {
                    Success = true,
                    Error = null,
                    Payment = payment
                };
            }

            var url = string.Format((RequestUrl + ConfirmUrlPart), payment.Id.ToString().ToLower());

            var data = GeneratePartialCaptureDataString(payment, captureSum);

            var result = Execute(url, data);

            return result;
        }

        public Task<YShopAnswer> CancelPaymentAsync(YShopPayment payment)
        {
            return Task.Run(() => CancelPayment(payment));
        }

        public YShopAnswer CancelPayment(YShopPayment payment)
        {
            if (payment == null || payment.Status != YPaymentStatus.waiting_for_capture)
            {
                return new YShopAnswer
                {
                    Success = true,
                    Error = null,
                    Payment = payment
                };
            }

            var url = string.Format((RequestUrl + CancelUrlPart), payment.Id.ToString().ToLower());

            var result = Execute(url, null);

            return result;

        }

        public YShopAnswer RequestPayment(Guid paymentId)
        {
            var url = string.Format((RequestUrl + "/{0}"), paymentId);

            var result = Execute(url, null);

            return result;
        }

        public Task<YShopAnswer> RequestPaymentAsync(Guid paymentId)
        {
            return Task.Run(() => RequestPayment(paymentId));
        }

        public YShopAnswer RefundPayment(Guid paymentId, decimal? refundSum = null)
        {
            var result = RequestPayment(paymentId);

            var payment = result.Payment;

            if (!result.Success)
            {
                return result;
            }

            var data = GenerateRefundDataString(result.Payment, refundSum);

            result = Execute(RefundUrl, data);

            if (!result.Success)
            {
                return result;
            }

            payment.RefundedAmount = result.Payment.RefundedAmount;

            result.Payment = payment;

            return result;
        }

        public Task<YShopAnswer> RefundPaymentAsync(Guid paymentId, decimal? refundSum = null)
        {
            return Task.Run(() => RefundPayment(paymentId, refundSum));
        }

        private YShopAnswer Execute(string url, string data)
        {
            Exception exception;
            ShopPayment payment;
            try
            {
                payment = RequestSender.Execute(url, data);

                exception = null;
            }
            catch (Exception ex)
            {
                payment = null;

                exception = ex;
            }

            var result = CreateAnswer(payment, exception);

            return result;
        }

        private static string GeneratePartialCaptureDataString(YShopPayment payment, decimal? captureSumValue)
        {
            var result = new ShopPayment
            {
                amount = new Amount
                {
                    value = decimal.Round(captureSumValue ?? payment.YAmount.Value, 2)
                        .ToString(CultureInfo.InvariantCulture),
                    currency = payment.YAmount.Currency
                }
            };

            return result.ToString();
        }

        private static string GenerateRefundDataString(YShopPayment payment, decimal? refundSum)
        {
            var result = new ShopPayment
            {
                amount = new Amount
                {
                    value = decimal.Round(refundSum??payment.YAmount.Value, 2)
                        .ToString(CultureInfo.InvariantCulture),
                    currency = payment.YAmount.Currency
                },
                payment_id = payment.Id.ToString()
            };

            return result.ToString();
        }

        private static YShopAnswer CreateAnswer(ShopPayment payment, Exception exception = null)
        {
            var success = exception == null && payment != null && payment.type == null;

            var result = new YShopAnswer
            {
                Success = success,
                Payment = success ? new YShopPayment(payment) : null,
                Error = success
                    ? null
                    : new YShopError
                    {
                        Code = payment == null ? "execute_error" : payment.code,
                        Description = payment == null 
                            ? exception == null ? "" : exception.Message 
                            : payment.description
                    }
            };

            return result;
        }

    }
}