using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppDieta.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(AppDieta.Services.MockDataStore))]
namespace AppDieta.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        bool isInitialized;
        List<Item> items;
        List<Alimento> Alimentos;
        List<Consejo> Consejos;
        List<Cuidados> Cuidados;
        List<Medicamento> Medicamentos;
        List<Pregunta> Preguntas;
        List<Receta> Recetas;

        public async Task<bool> AddItemAsync(Item item)
        {
            await InitializeAsync();

            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            await InitializeAsync();

            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            await InitializeAsync();

            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
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

            items = new List<Item>();
            var _items = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "test", Description="The cats are hungry"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "test de cacahuate", Description="Seems like a functional idea"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "test de coco", Description="Noted"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "test de girasol", Description="Pine and cranberry for that winter feel"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "test de higado de bacalao", Description="Keep it a secret!"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "test de maíz", Description="Done"},
            };

            foreach (Item item in _items)
            {
                items.Add(item);
            }


            Alimentos = new List<Alimento>();
            var _Alimentos = new List<Alimento>
            {
                new Alimento { Id = Guid.NewGuid().ToString(), Text = "Abadejo", Description="The cats are hungry"},
                new Alimento { Id = Guid.NewGuid().ToString(), Text = "Aceite de cacahuate", Description="Seems like a functional idea"},
                new Alimento { Id = Guid.NewGuid().ToString(), Text = "Aceite de coco", Description="Noted"},
                new Alimento { Id = Guid.NewGuid().ToString(), Text = "Aceite de girasol", Description="Pine and cranberry for that winter feel"},
                new Alimento { Id = Guid.NewGuid().ToString(), Text = "Aceite de higado de bacalao", Description="Keep it a secret!"},
                new Alimento { Id = Guid.NewGuid().ToString(), Text = "Aceite de maíz", Description="Done"},
            };

            foreach (Alimento item in _Alimentos)
            {
                Alimentos.Add(item);
            }


            Consejos = new List<Consejo>();
            var _Consejos = new List<Consejo>
            {
                new Consejo { Id = Guid.NewGuid().ToString(), Text = "Buy some cat food", Description="The cats are hungry"},
                new Consejo { Id = Guid.NewGuid().ToString(), Text = "Learn F#", Description="Seems like a functional idea"},
                new Consejo { Id = Guid.NewGuid().ToString(), Text = "Learn to play guitar", Description="Noted"},
                new Consejo { Id = Guid.NewGuid().ToString(), Text = "Buy some new candles", Description="Pine and cranberry for that winter feel"},
                new Consejo { Id = Guid.NewGuid().ToString(), Text = "Complete holiday shopping", Description="Keep it a secret!"},
                new Consejo { Id = Guid.NewGuid().ToString(), Text = "Finish a todo list", Description="Done"},
            };

            foreach (Consejo item in _Consejos)
            {
                Consejos.Add(item);
            }


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

            Recetas = new List<Receta>();
            var _Recetas = new List<Receta>
            {
                new Receta { Id = Guid.NewGuid().ToString(), Text = "Primer Plato", Description="The cats are hungry"},
                new Receta { Id = Guid.NewGuid().ToString(), Text = "Segundo Plato", Description="Seems like a functional idea"},
                new Receta { Id = Guid.NewGuid().ToString(), Text = "Postre", Description="Noted"},
                new Receta { Id = Guid.NewGuid().ToString(), Text = "Platos completos", Description="Pine and cranberry for that winter feel"},
            };

            foreach (Receta Receta in _Recetas)
            {
                Recetas.Add(Receta);
            }

            isInitialized = true;
        }
    }

}
