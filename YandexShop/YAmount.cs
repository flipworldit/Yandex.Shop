using System.Globalization;
using org.BTR.YandexShop.InnerClases;

namespace org.BTR.YandexShop
{
    public class YAmount
    {
        internal Amount Amount { get; set; }

        public decimal Value
        {
            get { return decimal.Round(decimal.Parse(Amount.value, CultureInfo.InvariantCulture), 2); }
            set { Amount.value = decimal.Round(value, 2).ToString(CultureInfo.InvariantCulture); }

        }

        public string Currency
        {
            get { return Amount.currency; }
            set { Amount.currency = value; }
        }

        public YAmount()
            : this(new Amount())
        { }

        internal YAmount(Amount amount)
        {
            Amount = amount;
        }
    }
}
