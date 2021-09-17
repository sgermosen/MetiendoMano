using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace APIPELICULA.Validaciones
{
    public class PesoArchivoValidacion : ValidationAttribute
    {
        private readonly int _pesoMaximomegabytes;

        public PesoArchivoValidacion(int pesoMaximomegabytes)
        {
            _pesoMaximomegabytes = pesoMaximomegabytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
                
            }

            IFormFile formFile = value as IFormFile;
            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (formFile.Length > _pesoMaximomegabytes * 1024 * 1024)
            {
                return new ValidationResult($"El peso no debe ser mayor a {_pesoMaximomegabytes}");
            }

            return ValidationResult.Success;
        }
    }
}