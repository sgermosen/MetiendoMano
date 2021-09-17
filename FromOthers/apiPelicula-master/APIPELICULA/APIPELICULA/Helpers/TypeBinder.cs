using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace APIPELICULA.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nombrePropiedad = bindingContext.ModelName;
            var proveedores = bindingContext.ValueProvider.GetValue(nombrePropiedad);

            if (proveedores == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            try
            {
                var desializado = JsonConvert.DeserializeObject<T>(proveedores.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(desializado);
                
            }
            catch (Exception )
            {
                bindingContext.ModelState.TryAddModelError(nombrePropiedad, "Valor invalido para lista <int>");
            }
            
            return Task.CompletedTask;
        }
    }
}