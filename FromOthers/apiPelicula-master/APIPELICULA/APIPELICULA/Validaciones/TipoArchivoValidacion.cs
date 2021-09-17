using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace APIPELICULA.Validaciones
{
    public class TipoArchivoValidacion: ValidationAttribute
    {
        private readonly string[] _tiposValidos;

        public TipoArchivoValidacion(String[] tiposValidos)
        {
            _tiposValidos = tiposValidos;
        }

        public TipoArchivoValidacion(GrupotipoArchivo grupotipoArchivo)
        {
            if (grupotipoArchivo == GrupotipoArchivo.Imagen)
            {
                _tiposValidos = new[] {"image/jpeg", "image/png", "image/gif"};
            }
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

            if (!_tiposValidos.Contains(formFile.ContentType))
            {
                return new ValidationResult(
                    $"El tipo de archivo de ser: {string.Join(", ", _tiposValidos)}");
            }
            
            return ValidationResult.Success;
        }
    }
}