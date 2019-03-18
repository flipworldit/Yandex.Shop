using org.BTR.YandexShop.InnerClases;

namespace org.BTR.YandexShop
{
    public class YShopCard
    {
        internal ShopCard ShopCard { get; private set; }

        public string First6
        {
            get { return ShopCard.first6; }
        }

        public string Last4
        {
            get { return ShopCard.last4; }
        }

        public string ExpiryMonth
        {
            get { return ShopCard.expiry_month; }
        }

        public string ExpiryYear
        {
            get { return ShopCard.expiry_year; }
        }

        public string CardType
        {
            get { return ShopCard.card_type; }
        }

        internal YShopCard(ShopCard card)
        {
            ShopCard = card;
        }
    }
}
