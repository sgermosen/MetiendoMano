using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsDivisas
{
    class Conversion
    {

        public static decimal ToDolares(decimal Pesos)
        {

            return Pesos / 2560;
        }
        public static decimal ToEuros(decimal Pesos)
        {

            return Pesos / 2890;
        }
        public static decimal ToLibras(decimal Pesos)
        {

            return Pesos / 3576;
        }
       
    }
}
