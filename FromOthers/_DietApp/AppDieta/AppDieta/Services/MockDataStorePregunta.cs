using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppDieta.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(AppDieta.Services.MockDataStorePregunta))]
namespace AppDieta.Services
{
    public class MockDataStorePregunta : IDataStore<Pregunta>
    {
        bool isInitialized;
        List<Pregunta> Preguntas;
      

        public async Task<bool> AddItemAsync(Pregunta item)
        {
            await InitializeAsync();

            Preguntas.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Pregunta item)
        {
            await InitializeAsync();

            var _item = Preguntas.Where((Pregunta arg) => arg.Id == item.Id).FirstOrDefault();
            Preguntas.Remove(_item);
            Preguntas.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Pregunta item)
        {
            await InitializeAsync();

            var _item = Preguntas.Where((Pregunta arg) => arg.Id == item.Id).FirstOrDefault();
            Preguntas.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Pregunta> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(Preguntas.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Pregunta>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(Preguntas);
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

          


            Preguntas = new List<Pregunta>();
            var _Preguntas = new List<Pregunta>
            {
                //new Pregunta { Id = Guid.NewGuid().ToString(), Text = "Buy some cat food", Description="The cats are hungry"},
                //new Pregunta { Id = Guid.NewGuid().ToString(), Text = "Learn F#", Description="Seems like a functional idea"},
                //new Pregunta { Id = Guid.NewGuid().ToString(), Text = "Learn to play guitar", Description="Noted"},
                //new Pregunta { Id = Guid.NewGuid().ToString(), Text = "Buy some new candles", Description="Pine and cranberry for that winter feel"},
                //new Pregunta { Id = Guid.NewGuid().ToString(), Text = "Complete holiday shopping", Description="Keep it a secret!"},
                //new Pregunta { Id = Guid.NewGuid().ToString(), Text = "Finish a todo list", Description="Done"},
            };

            foreach (Pregunta item in _Preguntas)
            {
                Preguntas.Add(item);
            }

          

            isInitialized = true;
        }
    }

}
