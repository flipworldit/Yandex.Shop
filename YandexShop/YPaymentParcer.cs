using Newtonsoft.Json;
using org.BTR.YandexShop.InnerClases;

namespace org.BTR.YandexShop
{
    public static class YPaymentParcer
    {
        public static YShopPayment DeserializePayment(string json)
        {
            var shopEvent = DeserializeEvent(json);

            return new YShopPayment(shopEvent.Object);
        }

        internal static ShopEvent DeserializeEvent(string json)
        {
            var result = JsonConvert.DeserializeObject<ShopEvent>(json);

            return result;
        }

        internal static ShopPayment Deserialize(string json)
        {
            var result =JsonConvert.DeserializeObject<ShopPayment>(json);

            return result;
        }

        internal static string Serialize(ShopPayment payment, bool withFormat = false)
        {
            return JsonConvert.SerializeObject(payment
                , withFormat? Formatting.Indented : Formatting.None
                , new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}