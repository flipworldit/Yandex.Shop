using System.Collections.Generic;

namespace org.BTR.YandexShop.InnerClases
{
    internal class ShopPayment
    {
        public string id { get; set; }
        public string status { get; set; }
        public bool? paid { get; set; }
        public Amount amount { get; set; }
        public ShopAuthorizationDetails authorization_details;
        public Confirmation confirmation;
        public PaymentMethodData payment_method_data;
        public PaymentMethod payment_method;
        public string captured_at { get; set; }
        public string created_at { get; set; }
        public string description { get; set; }
        public bool? test { get; set; }
        public Dictionary<string, string> metadata { get; set; }
        public CancelationDetails cancellation_details;
        public bool? capture;

        public Amount refunded_amount { get; set; }

        public string payment_id { get; set; }

        public string type { get; set; }
        public string code { get; set; }
        public string parameter { get; set; }

        public override string ToString()
        {
            return ToString(false);
        }

        public string ToString(bool withFormat)
        {
            return YPaymentParcer.Serialize(this, withFormat);
        }
    }
}