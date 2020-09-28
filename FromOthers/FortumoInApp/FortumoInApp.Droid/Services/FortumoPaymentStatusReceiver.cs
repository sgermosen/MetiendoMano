using Android.App;
using Android.Content;
using MP;
using System;



namespace InApp.Droid.Services
{
    [BroadcastReceiver(Permission = "com.yourcompany.permission.PAYMENT_BROADCAST_PERMISSION")]
    [IntentFilter(new[] { "mp.info.PAYMENT_STATUS_CHANGED" })]
    public class PaymentStatusReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent data)
        {
            // TODO: What is this for?
            if(data == null) 
            {
				return;
			}

            // To be safe we create a local instance of InappSevice as we are not sure
            // what is avialable when this is called. For instance, app might not be running
            // when this is called.
            var inAppService = new FortumoInAppService();

            try
            {
                // TODO: can we use PaymentResponse(Intent)?
                var paymentResponse = new PaymentResponse(data);

                /*
                Bundle extras = intent.Extras;

                int billingStatus = extras.GetInt("billing_status");
                var creditAmount = extras.GetString("credit_amount");
                var creditName = extras.GetString("credit_name");
                var messageId = extras.GetString("message_id");
                var paymentCode = extras.GetString("payment_code");
                var priceAmount = extras.GetString("price_amount");
                var priceCurrency = extras.GetString("price_currency");
                var productName = extras.GetString("product_name");
                var serviceId = extras.GetString("service_id");
                var userId = extras.GetString("user_id");

                 var simsipProductId = InappService.SharedInstance.FortumoToSimsipProductId(serviceId);
                 */

                switch (paymentResponse.BillingStatus)
                {
                    case MpUtils.MessageStatusBilled:
                        {
                            // Record what we purchased
                            inAppService.UpdatePayment(paymentResponse);

                            /*
                            var inAppPurchaseRepository = new InAppPurchaseRepository();

                            var existingPurchase = inAppPurchaseRepository.GetPurchaseByProductId(simsipProductId);

                            if (existingPurchase == null)
                            {
                                var newPurchase = new InAppPurchaseEntity
                                {
                                    OrderId = paymentCode,
                                    ProductId = simsipProductId,
                                    PurchaseTime = DateTime.Now
                                };

                                inAppPurchaseRepository.Create(newPurchase);
                            }
                            else
                            {
                                existingPurchase.OrderId = paymentCode;
                                existingPurchase.ProductId = simsipProductId;
                                existingPurchase.PurchaseTime = DateTime.Now;

                                inAppPurchaseRepository.Update(existingPurchase);
                            }
                            */

                            // Let anyone know who is interested that purchase has completed
                            inAppService.FireOnPurchaseProduct(paymentResponse.BillingStatus, null, string.Empty, string.Empty);

                            break;
                        }
                    case MpUtils.MessageStatusNotSent:
                    case MpUtils.MessageStatusFailed:
                        {
                            inAppService.FireOnPurchaseProductError(paymentResponse.BillingStatus, string.Empty);
                            break;
                        }
                    case MpUtils.MessageStatusPending:
                        {
                            break;
                        }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception in OnReceive: " + ex);
                inAppService.FireOnPurchaseProductError(-1, string.Empty);
            }
        }
    }
}
