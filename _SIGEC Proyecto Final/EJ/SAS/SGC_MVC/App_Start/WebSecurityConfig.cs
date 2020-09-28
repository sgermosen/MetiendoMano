using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace SGC_MVC
{
    public static class WebSecurityConfig
    {
        public static void RegisterWebSec()
        {
            WebSecurity.InitializeDatabaseConnection
                (
                 "SASContext",
                 "user",
                 "ID",
                 "username",
                 autoCreateTables: true
                );
        }
    }
}