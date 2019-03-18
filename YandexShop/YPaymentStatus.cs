namespace org.BTR.YandexShop
{
    /// <summary>
    /// Статусы платежа
    /// </summary>
    public enum YPaymentStatus
    {
        /// <summary>
        /// платеж создан, но не завершен
        /// </summary>
        pending = 1,
        /// <summary>
        /// waiting_for_capture
        /// </summary>
        waiting_for_capture = 2,
        /// <summary>
        /// платеж успешно завершен
        /// </summary>
        succeeded = 3,
        /// <summary>
        /// платеж отменен.
        /// </summary>
        canceled = 0 
    }
}