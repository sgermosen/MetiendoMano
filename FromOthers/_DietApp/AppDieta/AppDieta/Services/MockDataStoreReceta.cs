using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppDieta.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(AppDieta.Services.MockDataStoreReceta))]
namespace AppDieta.Services
{
    public class MockDataStoreReceta : IDataStore<Receta>
    {
        bool isInitialized;

        List<Receta> Recetas;

        public async Task<bool> AddItemAsync(Receta item)
        {
            await InitializeAsync();

            Recetas.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Receta item)
        {
            await InitializeAsync();

            var _item = Recetas.Where((Receta arg) => arg.Id == item.Id).FirstOrDefault();
            Recetas.Remove(_item);
            Recetas.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Receta item)
        {
            await InitializeAsync();

            var _item = Recetas.Where((Receta arg) => arg.Id == item.Id).FirstOrDefault();
            Recetas.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Receta> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(Recetas.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Receta>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(Recetas);
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


            Recetas = new List<Receta>();
            var _Recetas = new List<Receta>
            {
                new Receta { Id = Guid.NewGuid().ToString(), Text = "Primer Plato", Description="Ensalada Mar y Tierra básica – Ingredientes 1 lata de atún al natural 4 palitos de cangrejo 2 tomates (mejor si son del huerto, ya sabéis!) una cucharadita de aceite de oliva virgen extra una cucharadita de vinagre de vino  50 gr de Jamón Serrano (cuanto mejor sea el jamón, más rico estará el plato!) Ensalada Mar y ierra básica – Preparación En un mini bol incorporamos el atún al natural, los palitos de cangrejo (o surimi) en trozos de un dedo de grosor y los tomates lavados y cortados en daditos."},
                new Receta { Id = Guid.NewGuid().ToString(), Text = "Segundo Plato", Description="Las albóndigas son uno de las recetas más tradicionales de nuestra gastronomía, un plato consumido en todo el país y con multitud de variantes a la hora de su elaboración. Son unas bolas hechas de carne picada, habitualmente de cerdo o ternera, y otra serie de ingredientes adicionales, que normalmente se acompañan de alguna salsa, aunque se pueden servir también sin ella, fritas, cocidas o de alguna otra forma.En esta web vamos a proponerte una serie de diferentes recetas para que puedas preparar en casa unas riquísimas albóndigas, todas explicadas detalladamente para que te sea lo más sencillo posible. Esperamos que te gusten. "},
                new Receta { Id = Guid.NewGuid().ToString(), Text = "Postre", Description="4 peras no muy maduras, 125 gr. azúcar, media botella de vino tinto con cuerpo, una cucharada de zumo de limón, la piel de una naranja o limón, si es posible no tratada con insecticidas, un sobre de azúcar vainillado, pizca de canela molida, 2 clavos, anís estrellado (opcional) y cardamomo (opcional)."},
                new Receta { Id = Guid.NewGuid().ToString(), Text = "Platos completos", Description="Magdalenas esponjosas y caseras. Una magdalena recién salida del horno, bien esponjosa y con su copete sobresaliendo de la base envuelta en el papel rizado, una imagen a la que es difícil resistirse. Dentro de las recetas de postres del blog esta receta es la que más triunfa y es porque está al alcance de todos.La receta de magdalenas caseras es muy sencilla y nos tendrá poco tiempo en la cocina. En tan solo media hora podemos elaborar una rica hornada. Conocemos de sobra lo que son las magdalenas y seguramente habréis preparado unas cuantas en casa. Pero además de compartir mi receta para unas magdalenas perfectas voy también a daros unos cuantos consejos prácticos para asegurar el éxito en su preparación."},
            };

            foreach (Receta Receta in _Recetas)
            {
                Recetas.Add(Receta);
            }

            isInitialized = true;
        }

      
    }

}
