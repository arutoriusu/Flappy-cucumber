using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private bool adsShowed = false;
    private bool adsFailed = false;
    private bool adsIsInit = false;
    private bool adsIsFinish = false;

    [SerializeField] GameObject continueOrRestart;
    [SerializeField] GameObject continueTimer;
    [SerializeField] GameObject pause;

    public bool AdsShowed { get => adsShowed; set => adsShowed = value; }
    public bool AdsFailed { get => adsFailed; set => adsFailed = value; }
    public bool AdsIsInit { get => adsIsInit; set => adsIsInit = value; }
    public bool AdsIsFinish { get => adsIsFinish; set => adsIsFinish = value; }

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("4108117", false);
        
    }

    private void Update()
    {
        if (Advertisement.isInitialized)
        {
            AdsIsInit = true;
        }
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady("Interstitial_Android"))
        {
            var options = new ShowOptions { resultCallback = HandleAdsResult };
            Advertisement.Show("Interstitial_Android", options);
        }
    }

    private void HandleAdsResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                //Debug.Log("ads finished!");
                AdsShowed = true;
                break;
        }
        
    }

    public void OnUnityAdsDidError(string message)
    {
        AdsFailed = true;
        //debug.GetComponent<TextMeshProUGUI>().text = "ads failed";
    }

    public void OnUnityAdsDidStart(string placementId)
    {
       
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        AdsIsFinish = true;
    }
}
