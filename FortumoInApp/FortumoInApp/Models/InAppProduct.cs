using System;
using System.Collections.Generic;

namespace InApp.Models
{
    public class InAppProduct
    {
        /// <summary>
        /// The product ID for the product.
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Value must be “inapp” for an in-app product or "subs" for subscriptions.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 	Formatted price of the item, including its currency sign. The price does not include tax.
        /// </summary>
        public string Price { get; set; }

        public string IconSource { get; set; }

        /// <summary>
        /// Price in micro-units, where 1,000,000 micro-units equal one unit of the currency. 
        /// 
        /// For example, if price is "€7.99", price_amount_micros is "7990000".
        /// </summary>
        public int PriceAmountMicros { get; set; }

        /// <summary>
        /// ISO 4217 currency code for price. 
        /// 
        /// For example, if price is specified in British pounds sterling, price_currency_code is "GBP".
        /// </summary>
        public string PriceCurrencyCode { get; set; }

        /// <summary>
        ///	Title of the product.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of the product.
        /// </summary>
        public string Description { get; set; }
    }
}





