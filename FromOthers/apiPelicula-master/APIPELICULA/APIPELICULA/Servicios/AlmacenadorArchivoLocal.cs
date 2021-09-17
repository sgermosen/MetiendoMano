using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace APIPELICULA.Servicios
{
    public class AlmacenadorArchivoLocal : IAlmacenadorArchivo
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _contextAccessor;

        public AlmacenadorArchivoLocal( IWebHostEnvironment env, IHttpContextAccessor contextAccessor)
        {
            _env = env;
            _contextAccessor = contextAccessor;
        }
        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, 
            string contentType)
        {
            await BorrarArchivo(ruta, contenedor);

            return await GuardarArchivo(contenido, extension, contenedor, contentType);
        }

        public Task BorrarArchivo(string ruta, string contenedor)
        {
            if (ruta != null)
            {
                var nombreArchivo = Path.Combine(ruta);
                string directorio = Path.Combine(_env.WebRootPath, contenedor, nombreArchivo);

                if (File.Exists(directorio))
                {
                    File.Delete(directorio);
                }
            }

            return Task.FromResult(0);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(_env.WebRootPath, contenedor);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory((folder));
            }

            string ruta = Path.Combine(folder, nombreArchivo);
            await File.WriteAllBytesAsync(ruta,contenido);
            var urlActula = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";
            var urlparaBd = Path.Combine(urlActula, contenedor, nombreArchivo).Replace("\\", "/");
            return urlparaBd;
        }
    }
}