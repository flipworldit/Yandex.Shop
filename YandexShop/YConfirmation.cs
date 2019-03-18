using System;
using org.BTR.YandexShop.InnerClases;

namespace org.BTR.YandexShop
{
    public class YConfirmation
    {
        internal Confirmation Confirmation { get; private set; }

        public Uri ConfirmationUrl
        {
            get { return new Uri(Confirmation.confirmation_url); }
        }

        internal YConfirmation(Confirmation confirm)
        {
            Confirmation = confirm;
        }
    }
}
