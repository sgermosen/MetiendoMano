using System;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Linq;
using System.IO;
using DontBeDoryApp.Clases;
using System.Collections.Generic;

namespace DontBeDoryApp.Datos
{
    public class AzureDataService
    {
        public MobileServiceClient MobileService { get; set; }
        IMobileServiceSyncTable<Notas> tablaNotas;

        bool isInitialized;

        public AzureDataService()
        {
            Initialize();
        }

        public async Task Initialize()
        {
            if (isInitialized)
                return;

            SQLitePCL.Batteries.Init();
            MobileService = new MobileServiceClient("http://dontbedorry.azurewebsites.net");

            var path = "syncstore-notaes-v01.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<Notas>();
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            tablaNotas = MobileService.GetSyncTable<Notas>();
            isInitialized = true;
        }

        public async Task<IEnumerable<Notas>> ObtenerNotas()
        {
            await Initialize();
            await SyncNotas();
            return await tablaNotas.ToListAsync();
        }

        public async Task<Notas> ObtenerNotaID(string id)
        {
            await Initialize();
            await SyncNotas();
            return (await tablaNotas.Where(a => a.Id == id).Take(1).ToEnumerableAsync()).FirstOrDefault();
        }

        public async Task<Notas> AgregarNota(string nota, string cuerpo)
        {
            try
            {
                await Initialize();

                var item = new Notas
                {
                    NombreNota = nota,
                    NombreCuerpo = cuerpo
                };

                await tablaNotas.InsertAsync(item);
                await SyncNotas();
                return item;
            }
            catch (Exception )
            {
                return new Notas();
            }
        }

        public async Task<Notas> ModificarNota(string id, string nota, string cuerpo)
        {
            await Initialize();

            var item = await ObtenerNotaID(id);
            item.NombreNota = nota;
            item.NombreCuerpo= cuerpo;

            await tablaNotas.UpdateAsync(item);
            await SyncNotas();
            return item;
        }

        public async Task EliminarNota(string id)
        {
            await Initialize();

            var item = await ObtenerNotaID(id);
            await tablaNotas.DeleteAsync(item);
            await SyncNotas();
        }

        public async Task SyncNotas()
        {
            try
            {
                await tablaNotas.PullAsync("Notas", tablaNotas.CreateQuery());
                await MobileService.SyncContext.PushAsync();
            }
            catch (Exception )
            {
                return;
            }
        }
    }
}
