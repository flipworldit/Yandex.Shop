namespace org.BTR.YandexShop
{
    /// <summary>
    /// Причины отмены платежа
    /// </summary>
    public enum YCancelationDetailsReason
    {
        /// <summary>
        /// Не пройдена аутентификация по 3-D Secure
        /// </summary>
        d3_secure_failed = 0,
        /// <summary>
        /// Оплата данным платежным средством отклонена по неизвестным причинам
        /// </summary>
        call_issuer = 1,
        /// <summary>
        /// Истек срок действия банковской карты
        /// </summary>
        card_expired = 2,
        /// <summary>
        /// Нельзя заплатить банковской картой, выпущенной в этой стране
        /// </summary>
        country_forbidden = 3,
        /// <summary>
        /// Платеж заблокирован из-за подозрения в мошенничестве
        /// </summary>
        fraud_suspected = 4,
        /// <summary>
        /// Причина не детализирована
        /// </summary>
        general_decline = 5,
        /// <summary>
        /// Превышены ограничения на платежи для кошелька в Яндекс.Деньгах
        /// </summary>
        identification_required = 6,
        /// <summary>
        /// Не хватает денег для оплаты
        /// </summary>
        insufficient_funds = 7,
        /// <summary>
        /// Неправильно указан номер карты
        /// </summary>
        invalid_card_number = 8,
        /// <summary>
        /// Неправильно указан код CVV2 (CVC2, CID)
        /// </summary>
        invalid_csc = 9,
        /// <summary>
        /// Организация, выпустившая платежное средство, недоступна
        /// </summary>
        issuer_unavailable = 10,
        /// <summary>
        /// Исчерпан лимит платежей для данного платежного средства или вашего магазина
        /// </summary>
        payment_method_limit_exceeded = 11,
        /// <summary>
        /// Запрещены операции данным платежным средством
        /// </summary>
        payment_method_restricted = 12
    }
}