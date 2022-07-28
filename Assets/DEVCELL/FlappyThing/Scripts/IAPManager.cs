using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private IStoreController controller;
    private IExtensionProvider extensions;

    public string debug;

    private void Start ()
    {
        debug += " IAPManager \n";
        Debug.Log("Iap manager constructor");
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct("remove_ads3", ProductType.NonConsumable, new IDs
        {
            {"remove_ads3", GooglePlay.Name},
        });

        UnityPurchasing.Initialize(this, builder);
        DontDestroyOnLoad(gameObject);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        debug += " OnInitializeFailed \n";
        Debug.Log("OnInitializeFailed");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        Debug.Log("Iap manager ProcessPurchase");
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        debug += " OnPurchaseFailed \n";
        Debug.Log("OnPurchaseFailed");
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("Iap manager oninitialized");
        this.controller = controller;
        this.extensions = extensions;

        Product product = controller.products.WithID("remove_ads3");

        if (product != null && product.hasReceipt && product.transactionID != null)
        {
            debug += "yes\n";
            Debug.Log(" 3 - " + PlayerPrefs.GetString("no_ads"));
            PlayerPrefs.SetString("no_ads", "true");
            Debug.Log(" 4 - " + PlayerPrefs.GetString("no_ads"));
        }
        else
        {
            debug += "no\n";
            PlayerPrefs.SetString("no_ads", "false");
        }
    }
}
