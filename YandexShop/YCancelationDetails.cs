using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.BTR.YandexShop.InnerClases;

namespace org.BTR.YandexShop
{
    public class YCancelationDetails
    {
        internal CancelationDetails CancelationDetails { get; private set; }

        public YCancelationDetailsParty Party
        {
            get
            {
                YCancelationDetailsParty result;

                if (Enum.TryParse(CancelationDetails.party, out result))
                {
                    return result;
                }

                throw new ArgumentOutOfRangeException("party",
                    string.Format("Свойство 'party' не может иметь значение '{0}'", CancelationDetails.party));
            }

            set { CancelationDetails.party = value.ToString(); }
        }

        public YCancelationDetailsReason Reason
        {
            get
            {
                YCancelationDetailsReason result;

                var testValue = CancelationDetails.reason == "3d_secure_failed" 
                    ? "d3_secure_failed"
                    : CancelationDetails.reason;

                if (Enum.TryParse(testValue, out result))
                {
                    return result;
                }

                throw new ArgumentOutOfRangeException("reason",
                    string.Format("Свойство 'reason' не может иметь значение '{0}'", CancelationDetails.reason));
            }

            set
            {
                CancelationDetails.reason = value == YCancelationDetailsReason.d3_secure_failed
                    ? "3d_secure_failed"
                    : value.ToString();
            }
        }

        public YCancelationDetails(YCancelationDetailsParty party, YCancelationDetailsReason reason)
        {
            Party = party;
            Reason = reason;
        }

        internal YCancelationDetails(CancelationDetails cancelationDetails)
        {
            CancelationDetails = cancelationDetails;
        }

    }
}
