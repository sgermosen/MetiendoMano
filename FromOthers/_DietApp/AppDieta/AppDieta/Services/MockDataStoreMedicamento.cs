using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppDieta.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(AppDieta.Services.MockDataStoreMedicamento))]
namespace AppDieta.Services
{
    public class MockDataStoreMedicamento : IDataStore<Medicamento>
    {
        bool isInitialized;
      
        List<Medicamento> Medicamentos;
        

        public async Task<bool> AddItemAsync(Medicamento item)
        {
            await InitializeAsync();

            Medicamentos.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Medicamento item)
        {
            await InitializeAsync();

            var _item = Medicamentos.Where((Medicamento arg) => arg.Id == item.Id).FirstOrDefault();
            Medicamentos.Remove(_item);
            Medicamentos.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Medicamento item)
        {
            await InitializeAsync();

            var _item = Medicamentos.Where((Medicamento arg) => arg.Id == item.Id).FirstOrDefault();
            Medicamentos.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Medicamento> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(Medicamentos.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Medicamento>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(Medicamentos);
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


            Medicamentos = new List<Medicamento>();
            var _Medicamento = new List<Medicamento>
            {
                new Medicamento { Id = Guid.NewGuid().ToString(), Text = "Vitamina renal (riñón) y vitamina D nutritiva", Description="Proporciona vitaminas y otros nutrientes necesarios debido a pérdida durante la diálisis, ingesta alimentaria inadecuada o aumento de las necesidades nutricionales"},
                new Medicamento { Id = Guid.NewGuid().ToString(), Text = "Hierro", Description="Ayuda a mejorar la producción de hemoglobina y a tratar la anemia debida a deficiencias alimentarias o a la pérdida de sangre"},
                new Medicamento { Id = Guid.NewGuid().ToString(), Text = "Aglutinante de fosfato", Description="Ayuda a reducir la absorción de fósforo que se consumió en la dieta"},
                new Medicamento { Id = Guid.NewGuid().ToString(), Text = "Ablandador de heces", Description="Alivia el estreñimiento debido a la ingesta limitada de líquido, a ciertos medicamentos o a la inactividad"},
                new Medicamento { Id = Guid.NewGuid().ToString(), Text = "Heparina", Description="Evita que se formen coágulos de sangre en los tubos de diálisis o en el dializador"},
                new Medicamento { Id = Guid.NewGuid().ToString(), Text = "Epogen, Aranesp o Mircera", Description="Combate la anemia y aumenta el recuento sanguíneo"},
            };

            foreach (Medicamento item in _Medicamento)
            {
                Medicamentos.Add(item);
            }



            isInitialized = true;
        }
    }

}
