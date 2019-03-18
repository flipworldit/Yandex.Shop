using Newtonsoft.Json;

namespace org.BTR.YandexShop
{
    public class YShopRefundedAmount
    {
        public string value { get; set; }
        public string currency { get; set; }

        [JsonIgnore]
        public decimal? Value
        {
            get
            {
                decimal tmpDecimal;

                if (decimal.TryParse(value, out tmpDecimal))
                {
                    return tmpDecimal;
                }

                return null;
            }
        }
    }
}