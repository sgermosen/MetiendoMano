using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SGC_MVC.Models.Mapping;
using System.Data.Objects;

namespace SGC_MVC.Models
{
    public partial class SASContext : DbContext
    {
        public ObjectContext ObjectContext
        {
            get { return ((IObjectContextAdapter)this).ObjectContext; }
        }
    }
}
