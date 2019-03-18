using System;
using System.Collections.Generic;
using System.Globalization;
using org.BTR.YandexShop.InnerClases;

namespace org.BTR.YandexShop
{
    public sealed class YShopRequestBuilder
    {
        private readonly ShopPayment _payObject;

        public YShopRequestBuilder(decimal sum, Uri returnUri)
        {
            _payObject = new ShopPayment();

            _payObject.amount = new Amount
            {
                value = decimal.Round(sum, 2).ToString("#0.00", CultureInfo.InvariantCulture),
                currency = "RUB"
            };

            _payObject.confirmation = new Confirmation {
                type = "redirect",
                return_url = returnUri.ToString()
            };
        }

        public YShopRequestBuilder Description(string description)
        {
            _payObject.description = description;

            return this;
        }

        public YShopRequestBuilder WithoutCapture()
        {
            _payObject.capture = true;

            return this;
        }

        public YShopRequestBuilder PaymentMethod(YPaymentType paymentMethod)
        {
            _payObject.payment_method_data = new PaymentMethodData {type = paymentMethod.ToString()};

            return this;
        }

        public YShopRequestBuilder Metadata(string key, string value)
        {
            if (_payObject.metadata == null)
            {
                _payObject.metadata = new Dictionary<string, string>();
            }
            _payObject.metadata.Add(key, value);

            return this;
        }

        public YShopRequestBuilder Currency(string currency)
        {
            _payObject.amount.currency = currency;

            return this;
        }

        public string Build()
        {
            return _payObject.ToString();
        }
    }
}