using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using InApp.Models;
using InApp.Services;
using MP;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(InApp.Droid.Services.FortumoInAppService))]

namespace InApp.Droid.Services
{

    public class FortumoInAppService : IFortumoInAppService
    {
        public static FortumoInAppService SharedInstance;

#if TEST_INAPP
        // See here to simulute different types of transactions
        // https://fortumo.com/services/b3341490ed43018b752f8fe66f6d31e4/test
        private const string _serviceId = "enter your testing service id";
        private const string _inAppSecret = "enter your testing secrete";
        private const string _productName = "practicemode.qa";
#else
        private const string _serviceId = "enter your production service id";
        private const string _inAppSecret = "enter your production secret";
        private const string _productName = "practicemode";
#endif

        // We'll go for 10 second time-out for async calls that have a timeout parameter
        private const int _timeOut = 10;

        public FortumoInAppService()
        {

            FortumoInAppService.SharedInstance = this;
        }

        #region IInappService implementation

        public string PracticeModeProductId { get { return "com.simsip.linerunner.practicemode"; } }

        public void Initialize()
        {
            // Load inventory of available products
            this.QueryInventory();
        }

        public void QueryInventory()
        {
            // Are we connected to a network?
            ConnectivityManager connectivityManager = (ConnectivityManager)MainActivity.Instance.GetSystemService(MainActivity.ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            if ((activeConnection != null) && activeConnection.IsConnected)
            {
                // Ok, carefully attempt to connect to the in-app service
                try
                {
                    // Asynchronously get inventory
                    Task.Factory.StartNew(() =>
                        MP.MpUtils.FetchPaymentData(Android.App.Application.Context, FortumoInAppService._serviceId, FortumoInAppService._inAppSecret));

                    // Asnchronously get purchases
                    Task.Factory.StartNew(() =>
                        {
                            var responses = MP.MpUtils.GetPurchaseHistory(Android.App.Application.Context, FortumoInAppService._serviceId, FortumoInAppService._inAppSecret, FortumoInAppService._timeOut);

                            // Record what we purchased
                            foreach (var response in responses)
                            {
                                var paymentResponse = response as PaymentResponse;

                                // Sanity checks
                                if (paymentResponse == null || 
                                    paymentResponse.BillingStatus != MP.MpUtils.MessageStatusBilled)
                                {
                                    continue;
                                }

                                // Ok to record payment
                                this.UpdatePayment(paymentResponse);
                            }
                        });

                    // Get local inventory
                    // IMPORTANT: First time this is called, result will be empty)
                    var priceData = MP.MpUtils.GetFetchedPriceData(Android.App.Application.Context, FortumoInAppService._serviceId, FortumoInAppService._inAppSecret);

                    // TODO: Process priceData
                    // Update inventory
                    var inAppSkuRepository = new InAppProduct();
                    /*
                    foreach (var product in products)
                    {
                        var existingProduct = inAppSkuRepository.GetSkuByProductId(product.ProductId);
                        if (existingProduct != null)
                        {
                            existingProduct.Type = ItemType.Product;
                            existingProduct.Price = product.Price;
                            existingProduct.Title = product.Title;
                            existingProduct.Description = product.Description;
                            existingProduct.PriceCurrencyCode = product.Price_Currency_Code;

                            inAppSkuRepository.Update(existingProduct);
                        }
                        else
                        {
                            var newProduct = new InAppSkuEntity();
                            newProduct.ProductId = product.ProductId;
                            newProduct.Type = ItemType.Product;
                            newProduct.Price = product.Price;
                            newProduct.Title = product.Title;
                            newProduct.Description = product.Description;
                            newProduct.PriceCurrencyCode = product.Price_Currency_Code;

                            inAppSkuRepository.Create(newProduct);
                        }
                    }
                    */

                    this.FireOnQueryInventory();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception trying to connect to in app service: " + ex);
                    this.FireOnQueryInventoryError(-1, null);
                }
            }
        }

        public void PurchaseProduct(string productId)
        {
            try
            {
                // In case we need to process a delayed payment, otherwise our HandleActivityResult below will handle
                MpUtils.EnablePaymentBroadcast(Android.App.Application.Context, "com.simsip.permission.PAYMENT_BROADCAST_PERMISSION");

                PaymentRequest.PaymentRequestBuilder builder = new PaymentRequest.PaymentRequestBuilder();

                builder.SetService(FortumoInAppService._serviceId, FortumoInAppService._inAppSecret);

                // TODO
                var practiceText = string.Empty;
                builder.SetDisplayString(practiceText);

                // Non-consumable purchases are restored using this value
                builder.SetProductName(FortumoInAppService._productName);

                // Non-consumable items can be later restored
                builder.SetType(MpUtils.ProductTypeNonConsumable);

                builder.SetIcon(Resource.Drawable.icon);

                PaymentRequest pr = builder.Build();

                // Can be anything
                int REQUEST_CODE = 1234;

                MainActivity.Instance.StartActivityForResult(pr.ToIntent(Android.App.Application.Context), REQUEST_CODE);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception in PurchaseProduct: " + ex);
            }
        }

        public void HandleActivityResult(int requestCode, Result resultCode, Intent data)
        {
            // TODO: What is this for?
            if(data == null) 
            {
				return;
			}

            try
            {
                if (resultCode == Result.Ok)
                {
                    var paymentResponse = new PaymentResponse(data);

                    switch (paymentResponse.BillingStatus)
                    {
                        case MpUtils.MessageStatusBilled:
                            {
                                // Record what we purchased
                                this.UpdatePayment(paymentResponse);

                                // Let anyone know who is interested that purchase has completed
                                this.FireOnPurchaseProduct(paymentResponse.BillingStatus, null, string.Empty, string.Empty);

                                break;
                            }
                        case MpUtils.MessageStatusNotSent:
                        case MpUtils.MessageStatusFailed:
                            {
                                FortumoInAppService.SharedInstance.FireOnPurchaseProductError(paymentResponse.BillingStatus, string.Empty);
                                break;
                            }
                        case MpUtils.MessageStatusPending:
                            {
                                break;
                            }
                    }
                }
                else
                {
                    // Cancel
                    this.FireOnUserCanceled();
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception in HandleActivityResult: " + ex);
                this.FireOnPurchaseProductError(-1, string.Empty);
            }
		}

        public void RestoreProducts()
        {
            // Are we connected to a network?
            ConnectivityManager connectivityManager = (ConnectivityManager)MainActivity.Instance.GetSystemService(MainActivity.ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            if ((activeConnection != null) && activeConnection.IsConnected)
            {
                // Ok, carefully attempt to connect to the in-app service
                try
                {
                    var responses = MP.MpUtils.GetPurchaseHistory(Android.App.Application.Context, FortumoInAppService._serviceId, FortumoInAppService._inAppSecret, FortumoInAppService._timeOut);

                    if (responses.Count == 0)
                    {
                        this.FireOnRestoreProductsError(-1, null);
                    }
                    else
                    {
                        // Record what we purchased
                        bool foundProduct = false;
                        foreach (var response in responses)
                        {
                            var paymentResponse = response as PaymentResponse;

                            // Sanity checks
                            if (paymentResponse == null ||
                                paymentResponse.BillingStatus != MP.MpUtils.MessageStatusBilled)
                            {
                                continue;
                            }

                            if (paymentResponse.ServiceId == FortumoInAppService._serviceId)
                            {
                                // Ok to record payment
                                this.UpdatePayment(paymentResponse);
                                foundProduct = true;
                            }
                        }

                        // Notifiy anyone who needs to know outcome
                        if (foundProduct)
                        {
                            this.FireOnRestoreProducts();
                        }
                        else
                        {
                            this.FireOnRestoreProductsError(-1, null);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception in RestoreProducts: " + ex);
                    this.FireOnRestoreProductsError(-1, null);
                }
            }
            else
            {
                this.FireOnRestoreProductsError(-1, null);
            }
        }

        public void RefundProduct()
        {
            // No-op
        }

        public event OnQueryInventoryDelegate OnQueryInventory;

        public event OnPurchaseProductDelegate OnPurchaseProduct;

        public event OnRestoreProductsDelegate OnRestoreProducts;

        public event OnQueryInventoryErrorDelegate OnQueryInventoryError;

        public event OnPurchaseProductErrorDelegate OnPurchaseProductError;

        public event OnRestoreProductsErrorDelegate OnRestoreProductsError;

        public event OnUserCanceledDelegate OnUserCanceled;

        public event OnInAppBillingProcessingErrorDelegate OnInAppBillingProcesingError;

        public event OnInvalidOwnedItemsBundleReturnedDelegate OnInvalidOwnedItemsBundleReturned;

        public event OnPurchaseFailedValidationDelegate OnPurchaseFailedValidation;

        public void FireOnQueryInventory()
        {
            if (this.OnQueryInventory != null)
            {
                this.OnQueryInventory();
            }
        }

        public void FireOnQueryInventoryError(int responseCode, Bundle skuDetails)
        {
            if (this.OnQueryInventoryError != null)
            {
                this.OnQueryInventoryError(responseCode, null);
            }
        }

        public void FireOnPurchaseProduct(int response, InAppPurchase purchase, string purchaseData, string purchaseSignature)
        {
            if (this.OnPurchaseProduct != null)
            {
                this.OnPurchaseProduct();
            }
        }

        public void FireOnPurchaseProductError(int responseCode, string sku)
        {
            if (this.OnPurchaseProductError != null)
            {
                this.OnPurchaseProductError(responseCode, sku);
            }
        }

        public void FireOnRestoreProducts()
        {
            if (this.OnRestoreProducts != null)
            {
                this.OnRestoreProducts();
            }
        }

        public void FireOnRestoreProductsError(int responseCode, IDictionary<string, object> skuDetails)
        {
            if (this.OnRestoreProductsError != null)
            {
                this.OnRestoreProductsError(responseCode, skuDetails);
            }
        }

        public void FireOnOnInAppBillingProcesingError(string message)
        {
            if (this.OnInAppBillingProcesingError != null)
            {
                this.OnInAppBillingProcesingError(message);
            }
        }

        public void FireOnUserCanceled()
        {
            if (this.OnUserCanceled != null)
            {
                this.OnUserCanceled();
            }
        }

        public string SimsipToFortumoProductId(string simsipProductId)
        {
            string result = string.Empty;

            if (simsipProductId == this.PracticeModeProductId)
            {
                result = FortumoInAppService._serviceId;
            }

            return result;
        }

        public string FortumoToSimsipProductId(string fortumoProductId)
        {
            string result = string.Empty;

            if (fortumoProductId == FortumoInAppService._serviceId)
            {
                result = this.PracticeModeProductId;
            }

            return result;
        }

        public void UpdatePayment(PaymentResponse paymentResponse)
        {
            var simsipProductId = this.FortumoToSimsipProductId(paymentResponse.ServiceId);
            var newPurchase = new InAppPurchase
            {
                OrderId = paymentResponse.PaymentCode,
                ProductId = simsipProductId,
                PurchaseTime = DateTime.Now
            };
            App.ViewModel.Purchases.Add(newPurchase);
        }

        #endregion

    }
}