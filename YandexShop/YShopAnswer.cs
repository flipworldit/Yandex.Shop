namespace org.BTR.YandexShop
{
    public class YShopAnswer
    {
        public bool Success { get; internal set; }
        public YShopPayment Payment { get; internal set; }
        public YShopError Error{ get; internal set; }
    }
}
