using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ConfirmButton : MonoBehaviour
{
    PlayerRequest playerRequest;
    [SerializeField] GameObject nicknameObject;
    [SerializeField] GameObject analytics;

    SetCountry sc;
    string myGoogleId;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerRequest = new PlayerRequest();
        myGoogleId = SystemInfo.deviceUniqueIdentifier;
        sc = new SetCountry();
        StartCoroutine(sc.SetCountryCode());
    }

    public void SaveData()
    {
        if (nicknameObject.GetComponent<TextMeshProUGUI>().text.Length < 3)
        {
            return;
        }

        PlayerMiniSerializer player = playerRequest.GetPlayerMiniData(myGoogleId);
        if (player != null)
        {
            if (sc.CountryCode != null && sc.CountryCode != "-")
            {
                playerRequest.SendDataToServer(nicknameObject.GetComponent<TextMeshProUGUI>().text, sc.CountryCode, player.score, myGoogleId);
                SaveSystem.SavePlayerInfo(false, nicknameObject.GetComponent<TextMeshProUGUI>().text, sc.CountryCode, player.score, myGoogleId);
            }
            else
            {
                playerRequest.SendDataToServer(nicknameObject.GetComponent<TextMeshProUGUI>().text, player.country, player.score, myGoogleId);
                SaveSystem.SavePlayerInfo(false, nicknameObject.GetComponent<TextMeshProUGUI>().text, player.country, player.score, myGoogleId);
            } 
        } else
        {
            if (sc.CountryCode != null)
            {
                playerRequest.SendDataToServer(nicknameObject.GetComponent<TextMeshProUGUI>().text, sc.CountryCode, 0, myGoogleId);
                SaveSystem.SavePlayerInfo(false, nicknameObject.GetComponent<TextMeshProUGUI>().text, sc.CountryCode, 0, myGoogleId);
            }
            else
            {
                playerRequest.SendDataToServer(nicknameObject.GetComponent<TextMeshProUGUI>().text, "-", 0, myGoogleId);
                SaveSystem.SavePlayerInfo(false, nicknameObject.GetComponent<TextMeshProUGUI>().text, "-", 0, myGoogleId);
            }
                
        }
        PlayerPrefs.SetString("needTutorial", "true");
        PlayerPrefs.SetInt("collisionsTimes", 0);
        analytics.GetComponent<FlappyAnalytics>().sendTimeChooseNickname(Time.time);
        SceneManager.LoadScene(0);
    }
}
