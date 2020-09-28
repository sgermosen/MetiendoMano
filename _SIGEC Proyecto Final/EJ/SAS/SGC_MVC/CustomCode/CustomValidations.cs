using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGC_MVC.Models;

namespace SGC_MVC.CustomCode
{
    public class CustomValidations
    {
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public sealed class DateStartAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            DateTime dateStart = (DateTime)value;
            // El tiempo de inicio debe ser mayor o 
            // igual al dia de hoy
            return (dateStart >= DateTime.Now);
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "datestart"
            };
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public sealed class DateEndAttribute : ValidationAttribute, IClientValidatable
    {

        public DateEndAttribute()
            :base("{0} debe ser mayor que {1}")
        {
        }

        public string DateStartProperty { get; set; }
        public override bool IsValid(object value)
        {
            // Obtener el valor de la propiedad DateStart
            string dateStartString = HttpContext.Current.Request[DateStartProperty];
            DateTime dateEnd = (DateTime)value;
            DateTime dateStart = DateTime.Parse(dateStartString);

            // El tiempo de finalizar debe ser mayor o igual  
            // que el tiempo de inicio
            return dateStart <= dateEnd;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("datestart", DateStartProperty);
            rule.ValidationType = "dateend";

            yield return rule;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, new UserTask().GetDisplayName(u => u.fromDate));
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class MaxFileSizeAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly int MaxFileSize;
        public MaxFileSizeAttribute(int MaxFileSize)
        {
            this.MaxFileSize = MaxFileSize;
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return true;
            }
            return file.ContentLength <= MaxFileSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(MaxFileSize.ToString());
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(MaxFileSize.ToString()),
                ValidationType = "filesize"
            };
            rule.ValidationParameters.Add("maxsize", MaxFileSize);

            yield return rule;
        }
    }
}