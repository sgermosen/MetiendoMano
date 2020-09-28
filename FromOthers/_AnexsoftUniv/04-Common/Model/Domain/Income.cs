using Common;
using Common.CustomFilters;
using Model.Auth;
using Model.Helper;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Domain
{
    public class Income : AuditEntity, ISoftDeleted
    {
        public int Id { get; set; }
        public Enums.EntityType EntityType { get; set; }
        public Enums.IncomeType IncomeType { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public int EntityID { get; set; }

        public bool Deleted { get; set; }
    }
}
