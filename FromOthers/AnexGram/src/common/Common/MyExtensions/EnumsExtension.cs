using System;
using System.ComponentModel;

namespace Common.MyExtensions
{
    public static class EnumsExtension
    {
        public static int ToInt32(this Enum type)
        {
            return Convert.ToInt32(type);
        }

        public static string GetDescription(this Enum type)
        {
            var field = type.GetType().GetField(type.ToString());

            DescriptionAttribute attribute =
                Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                as DescriptionAttribute;

            return attribute == null
                ? type.ToString()
                : attribute.Description;
        }

        public static string GetAttributeName(this Enum type)
        {
            return type.ToString();
        }
    }
}
