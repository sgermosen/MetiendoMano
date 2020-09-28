using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppDieta.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(AppDieta.Services.MockDataStoreConsejoDetalle))]
namespace AppDieta.Services
{
    public class MockDataStoreConsejoDetalle : IDataStore<ConsejoDetalle>
    {
        bool isInitialized;
        List<ConsejoDetalle> ConsejoDetalles;
  

        public async Task<bool> AddItemAsync(ConsejoDetalle item)
        {
            await InitializeAsync();

            ConsejoDetalles.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ConsejoDetalle item)
        {
            await InitializeAsync();

            var _item = ConsejoDetalles.Where((ConsejoDetalle arg) => arg.Id == item.Id).FirstOrDefault();
            ConsejoDetalles.Remove(_item);
            ConsejoDetalles.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(ConsejoDetalle item)
        {
            await InitializeAsync();

            var _item = ConsejoDetalles.Where((ConsejoDetalle arg) => arg.Id == item.Id).FirstOrDefault();
            ConsejoDetalles.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<ConsejoDetalle> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(ConsejoDetalles.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ConsejoDetalle>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(ConsejoDetalles);
        }

     
        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }


        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

          

            ConsejoDetalles = new List<ConsejoDetalle>();
            var _ConsejoDetalles = new List<ConsejoDetalle>
            {
                new ConsejoDetalle { Id = Guid.NewGuid().ToString(),Concejo="Alimentos con alto contenido de sodio", Text = "test 1", Description="Evitar alimentos con alto contenido de sal, aperitivos salados. aceitunas"},
                new ConsejoDetalle { Id = Guid.NewGuid().ToString(),Concejo="Alternativa a la sal", Text = "test 2", Description="usar alternativas a la sal como especias y hierbas aromáticas y consumir procutos frescos no manufacturados"},
                new ConsejoDetalle { Id = Guid.NewGuid().ToString(), Concejo="Alimentos con alto contenido de sodio",Text = "test 4", Description="la cantidad de sal común que usted puede usar para todas las comidas del día no debe sobrepasar 1.5g"},
                new ConsejoDetalle { Id = Guid.NewGuid().ToString(), Concejo="Sodio en las etiquetas de los alimentos",Text = "test 4", Description="Mira el contenido de sodio en las etiquetas de los alimentos.Los alimentos que contienen más de 1.2g de sal "},
                
            };

            foreach (ConsejoDetalle item in _ConsejoDetalles)
            {
                ConsejoDetalles.Add(item);
            }


       

            isInitialized = true;
        }

       
    }

}
