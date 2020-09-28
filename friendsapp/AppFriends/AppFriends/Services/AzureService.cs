using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace AppFriends.Services
{
    public class AzureService<T>
    {
        private IMobileServiceClient _client;
        private IMobileServiceTable<T> _table;

        /* O código inicia o cliente do back-end e a instância IMobileServiceTable para poder realizar operações com o quadro do back-end. 
            Certifique-se de atribuir a URL de seu Azure Mobile Apps na variável MyAppServiceURL. */
        public AzureService()
        {
            string myAppServiceURL = "http://friendsappvini.azurewebsites.net";
            _client = new MobileServiceClient(myAppServiceURL);
            _table = _client.GetTable<T>();
        }

        // O seguinte código que permitirá obter os dados de uma tabela como uma coleção IEnumerable.
        public Task<IEnumerable<T>> GetTable()
        {
            return _table.ToEnumerableAsync();
        }
    }
}
