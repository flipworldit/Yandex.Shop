using System;
using Newtonsoft.Json;

namespace org.BTR.YandexShop.InnerClases
{
    internal class PaymentMethod : PaymentMethodData
    {
        public string id { get; set; }
        public bool? saved { get; set; }
        public ShopCard card { get; set; }
        public string title { get; set; }
    }
}