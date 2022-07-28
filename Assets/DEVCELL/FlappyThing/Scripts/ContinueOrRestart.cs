using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueOrRestart : MonoBehaviour
{
    [SerializeField] GameObject popupResurrect;
    [SerializeField] GameObject popupResurrectNoAds;
    [SerializeField] GameObject timerObj;
    [SerializeField] GameObject timerObjNoAds;
    [SerializeField] GameObject thing;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject ads;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject continueTimer;
    [SerializeField] GameObject adsLoading;

    GameObject popupResurrectUse;
    GameObject timerUse;
    double timer;
    double timerAdsLoading;
    int count;
    bool continuation = false;
    bool resurrected = false;
    float openDelay = 0;
    float endgameDelay = 1000;
    float adsLoadingDelay = 100;
    byte adsLoadingOpacity = 255;
    bool makeOpacity = true;

    public bool Continuation { get => continuation; set => continuation = value; }
    public bool Resurrected { get => resurrected; set => resurrected = value; }

    private void OnEnable()
    {
        pauseButton.GetComponent<Button>().interactable = false;
    }

    private void OnDisable()
    {
        //if (pauseButton)
        //pauseButton.GetComponent<Button>().interactable = true;
        if (adsLoading)
        {
            adsLoading.SetActive(false);
        }
        
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("no_ads") || PlayerPrefs.GetString("no_ads") == "false")
        {
            if (PlayerPrefs.GetInt("countCollisions") > 5)
            {
                adsLoading.SetActive(true);
            }
            popupResurrectUse = popupResurrect;
            timerUse = timerObj;
        }
        else
        {
            popupResurrectUse = popupResurrectNoAds;
            timerUse = timerObjNoAds;
        }

        popupResurrectUse.SetActive(true);
        Time.timeScale = 0;
        count = 6;
        timer = DateTime.Now.ToUniversalTime().Subtract(
                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                ).TotalMilliseconds;
        timerAdsLoading = DateTime.Now.ToUniversalTime().Subtract(
                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                ).TotalMilliseconds;
        timerUse.GetComponent<TextMeshProUGUI>().text = count.ToString();
        Resurrected = true;
    }

    void Update()
    {
        if (timerAdsLoading + adsLoadingDelay <= DateTime.Now.ToUniversalTime().Subtract(
                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                ).TotalMilliseconds)
        {
            if (adsLoading.activeSelf)
            {
                if (makeOpacity)
                {
                    adsLoadingOpacity -= 10;
                    if (adsLoadingOpacity <= 10)
                    {
                        makeOpacity = false;
                        adsLoadingOpacity = 0;
                    }
                }
                else
                {
                    adsLoadingOpacity += 10;
                    if (adsLoadingOpacity >= 245)
                    {
                        makeOpacity = true;
                        adsLoadingOpacity = 255;
                    }
                }
                adsLoading.GetComponent<TextMeshProUGUI>().faceColor = new Color32(255, 255, 255, adsLoadingOpacity);
            }
            timerAdsLoading = DateTime.Now.ToUniversalTime().Subtract(
                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                ).TotalMilliseconds;
        }

        if (timer + endgameDelay <= DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds)
        {
            count -= 1;
            timerUse.GetComponent<TextMeshProUGUI>().text = count.ToString();
            timer = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        if (count == 0)
        {
            Time.timeScale = 1;
            popupResurrectUse.SetActive(false);
            count = 6;
            SceneManager.LoadScene(2);
        }

    }

    public void Continue()
    {
        Continuation = true;
        var myThing = Instantiate(thing);
        myThing.SetActive(true);
        myThing.GetComponent<Thing>().God = true;

        popupResurrectUse.SetActive(false);

        if (!PlayerPrefs.HasKey("no_ads") || PlayerPrefs.GetString("no_ads") == "false")
        {
            if (ads.GetComponent<AdsManager>().AdsIsInit)
            {
                ads.GetComponent<AdsManager>().ShowAd();
            } 
            else
            {
                continueTimer.SetActive(true);
            }
        }
        else
        {
            continueTimer.SetActive(true);
        }
        pause.GetComponent<PauseContinue>().PopupDisactive();
        gameObject.SetActive(false);

    }

    public void Close()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        SceneManager.LoadScene(2);
    }
}
