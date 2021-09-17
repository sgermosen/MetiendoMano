using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contactes.Web.Helpers
{
    public class NumeroValido : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Convert.ToDecimal(value.ToString()) < 1)
                return false;

            return true;


        }
    }
}
