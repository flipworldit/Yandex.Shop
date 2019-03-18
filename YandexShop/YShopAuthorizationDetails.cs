using org.BTR.YandexShop.InnerClases;

namespace org.BTR.YandexShop
{
    public class YShopAuthorizationDetails
    {
        internal ShopAuthorizationDetails ShopAuthorizationDetails { get; private set; }

        public string Rrn
        {
            get { return ShopAuthorizationDetails.rrn; }
        }

        public string AuthCode
        {
            get { return ShopAuthorizationDetails.auth_code; }
        }

        internal YShopAuthorizationDetails(ShopAuthorizationDetails sad)
        {
            ShopAuthorizationDetails = sad;
        }
    }
}
