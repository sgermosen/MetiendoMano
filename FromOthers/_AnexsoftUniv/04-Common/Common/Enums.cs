using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Common
{
    public class Enums
    {
        public enum EntityType
        {
            Users,
            Categories,
            Courses
        }

        public enum IncomeType
        {
            Total,
            CompanyTotal,
            TeacherTotal
        }

        public enum PanelMenu
        {
            Home,
            Categories,
            Courses,
            Users,
        }

        public enum Gender
        {
            [Description("Masculino")]
            Male,
            [Description("Femenino")]
            Female
        }

        public enum Status
        {
            [Description("Pendiente")]
            Pending,

            [Description("Aprobado")]
            Approved,

            [Description("Rechazado")]
            Rejected
        }

        public enum MyFilters
        {
            IsDeleted
        }

        public static IEnumerable<EnumDescriptionAndValue> GetAllEnumsWithChilds()
        {
            var enums = new List<EnumDescriptionAndValue>();
            var order = 0;

            foreach (var type in typeof(Enums).GetNestedTypes())
            {
                var parent = new EnumDescriptionAndValue
                {
                    Name = type.Name,
                    Order = order
                };

                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                {
                    var i = 0;
                    var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                    parent.Childs.Add(new EnumDescriptionAndValue
                    {
                        Name = field.Name,
                        Value = field.GetRawConstantValue().ToString(),
                        Description = attribute == null ? field.Name : attribute.Description,
                        Order = i
                    });

                    i++;
                }

                enums.Add(parent);
                order++;
            }

            return enums;
        }
    }

    public class EnumDescriptionAndValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public List<EnumDescriptionAndValue> Childs { get; set; }

        public EnumDescriptionAndValue()
        {
            Childs = new List<EnumDescriptionAndValue>();
        }
    }
}
