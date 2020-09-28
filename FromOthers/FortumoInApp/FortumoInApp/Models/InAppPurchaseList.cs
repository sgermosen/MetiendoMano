using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace InApp.Models
{
    [DataContract]
    public class InAppPurchaseList
    {
        [DataMember]
        public List<InAppPurchase> Purchases { get; set; }
    }
}





