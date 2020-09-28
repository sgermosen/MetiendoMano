using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class HistDocument
    {
        public int documentID { get; set; }
        public Nullable<int> documentParentID { get; set; }
        public int documentTypeID { get; set; }
        public double EDT { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string documentText { get; set; }
        public string url { get; set; }
        public Nullable<System.DateTime> dateAdded { get; set; }
        public Nullable<int> createUser { get; set; }
        public int version { get; set; }
        public int companyID { get; set; }
        public int ID { get; set; }
        public string changeReason { get; set; }
        public int responsible { get; set; }
    }
}
