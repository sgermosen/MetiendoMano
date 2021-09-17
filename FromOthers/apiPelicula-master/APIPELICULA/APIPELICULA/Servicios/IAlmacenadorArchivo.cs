using System.Threading.Tasks;

namespace APIPELICULA.Servicios
{
    public interface IAlmacenadorArchivo
    {
        Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta,
            string contentType);
        
        Task BorrarArchivo(string ruta ,string contenedor);
        
        Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor,string contentType);
    }
}