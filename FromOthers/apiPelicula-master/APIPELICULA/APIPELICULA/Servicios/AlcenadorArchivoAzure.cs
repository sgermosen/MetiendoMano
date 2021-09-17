using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;

namespace APIPELICULA.Servicios
{
    public class AlcenadorArchivoAzure : IAlmacenadorArchivo
    {
        private readonly string _connectionString;
        public AlcenadorArchivoAzure(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AzureStorage");
        }
        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, 
            string contentType)
        {
            await BorrarArchivo(ruta, contenedor);

            return await GuardarArchivo(contenido, extension, contenedor, contentType);
        }

        public async Task BorrarArchivo(string ruta, string contenedor)
        {
            if (ruta != null)
            {
                var cuenta = CloudStorageAccount.Parse(_connectionString);
                var cliente = cuenta.CreateCloudBlobClient();
                var contenedorRef = cliente.GetContainerReference(contenedor);

                var nombreblob = Path.GetFileName(ruta);
                var blob = contenedorRef.GetBlobReference(nombreblob);
                await blob.DeleteIfExistsAsync();

            }
        }

        public async  Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, 
            string contentType)
        {
            var cuenta = CloudStorageAccount.Parse(_connectionString);
            var cliente = cuenta.CreateCloudBlobClient();
            var contenedorRef = cliente.GetContainerReference(contenedor);

            await contenedorRef.CreateIfNotExistsAsync();
            await contenedorRef.SetPermissionsAsync( new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            var blob = contenedorRef.GetBlockBlobReference(nombreArchivo);
            await blob.UploadFromByteArrayAsync(contenido, 0, contenido.Length);
            blob.Properties.ContentType = contentType;
            
            await  blob.SetPropertiesAsync();
            return blob.Uri.ToString();
        }
    }
}