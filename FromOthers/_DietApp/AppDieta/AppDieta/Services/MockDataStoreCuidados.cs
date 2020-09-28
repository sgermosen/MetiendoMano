using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppDieta.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(AppDieta.Services.MockDataStoreCuidados))]
namespace AppDieta.Services
{
    public class MockDataStoreCuidados : IDataStore<Cuidados>
    {
        bool isInitialized;
    
        List<Cuidados> Cuidados;
      

        public async Task<bool> AddItemAsync(Cuidados item)
        {
            await InitializeAsync();

            Cuidados.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Cuidados item)
        {
            await InitializeAsync();

            var _item = Cuidados.Where((Cuidados arg) => arg.Id == item.Id).FirstOrDefault();
            Cuidados.Remove(_item);
            Cuidados.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Cuidados item)
        {
            await InitializeAsync();

            var _item = Cuidados.Where((Cuidados arg) => arg.Id == item.Id).FirstOrDefault();
            Cuidados.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Cuidados> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(Cuidados.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Cuidados>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(Cuidados);
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

            Cuidados = new List<Cuidados>();
            var _Cuidados = new List<Cuidados>
            {
                new Cuidados { Id = Guid.NewGuid().ToString(), Text = "Higiene diaria (ducha o baño)", Description="Antes  de  entrar  en  la  sala  de  hemodiálisis  debe  lavarse  el  brazo  y  se  le  aplicará  un antiséptico."},
                new Cuidados { Id = Guid.NewGuid().ToString(), Text = "Vigilar la aparición de calor", Description="Vigilar la aparición de calor, enrojecimiento o hinchazón del brazo.-  Avisar de cualquier otra complicación"},
                new Cuidados { Id = Guid.NewGuid().ToString(), Text = "fístula ", Description="Si  deja    de  notar  su  fístula  o  percibe  alguna  alteración  en  ella  debe  acudir  a  su  hospital de referencia.-  Ante una bajada de tensión brusca controle el buen funcionamiento de la fístula."},
                new Cuidados { Id = Guid.NewGuid().ToString(), Text = "No utilizar: pulseras", Description="relojes ni prendas que puedan comprimir el brazo de la fístula.- Evitar dormir sobre el brazo. - Evitar el rascado de la zona de la fístula."},
               
            };

            foreach (Cuidados item in _Cuidados)
            {
                Cuidados.Add(item);
            }


            isInitialized = true;
        }
    }

}
