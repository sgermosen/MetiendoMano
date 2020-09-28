using Common;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace FrontEnd.App_Start
{
    public class ParamaterConfig
    {
        public static void Initialize()
        {
            var properties = typeof(Parameters).GetProperties(
                BindingFlags.Public |
                BindingFlags.Static
            );

            foreach (var p in properties)
            {
                var setting = ConfigurationManager.AppSettings.Get(p.Name);

                if (p.PropertyType.Name.ToLower().Equals("string"))
                {
                    p.SetValue(typeof(Parameters), setting);
                }

                if (p.PropertyType.Name.ToLower().Equals("int32"))
                {
                    p.SetValue(typeof(Parameters), Convert.ToInt32(setting));
                }

                if (p.PropertyType.Name.ToLower().Equals("decimal"))
                {
                    p.SetValue(typeof(Parameters), Convert.ToDecimal(setting));
                }

                if (p.PropertyType.Name.ToLower().Equals("double"))
                {
                    p.SetValue(typeof(Parameters), Convert.ToDouble(setting));
                }

                if (p.PropertyType.Name.ToLower().Equals("boolean"))
                {
                    p.SetValue(typeof(Parameters), (setting.ToLower() == "true"));
                }

                if (p.PropertyType.Name.ToLower().Equals("list`1"))
                {
                    p.SetValue(typeof(Parameters), setting.ToLower().Split(',').ToList());
                }
            }
        }
    }
}