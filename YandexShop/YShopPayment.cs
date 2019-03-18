using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.BTR.YandexShop.InnerClases;

namespace org.BTR.YandexShop
{
    public class YShopPayment
    {
        internal ShopPayment ShopPayment { get; private set; }

        private YAmount amount;
        private YAmount refundedAmount;
        private YShopAuthorizationDetails authorizationDetails;
        private YConfirmation confirmation;
        private YPaymentMethod paymentMethod;
        private YCancelationDetails cancelationDetails;

        public Guid Id
        {
            get { return new Guid(ShopPayment.id); }
        }

        public YPaymentStatus Status
        {
            get
            {
                if (Enum.IsDefined(typeof(YPaymentStatus), ShopPayment.status))
                {
                    return (YPaymentStatus)Enum.Parse(typeof(YPaymentStatus), ShopPayment.status);
                }

                throw new ArgumentOutOfRangeException();
            }
        }

        public bool? Paid
        {
            get { return ShopPayment.paid; }
        }

        public DateTime? CreatedAt
        {
            get
            {
                DateTime result;

                if (DateTime.TryParse(ShopPayment.created_at, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }

                return null;
            }
        }

        public DateTime? CapturedAt
        {
            get {
                DateTime result;

                if (DateTime.TryParse(ShopPayment.captured_at, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }

                return null;
            }
        }

        public string Description
        {
            get { return ShopPayment.description; }
        }

        public bool? Test
        {
            get { return ShopPayment.test; }
        }

        public Dictionary<string, string> Metadata
        {
            get { return ShopPayment.metadata; }
        }

        public YAmount YAmount
        {
            get { return amount; }
        }

        public YAmount RefundedAmount
        {
            get { return refundedAmount; }
            internal set
            {
                if (refundedAmount == null)
                {
                    refundedAmount = value;
                }
                else
                {
                    refundedAmount.Value += value.Value;
                }

                ShopPayment.refunded_amount = new Amount
                {
                    value = decimal.Round(refundedAmount.Value, 2).ToString(CultureInfo.InvariantCulture),
                    currency = refundedAmount.Currency
                };
            }
        }

        public YShopAuthorizationDetails YShopAuthorizationDetails
        {
            get { return authorizationDetails; }
        }

        public YConfirmation YConfirmation
        {
            get { return confirmation; }
        }

        public YPaymentMethod YPaymentMethod
        {
            get { return paymentMethod; }
        }

        public YCancelationDetails YCancelationDetails
        {
            get { return cancelationDetails; }
        }

        internal YShopPayment(ShopPayment payment)
        {
            ShopPayment = payment;

            if (payment.amount != null)
            {
                amount = new YAmount(payment.amount);
            }

            if (payment.authorization_details != null)
            {
                authorizationDetails = new YShopAuthorizationDetails(payment.authorization_details);
            }

            if (payment.confirmation != null)
            {
                confirmation = new YConfirmation(payment.confirmation);
            }

            if (payment.payment_method != null)
            {
                paymentMethod = new YPaymentMethod(payment.payment_method);
            }

            if (payment.cancellation_details != null)
            {
                cancelationDetails = new YCancelationDetails(payment.cancellation_details);
            }

            if (payment.refunded_amount != null)
            {
                refundedAmount = new YAmount(payment.refunded_amount);
            }
        }

        public override string ToString()
        {
            return ShopPayment.ToString();
        }

        public string ToString(bool isFormatting)
        {
            return ShopPayment.ToString(isFormatting);
        }
    }
}
