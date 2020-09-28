using System.Collections.Generic;
using InApp.Models;

namespace InApp.Services
{
    public delegate void OnQueryInventoryDelegate();

    public delegate void OnPurchaseProductDelegate();

    public delegate void OnRestoreProductsDelegate();

    public delegate void OnQueryInventoryErrorDelegate(int responseCode, IDictionary<string, object> skuDetails);

    public delegate void OnPurchaseProductErrorDelegate(int responseCode, string sku);

    public delegate void OnRestoreProductsErrorDelegate(int responseCode, IDictionary<string, object> skuDetails);

    public delegate void OnUserCanceledDelegate();

    public delegate void OnInAppBillingProcessingErrorDelegate(string message);

    public delegate void OnInvalidOwnedItemsBundleReturnedDelegate(IDictionary<string, object> ownedItems);

    public delegate void OnPurchaseFailedValidationDelegate(InAppPurchase purchase, string purchaseData, string purchaseSignature);

    public interface IFortumoInAppService
    {
        string PracticeModeProductId { get; }

        /// <summary>
        /// Starts the setup of this Android application by connection to the Google Play Service
        /// to handle In-App purchases.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Queries the inventory asynchronously and returns a list of Xamarin.Android.InAppBilling.Products
        /// matching the given list of SKU numbers.
        /// </summary>
        /// <param name="skuList">Sku list.</param>
        /// <param name="itemType">The Xamarin.Android.InAppBilling.ItemType of product being queried.</param>
        /// <returns>List of Xamarin.Android.InAppBilling.Products matching the given list of SKUs.
        /// </returns>
        void QueryInventory();

        /// <summary>
        /// Buys the given Xamarin.Android.InAppBilling.Product
        /// 
        /// This method automatically generates a unique GUID and attaches it as the
        /// developer payload for this purchase.
        /// </summary>
        /// <param name="product">The Xamarin.Android.InAppBilling.Product representing the item the users
        /// wants to purchase.</param>
        void PurchaseProduct(string productId);

        void RestoreProducts();

        /// <summary>
        /// For testing purposes only.
        /// </summary>
        void RefundProduct();

        /// <summary>
        /// Occurs when a query inventory transactions completes successfully with Google Play Services.
        /// </summary>
        event OnQueryInventoryDelegate OnQueryInventory;

        /// <summary>
        /// Occurs after a product has been successfully purchased Google Play.
        /// 
        /// This event is fired after a OnProductPurchased which is raised when the user
        /// successfully logs an intent to purchase with Google Play.
        /// </summary>
        event OnPurchaseProductDelegate OnPurchaseProduct;

        /// <summary>
        /// Occurs after a successful products restored transactions with Google Play.
        /// </summary>
        event OnRestoreProductsDelegate OnRestoreProducts;

        /// <summary>
        /// Occurs when there is an error querying inventory from Google Play Services.
        /// </summary>
        event OnQueryInventoryErrorDelegate OnQueryInventoryError;

        /// <summary>
        /// Occurs when the user attempts to buy a product and there is an error.
        /// </summary>
        event OnPurchaseProductErrorDelegate OnPurchaseProductError;

        /// <summary>
        /// Occurs when the user attempts to restore products and there is an error.
        /// </summary>
        event OnRestoreProductsErrorDelegate OnRestoreProductsError;

        /// <summary>
        /// Occurs when on user canceled.
        /// </summary>
        event OnUserCanceledDelegate OnUserCanceled;

        /// <summary>
        /// Occurs when there is an in app billing procesing error.
        /// </summary>
        event OnInAppBillingProcessingErrorDelegate OnInAppBillingProcesingError;

        /// <summary>
        /// Raised when Google Play Services returns an invalid bundle from previously
        /// purchased items
        /// </summary>
        event OnInvalidOwnedItemsBundleReturnedDelegate OnInvalidOwnedItemsBundleReturned;

        /// <summary>
        /// Occurs when a previously purchased product fails to validate.
        /// </summary>
        event OnPurchaseFailedValidationDelegate OnPurchaseFailedValidation;
    }
}
