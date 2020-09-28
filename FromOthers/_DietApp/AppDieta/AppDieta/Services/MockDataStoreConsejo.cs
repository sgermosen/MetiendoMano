using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppDieta.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(AppDieta.Services.MockDataStoreConsejo))]
namespace AppDieta.Services
{
    public class MockDataStoreConsejo : IDataStore<Consejo>
    {
        bool isInitialized;
        List<Consejo> Consejos;
  

        public async Task<bool> AddItemAsync(Consejo item)
        {
            await InitializeAsync();

            Consejos.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Consejo item)
        {
            await InitializeAsync();

            var _item = Consejos.Where((Consejo arg) => arg.Id == item.Id).FirstOrDefault();
            Consejos.Remove(_item);
            Consejos.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Consejo item)
        {
            await InitializeAsync();

            var _item = Consejos.Where((Consejo arg) => arg.Id == item.Id).FirstOrDefault();
            Consejos.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Consejo> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(Consejos.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Consejo>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(Consejos);
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

          

            Consejos = new List<Consejo>();
            var _Consejos = new List<Consejo>
            {
                new Consejo { Id = Guid.NewGuid().ToString(), Text = "Alimentos con alto contenido de sodio", Description="Evitar alimentos con alto contenido de sal, aperitivos salados. aceitunas"},
                new Consejo { Id = Guid.NewGuid().ToString(), Text = "Alternativa a la sal", Description="usar alternativas a la sal como especias y hierbas aromáticas y consumir procutos frescos no manufacturados"},
                new Consejo { Id = Guid.NewGuid().ToString(), Text = "Salero", Description="la cantidad de sal común que usted puede usar para todas las comidas del día no debe sobrepasar 1.5g"},
                new Consejo { Id = Guid.NewGuid().ToString(), Text = "Sodio en las etiquetas de los alimentos", Description="Mira el contenido de sodio en las etiquetas de los alimentos.Los alimentos que contienen más de 1.2g de sal "},
                
            };

            foreach (Consejo item in _Consejos)
            {
                Consejos.Add(item);
            }


       

            isInitialized = true;
        }

       
    }

}
