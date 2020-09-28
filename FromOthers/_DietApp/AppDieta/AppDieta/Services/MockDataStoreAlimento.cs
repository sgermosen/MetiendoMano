using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppDieta.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(AppDieta.Services.MockDataStoreAlimento))]
namespace AppDieta.Services
{
    public class MockDataStoreAlimento : IDataStore<Alimento>
    {
        bool isInitialized;
        List<Alimento> items;
     

        public async Task<bool> AddItemAsync(Alimento item)
        {
            await InitializeAsync();

            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Alimento item)
        {
            await InitializeAsync();

            var _item = items.Where((Alimento arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Alimento item)
        {
            await InitializeAsync();

            var _item = items.Where((Alimento arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Alimento> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Alimento>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(items);
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

            items = new List<Alimento>();
            var _items = new List<Alimento>
            {
                    new Alimento { Id = Guid.NewGuid().ToString(), Text = "Abadejo", Description="Para producir el potasio ver consejo de carne y pescado"},
                    new Alimento { Id = Guid.NewGuid().ToString(), Text = "Aceite de cacahuate", Description="Es un aceite comestible dulce y apetecible de cacahuate, también llamado aceite de maní, está elaborado de Arachis hipogea, una planta anual de lento crecimiento, que es el único miembro de la familia Fabaceae"},
                    new Alimento { Id = Guid.NewGuid().ToString(), Text = "Aceite de coco", Description="Los beneficios del aceite de coco incluyen cuidado del cabello, cuidado de la piel, alivio del estrés, mantenimiento de nivel de colesterol, pérdida del peso, estimular el sistema del inmune, la digestión y regular el metabolismo. También proporciona la relevación de los problemas renales, enfermedades cardíacas, hipertensión arterial, diabetes, VIH y el cáncer, mientras que ayuda a mejorar la cualidad de los dientes y los huesos."},
                    new Alimento { Id = Guid.NewGuid().ToString(), Text = "Aceite de girasol", Description="El aceite de girasol o aceite de maravilla es un aceite de origen vegetal que se extrae del prensado de las semillas del capítulo de la planta de girasol, también llamado chimalate, jáquima, maravilla, mirasol, tlapololote, maíz de teja."},
                    new Alimento { Id = Guid.NewGuid().ToString(), Text = "Aceite de higado de bacalao", Description="Debido a su contenido en ácidos grasos y vitaminas A y D, se utiliza para prevenir los resfriados, la tos y demás enfermedades del aparato respiratorio, como complemento en el tratamiento del acné, para mantener en buen estado dientes y huesos, y para prevenir la osteoporosis entre otras."},
                    new Alimento { Id = Guid.NewGuid().ToString(), Text = "Aceite de maíz", Description="El porcentaje de aceite de un grano de maíz oscila entre el 3,1 y el 5,7% del peso del mismo y el 83% de ese contenido graso se ubica en el germen. Este se separa del resto del grano en la primera etapa del proceso de molienda húmeda, obteniéndose de esta manera la materia prima para la recuperación del aceite."},
            };

            foreach (Alimento item in _items)
            {
                items.Add(item);
            }

            isInitialized = true;
        }
    }

}
