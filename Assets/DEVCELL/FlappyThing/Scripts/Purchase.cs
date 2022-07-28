using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Purchase : MonoBehaviour
{
    [SerializeField] GameObject noAdsButton;
    [SerializeField] GameObject iapManager;

    private bool activated;

    [SerializeField] GameObject debug;
    //IAPManager iapManager;

    void Start()
    {
        //iapManager = new IAPManager();
        if (PlayerPrefs.GetString("no_ads") == "true")
        {
            noAdsButton.SetActive(false);
            //debug.GetComponent<TextMeshProUGUI>().text += "no ads = " + PlayerPrefs.GetString("no_ads");
        }
    }

    public void RemoveAds()
    {
        Method();
    }

    private void Update()
    {
        //debug.GetComponent<TextMeshProUGUI>().text = iapManager.debug;
    }
    
    public void Method()
    {
        if (Time.time > 0.3f)
        {
            noAdsButton.SetActive(false);
        }
    }
}
