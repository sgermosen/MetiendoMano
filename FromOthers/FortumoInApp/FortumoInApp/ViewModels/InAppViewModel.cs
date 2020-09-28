using InApp.Models;
using InApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;


namespace InApp.ViewModels
{
    public class InAppViewModel : ViewModelBase
    {
        IFortumoInAppService _inAppService;

        ObservableCollection<InAppProduct> _products;
        ObservableCollection<InAppPurchase> _purchases;
        InAppPurchaseList _purchaseList;

        public InAppViewModel()
        {
            _inAppService = DependencyService.Get<IFortumoInAppService>();
            _inAppService.OnQueryInventory += OnQueryInventory;
            _inAppService.OnPurchaseProduct += OnPurchaseProduct;
            _inAppService.OnRestoreProducts += OnRestoreProducts;

            _purchases = new ObservableCollection<InAppPurchase>();
            _purchaseList = new InAppPurchaseList();

            InitializeProducts();

            QueryCommand = new Command<InAppProduct>(
                execute: (product) =>
                {
                    _inAppService.QueryInventory();
                });

            PurchaseCommand = new Command<InAppProduct>(
                execute: (product) =>
                {
                    _inAppService.PurchaseProduct(product.ProductId);
                });

            RestoreCommand = new Command<InAppProduct>(
                execute: (product) =>
                {
                    _inAppService.RestoreProducts();
                });
        }

        void OnQueryInventory()
        {
            throw new System.NotImplementedException();
        }

        void OnPurchaseProduct()
        {
            throw new System.NotImplementedException();
        }
        void OnRestoreProducts()
        {
            throw new System.NotImplementedException();
        }

        public ObservableCollection<InAppProduct> Products
        {
            private set { SetProperty(ref _products, value); }
            get { return _products; }
        }

        public ObservableCollection<InAppPurchase> Purchases 
        { 
            private set { SetProperty(ref _purchases, value); }
            get { return _purchases; }
        }

        public ICommand QueryCommand { private set; get; }

        public ICommand PurchaseCommand { private set; get; }

        public ICommand RestoreCommand { private set; get; }

        public void SaveState(IDictionary<string, object> dictionary)
        {
            _purchaseList.Purchases = new List<InAppPurchase>(_purchases);

            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(InAppPurchaseList));
                ser.WriteObject(ms, _purchaseList);
                ms.Position = 0;
                using (var sr = new StreamReader(ms))
                {
                    var purchases = sr.ReadToEnd();
                    dictionary["Purchases"] = purchases;
                }
            }
        }

        public void RestoreState(IDictionary<string, object> dictionary)
        {
            string purchases = GetDictionaryEntry(dictionary, "Purchases", string.Empty);
            if (purchases != string.Empty)
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(InAppPurchaseList));
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(purchases)))
                {
                    InAppPurchaseList purchaseList = (InAppPurchaseList)ser.ReadObject(ms);
                    Purchases = new ObservableCollection<InAppPurchase>(purchaseList.Purchases);
                }
            }
        }

        public T GetDictionaryEntry<T>(IDictionary<string, object> dictionary, 
                                        string key, T defaultValue)
        {
            if (dictionary.ContainsKey(key))
                return (T)dictionary[key];

            return defaultValue;
        }

        private void InitializeProducts()
        {
            _products = new ObservableCollection<InAppProduct>();

            _products.Add(new InAppProduct
                {
                    Title = "Small Monkey",
                    Description = "Keyboard companion",
                    IconSource = "monkey1.png"
                });
            _products.Add(new InAppProduct
                {
                    Title = "Medium Monkey",
                    Description = "Plush and cuddly",
                    IconSource = "monkey2.png"
                });
            _products.Add(new InAppProduct
            {
                Title = "Large Monkey",
                Description = "Monitor buddy",
                IconSource = "monkey3.png"
            });
        }
    }
}
