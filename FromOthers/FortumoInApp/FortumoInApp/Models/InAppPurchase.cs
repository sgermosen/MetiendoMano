using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace InApp.Models
{
    [DataContract]
    public class InAppPurchase
    {
        /// <summary>
        /// A unique order identifier for the transaction. 
        /// 
        /// This identifier corresponds to the Google payments order ID.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Indicates whether the subscription renews automatically. 
        /// 
        /// If true, the subscription is active, and will automatically renew on the 
        /// next billing date. If false, indicates that the user has canceled the subscription. 
        /// The user has access to subscription content until the next billing date and will 
        /// lose access at that time unless they re-enable automatic renewal (or manually renew, 
        /// as described in Manual Renewal). If you offer a grace period, this value remains set 
        /// to true for all subscriptions, as long as the grace period has not lapsed. 
        /// The next billing date is extended dynamically every day until the end of the grace 
        /// period or until the user fixes their payment method.
        /// </summary>
        public bool AutoRenewing { get; set; }

        /// <summary>
        ///	The application package from which the purchase originated.
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// The item's product identifier. 
        /// 
        /// Every item has a product ID, which you must specify in the application's product list 
        /// on the Google Play Developer Console.
        /// </summary>
        [DataMember]
        public string ProductId { get; set; }
        
        /// <summary>
        /// The time the product was purchased, in milliseconds since the epoch (Jan 1, 1970).
        /// </summary>
        public DateTime PurchaseTime { get; set; }

        /// <summary>
        /// The purchase state of the order. 
        /// 
        /// Possible values are 0 (purchased), 1 (canceled), or 2 (refunded).
        /// </summary>
        public int PurchaseState { get; set; }
        
        /// <summary>
        /// A developer-specified string that contains supplemental information about an order. 
        /// 
        /// You can specify a value for this field when you make a getBuyIntent request.
        /// </summary>
        public string DeveloperPayload	{get; set; }
        
        /// <summary>
        /// A token that uniquely identifies a purchase for a given item and user pair.
        /// </summary>
        public string PurchaseToken	{get; set; }
    }
}





