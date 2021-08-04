using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
//using Google.Play.Billing;

// Placing the Purchaser class in the CompleteProject namespace allows it to interact with ScoreManager, 
// one of the existing Survival Shooter scripts.

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class IAPManager : MonoBehaviour, IStoreListener
{
    IGooglePlayStoreExtensions IgoogleplayStoreExtention;
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
    public static IAPManager Instance { set; get; }

    UnityEngine.Purchasing.IGooglePlayStoreExtensions m_GooglePlayStoreExtensions;
    // Product identifiers for all products capable of being purchased: 
    // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
    // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
    // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

    // General product identifiers for the consumable, non-consumable, and subscription products.
    // Use these handles in the code to reference which product to purchase. Also use these values 
    // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
    // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
    // specific mapping to Unity Purchasing's AddProduct, below.
    public static string kProductIDConsumable = "consumable";
    public static string kProductIDNonConsumable = "nonconsumable";

    public static string _3MonthsSubscription = "subscription3months";
    public static string _6MonthsSubscription = "newsubscription6months";
    public static string _1YearSubscription = "subscription1year";

    // Apple App Store-specific product identifier for the subscription product.
    private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

    // Google Play Store-specific product identifier subscription product.
    private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

    private void Awake()
    {
        Instance = this;
        //  m_GooglePlayStoreExtensions.

    }

    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        ConfigurationBuilder builder;

        builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());


        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.
        builder.AddProduct(kProductIDConsumable, ProductType.Consumable);
        // Continue adding the non-consumable product.
        builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);
        // And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
        // if the Product ID was configured differently between Apple and Google stores. Also note that
        // one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
        // must only be referenced here. 

        /*
         builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
                  { kProductNameAppleSubscription, AppleAppStore.Name },
                  { kProductNameGooglePlaySubscription, GooglePlay.Name },
              });
              
         */


        builder.AddProduct(_3MonthsSubscription, ProductType.Subscription);
        builder.AddProduct(_6MonthsSubscription, ProductType.Subscription);
        builder.AddProduct(_1YearSubscription, ProductType.Subscription);

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void BuyConsumable()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(kProductIDConsumable);
    }


    public void BuyNonConsumable()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(kProductIDNonConsumable);
    }


    /* 
     public void BuySubscription()
     {
         // Buy the subscription product using its the general identifier. Expect a response either 
         // through ProcessPurchase or OnPurchaseFailed asynchronously.
         // Notice how we use the general product identifier in spite of this ID being mapped to
         // custom store-specific identifiers above.
         BuyProductID(kProductIDSubscription);
     }
     */

    public void Buy3MonthsSubscription()
    {
        print("trying to buy 3 months of subscription");

        bool alreadyHaveSomeSubscription = false;
        Product oldProduct = m_StoreController.products.WithID(_3MonthsSubscription);
        // Buy the subscription product using its the general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        // Notice how we use the general product identifier in spite of this ID being mapped to
        // custom store-specific identifiers above.

        foreach (Product p in m_StoreController.products.all)
        {
            GooglePurchaseData data = new GooglePurchaseData(p.receipt);

            if (p.hasReceipt)
            {
                alreadyHaveSomeSubscription = true;
                oldProduct = m_StoreController.products.WithID(data.json.productId);
                GooglePurchaseData data2 = new GooglePurchaseData(oldProduct.receipt);

                print("Has the reciept of----->" + data2.json.productId);
                break;
            }
        }

        if (!alreadyHaveSomeSubscription)
        {
            print("no previous subscription");
            BuyProductID(_3MonthsSubscription);
        }
        else
        {
            print("upgrading to 3 months");
            Product newProduct = m_StoreController.products.WithID(_3MonthsSubscription);

            SubscriptionManager.UpdateSubscriptionInGooglePlayStore(oldProduct, m_StoreController.products.WithID(_3MonthsSubscription), (productInfos, newProductId) =>
            {
                m_GooglePlayStoreExtensions.UpgradeDowngradeSubscription(oldProduct.definition.id, newProduct.definition.id, 5);



            });

        }


    }
    public void Buy6MonthsSubscription()
    {
        bool alreadyHaveSomeSubscription = false;
        Product oldProduct = m_StoreController.products.WithID(_6MonthsSubscription);
        print("trying to buy 6 months of subscription");




        foreach (Product p in m_StoreController.products.all)
        {
            GooglePurchaseData data = new GooglePurchaseData(p.receipt);

            if (p.hasReceipt)
            {
                alreadyHaveSomeSubscription = true;
                oldProduct = m_StoreController.products.WithID(data.json.productId);
                GooglePurchaseData data2 = new GooglePurchaseData(oldProduct.receipt);

                print("Has the reciept of----->" + data2.json.productId);
                break;
            }
        }



        // Buy the subscription product using its the general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        // Notice how we use the general product identifier in spite of this ID being mapped to
        // custom store-specific identifiers above.
        if (!alreadyHaveSomeSubscription)
        {
            print("no previous subscription");
            BuyProductID(_6MonthsSubscription);
        }
        else
        {
            Product newProduct = m_StoreController.products.WithID(_6MonthsSubscription);
            print("upgrading to 6 months");
            SubscriptionManager.UpdateSubscriptionInGooglePlayStore(oldProduct, m_StoreController.products.WithID(_6MonthsSubscription), (productInfos, newProductId) =>
            {
                m_GooglePlayStoreExtensions.UpgradeDowngradeSubscription(oldProduct.definition.id, newProduct.definition.id, 5);
                

            });
        }
    }
    public void Buy1YearSubscription()
    {

        print("trying to buy 12 months of subscription");




        bool alreadyHaveSomeSubscription = false;
        Product oldProduct = m_StoreController.products.WithID(_1YearSubscription);


        foreach (Product p in m_StoreController.products.all)
        {
            GooglePurchaseData data = new GooglePurchaseData(p.receipt);

            if (p.hasReceipt)
            {
                alreadyHaveSomeSubscription = true;
                oldProduct = m_StoreController.products.WithID(data.json.productId);
                GooglePurchaseData data2 = new GooglePurchaseData(oldProduct.receipt);

                print("Has the reciept of----->" + data2.json.productId);

                break;
            }
        }
        // Buy the subscription product using its the general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        // Notice how we use the general product identifier in spite of this ID being mapped to
        // custom store-specific identifiers above
        if (!alreadyHaveSomeSubscription)
        {
            print("no previous subscription");
            BuyProductID(_1YearSubscription);
        }
        else
        {

            print("upgrading to 1year");
            /*
            SubscriptionManager.UpdateSubscriptionInGooglePlayStore( oldProduct, m_StoreController.products.WithID(_1YearSubscription), (string msg1, string msg2) =>
            {

                IgoogleplayStoreExtention.UpdateSubscription(oldProduct, m_StoreController.products.WithID(_1YearSubscription), GooglePlayStoreProrationMode.ImmediateWithoutProration);
            });
            */


            Product newProduct = m_StoreController.products.WithID(_1YearSubscription);

            SubscriptionManager.UpdateSubscriptionInGooglePlayStore(oldProduct, m_StoreController.products.WithID(_1YearSubscription),
                (productInfos, newProductId) =>
                {
                    m_GooglePlayStoreExtensions.UpgradeDowngradeSubscription(oldProduct.definition.id, newProduct.definition.id,5);
                });


        }
    }

    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) =>
            {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
        // IgoogleplayStoreExtention.GetExtension<IGooglePlayStoreExtensions>();

        m_GooglePlayStoreExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// ScoreManager.score += 100;
        }
        // Or ... a non-consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
        }
        // Or ... a subscription product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, _3MonthsSubscription, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The subscription item has been successfully purchased, grant this to the player.
        }
        else if (String.Equals(args.purchasedProduct.definition.id, _6MonthsSubscription, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The subscription item has been successfully purchased, grant this to the player.
        }
        else if (String.Equals(args.purchasedProduct.definition.id, _1YearSubscription, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The subscription item has been successfully purchased, grant this to the player.
        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public void Checker()
    {


        SubscriptionInfo _3MonthsSubscriptionInfo = new SubscriptionInfo(_3MonthsSubscription);
        SubscriptionInfo _6MonthsSubscriptionInfo = new SubscriptionInfo(_6MonthsSubscription);
        SubscriptionInfo _1YearSubscriptionInfo = new SubscriptionInfo(_1YearSubscription);
        if (_1YearSubscriptionInfo.isSubscribed() == Result.True)
        {

            print("subscribed to 12 months");
            print(_1YearSubscriptionInfo.getExpireDate() + "_1YearSubscriptionInfo.getExpireDate()");
            print(_1YearSubscriptionInfo.getRemainingTime() + "_1YearSubscriptionInfo.getRemainingTime()");
            print(_1YearSubscriptionInfo.isFreeTrial() + "isfree trial");
            print(_1YearSubscriptionInfo.isCancelled() + "_1YearSubscriptionInfo.isCancelled()");
            print(_1YearSubscriptionInfo.getPurchaseDate() + "_1YearSubscriptionInfo.getPurchaseDate()");

            print(_3MonthsSubscriptionInfo.isSubscribed() + "_3MonthsSubscriptionInfo.isSubscribed()");
            print(_6MonthsSubscriptionInfo.isSubscribed() + "_6MonthsSubscriptionInfo.isSubscribed()");
            print(_1YearSubscriptionInfo.isSubscribed() + "_1YearSubscriptionInfo.isSubscribed()");

        }
        else
        {
            print("not subscribed12");
            print(_3MonthsSubscriptionInfo.isSubscribed() + "_3MonthsSubscriptionInfo.isSubscribed()");
            print(_6MonthsSubscriptionInfo.isSubscribed() + "_6MonthsSubscriptionInfo.isSubscribed()");
            print(_1YearSubscriptionInfo.isSubscribed() + "_1YearSubscriptionInfo.isSubscribed()");

        }
        if (_6MonthsSubscriptionInfo.isSubscribed() == Result.True)
        {

            print("subscribed to 6 months");
            print(_6MonthsSubscriptionInfo.getExpireDate() + "_6MonthsSubscriptionInfo.getExpireDate()");
            print(_6MonthsSubscriptionInfo.getRemainingTime() + "_6MonthsSubscriptionInfo.getRemainingTime()");
            print(_6MonthsSubscriptionInfo.isFreeTrial() + "isfree trial");
            print(_6MonthsSubscriptionInfo.isCancelled() + "_6MonthsSubscriptionInfo.isCancelled()");
            print(_6MonthsSubscriptionInfo.getPurchaseDate() + "_6MonthsSubscriptionInfo.getPurchaseDate()");



        }
        else
        {
            print("not subscribed6");
            print(_3MonthsSubscriptionInfo.isSubscribed() + "_3MonthsSubscriptionInfo.isSubscribed()");
            print(_6MonthsSubscriptionInfo.isSubscribed() + "_6MonthsSubscriptionInfo.isSubscribed()");
            print(_1YearSubscriptionInfo.isSubscribed() + "_1YearSubscriptionInfo.isSubscribed()");

        }
        if (_3MonthsSubscriptionInfo.isSubscribed() == Result.True)
        {
            print("subscribed to 3 months");
            print(_3MonthsSubscriptionInfo.getExpireDate() + "_3MonthsSubscriptionInfo.getExpireDate()");
            print(_3MonthsSubscriptionInfo.getRemainingTime() + "_3MonthsSubscriptionInfo.getRemainingTime()");
            print(_3MonthsSubscriptionInfo.isFreeTrial() + "isfree trial");
            print(_3MonthsSubscriptionInfo.isCancelled() + "_3MonthsSubscriptionInfo.isCancelled()");
            print(_3MonthsSubscriptionInfo.getPurchaseDate() + "_3MonthsSubscriptionInfo.getPurchaseDate()");

        }
        else
        {
            print("not subscribed3");
            print(_3MonthsSubscriptionInfo.isSubscribed() + "_3MonthsSubscriptionInfo.isSubscribed()");
            print(_6MonthsSubscriptionInfo.isSubscribed() + "_6MonthsSubscriptionInfo.isSubscribed()");
            print(_1YearSubscriptionInfo.isSubscribed() + "_1YearSubscriptionInfo.isSubscribed()");

        }
    }

    public void RecipetInfo()
    {
        foreach (Product p in m_StoreController.products.all)
        {

            if (p.hasReceipt)
            {
                GooglePurchaseData data = new GooglePurchaseData(p.receipt);

                Debug.Log(data.json.autoRenewing);
                Debug.Log(data.json.orderId);
                Debug.Log(data.json.packageName);
                Debug.Log(data.json.productId);
                Debug.Log(data.json.purchaseTime);
                Debug.Log(data.json.purchaseState);
                Debug.Log(data.json.purchaseToken);

            }
        }

    }




    private void Update()
    {
        //  RecipetInfo();
        //Checker();
    }

    class GooglePurchaseData
    {
        // INAPP_PURCHASE_DATA
        public string inAppPurchaseData;
        // INAPP_DATA_SIGNATURE
        public string inAppDataSignature;

        public GooglePurchaseJson json;

        [System.Serializable]
        private struct GooglePurchaseReceipt
        {
            public string Payload;
        }

        [System.Serializable]
        private struct GooglePurchasePayload
        {
            public string json;
            public string signature;
        }

        [System.Serializable]
        public struct GooglePurchaseJson
        {
            public string autoRenewing;
            public string orderId;
            public string packageName;
            public string productId;
            public string purchaseTime;
            public string purchaseState;
            public string developerPayload;
            public string purchaseToken;
        }

        public GooglePurchaseData(string receipt)
        {
            try
            {
                var purchaseReceipt = JsonUtility.FromJson<GooglePurchaseReceipt>(receipt);
                var purchasePayload = JsonUtility.FromJson<GooglePurchasePayload>(purchaseReceipt.Payload);
                var inAppJsonData = JsonUtility.FromJson<GooglePurchaseJson>(purchasePayload.json);

                inAppPurchaseData = purchasePayload.json;
                inAppDataSignature = purchasePayload.signature;
                json = inAppJsonData;
            }
            catch
            {
                Debug.Log("Could not parse receipt: " + receipt);
                inAppPurchaseData = "";
                inAppDataSignature = "";
            }
        }
    }


    public bool CheckIfUserIsSubscribed()
    {
        bool haveSubscription = false;
        foreach (Product p in m_StoreController.products.all)
        {
            if (p.hasReceipt)
            {
                haveSubscription = true;
            }
        }

        if (haveSubscription)
            return true;
        else
            return false;
    }

    public (string, string, int) getSubscriptionEpochTimeAndPlan()
    {
        foreach (Product p in m_StoreController.products.all)
        {
            if (p.hasReceipt)
            {

                GooglePurchaseData data = new GooglePurchaseData(p.receipt);
                switch (data.json.productId)
                {
                    case "subscription3months":
                        return (data.json.purchaseTime, "3 months", 3);
                    case "newsubscription6months":
                        return (data.json.purchaseTime, "6 months", 6);
                    case "subscription1year":
                        return (data.json.purchaseTime, "1 year", 12);

                }

            }
        }
        return ("not subscribed", "0 months", 0);

    }






    public (string, string, string, string, string, string, string) GetLocalizedPrice()
    {
        string _3MonthsPrice = "", _6MonthsPrice = "", _12MonthsPrice = "", _3MonthsPricePerMonth = "", _6MonthsPricePerMonth = "", _12MonthsPricePerMonth = "", isoCurrencyCode = "";

        foreach (var product in m_StoreController.products.all)
        {
            Debug.Log(product.metadata.localizedTitle);
            Debug.Log(product.metadata.localizedDescription);
            Debug.Log(product.metadata.localizedPriceString);
            print(product.metadata.isoCurrencyCode);
            if (String.Compare(product.definition.id, _3MonthsSubscription) == 0)
            {
                _3MonthsPrice = product.metadata.localizedPriceString;
                _3MonthsPricePerMonth = (product.metadata.localizedPrice / 3).ToString("F2");
                _3MonthsPrice = product.metadata.localizedPriceString;

            }
            if (String.Compare(product.definition.id, _6MonthsSubscription) == 0)
            {
                _6MonthsPrice = product.metadata.localizedPriceString;
                _6MonthsPricePerMonth = (product.metadata.localizedPrice / 6).ToString("F2");
                _6MonthsPrice = product.metadata.localizedPriceString;


            }
            if (String.Compare(product.definition.id, _1YearSubscription) == 0)
            {
                _12MonthsPrice = product.metadata.localizedPriceString;
                _12MonthsPricePerMonth = (product.metadata.localizedPrice / 12).ToString("F2");
                _12MonthsPrice = product.metadata.localizedPriceString;
            }

            isoCurrencyCode = product.metadata.isoCurrencyCode;

        }







        return (_3MonthsPrice, _6MonthsPrice, _12MonthsPrice, _3MonthsPricePerMonth, _6MonthsPricePerMonth, _12MonthsPricePerMonth, isoCurrencyCode);
    }


}
