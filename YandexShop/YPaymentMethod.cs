using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.BTR.YandexShop.InnerClases;

namespace org.BTR.YandexShop
{
    public class YPaymentMethod
    {
        internal PaymentMethod PaymentMethod { get; private set; }

        private YShopCard card;

        public Guid Id
        {
            get { return new Guid(PaymentMethod.id); }
        }

        public bool? Saved
        {
            get { return PaymentMethod.saved; }
        }

        public string Title
        {
            get { return PaymentMethod.title; }
        }

        public YShopCard YShopCard
        {
            get { return card; }
        }

        public YPaymentType Type
        {
            get
            {
                if (Enum.IsDefined(typeof(YPaymentType), PaymentMethod.type))
                {
                    return (YPaymentType)Enum.Parse(typeof(YPaymentType), PaymentMethod.type);
                }

                throw new ArgumentOutOfRangeException();
            }
        }

        internal YPaymentMethod(PaymentMethod pMethod)
        {
            PaymentMethod = pMethod;

            card = pMethod.card == null ? null : new YShopCard(pMethod.card);
        }
    }
}
