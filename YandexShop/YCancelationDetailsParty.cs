namespace org.BTR.YandexShop
{
    /// <summary>
    /// Инициатор отмены платежа
    /// </summary>
    public enum YCancelationDetailsParty
    {
        /// <summary>
        /// Продавец товаров и услуг
        /// </summary>
        merchant = 1,
        /// <summary>
        /// Яндекс.Касса
        /// </summary>
        yandex_checkout = 2,
        /// <summary>
        /// Внешние участники платежного процесса
        /// </summary>
        payment_network = 3
    }
}