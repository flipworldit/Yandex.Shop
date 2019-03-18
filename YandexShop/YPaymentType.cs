namespace org.BTR.YandexShop
{
    public enum YPaymentType
    {
        /// <summary>
        /// Банковская карта
        /// </summary>
        bank_card = 1,
        /// <summary>
        /// Яндекс.Деньги
        /// </summary>
        yandex_money = 2,
        /// <summary>
        /// Сбербанк Онлайн (интернет-банк Сбербанка)
        /// </summary>
        sberbank = 3,
        /// <summary>
        /// Альфа-Клик (интернет-банк Альфа-Банка)
        /// </summary>
        alfabank = 4,
        /// <summary>
        /// Криптограмма Apple Pay
        /// </summary>
        apple_pay = 5,
        /// <summary>
        /// Криптограмма Google Pay
        /// </summary>
        google_pay = 6,
        /// <summary>
        /// QIWI Кошелек
        /// </summary>
        qiwi = 7,
        /// <summary>
        /// Webmoney
        /// </summary>
        webmoney = 8,
        /// <summary>
        /// Баланс мобильного телефона
        /// </summary>
        mobile_balance = 9,
        /// <summary>
        /// Оплата наличными в терминале
        /// </summary>
        cash = 10,
        /// <summary>
        /// Оплата через сервис «Заплатить по частям»
        /// </summary>
        installments = 11
    }
}