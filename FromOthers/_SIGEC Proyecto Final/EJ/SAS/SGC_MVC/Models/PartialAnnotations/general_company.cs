using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(GeneralCompanyMetadata))]
    public partial class GeneralCompany
    {
    }

    class GeneralCompanyMetadata
    {
        [Key]
        public int companyID { get; set; }
        [DataType(DataType.MultilineText)]
        public string companyMision { get; set; }
        [DataType(DataType.MultilineText)]
        public string companyVision { get; set; }
        [DataType(DataType.MultilineText)]
        public string companyValues { get; set; }
        [DataType(DataType.MultilineText)]
        public string policies { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> createDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> updateDate { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<int> createUser { get; set; }
        [ScaffoldColumn(false)]
        public virtual User User { get; set; }
    }
}
