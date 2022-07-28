using Microsoft.Win32;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEnd : MonoBehaviour
{
    private int yourScore;
    private int yourHighScore;
    private string yourCountry;
    private string yourName;
    private string googleId;
    private string yourPosition;
    private PlayerRequest pr;

    [SerializeField] GameObject yourPositionObject;
    [SerializeField] GameObject yourCountryObject;
    [SerializeField] GameObject yourHighScoreObject;
    [SerializeField] GameObject yourNameObject;
    [SerializeField] GameObject review;

    [SerializeField] GameObject scoresControl;

    [SerializeField] AdsManager adsManager;

    void Start()
    {
        pr = new PlayerRequest();
        PlayerInfo pi = SaveSystem.LoadPlayerInfo();
        yourName = pi.username;
        yourCountry = pi.country;
        googleId = pi.googleId;
        PlayerMiniSerializer miniData = pr.GetPlayerMiniData(googleId);

        if (PlayerPrefs.HasKey("player_country"))
        {
            yourCountry = PlayerPrefs.GetString("player_country");
        }

        if (miniData != null)
        {
            yourPosition = miniData.position;
            yourHighScore = miniData.score;
        } else
        {
            yourPosition = "-";
            yourHighScore = pi.highScore;
        }

        SaveSystem.SavePlayerInfo(pi.firstStart, yourName, yourCountry, yourHighScore, googleId);

        GetScores();

        int countCollisions = PlayerPrefs.GetInt("countCollisions");

        if (countCollisions > 5)
        {
            int collisionsTimes = PlayerPrefs.GetInt("collisionsTimes");
            PlayerPrefs.SetInt("collisionsTimes", collisionsTimes+1);

            PlayerPrefs.SetString("needTutorial", "false");
            countCollisions = 0;
            PlayerPrefs.SetInt("countCollisions", 0);
            if (!PlayerPrefs.HasKey("no_ads") || PlayerPrefs.GetString("no_ads") == "false")
            {
                adsManager.GetComponent<AdsManager>().ShowAd();
            }
        }
        if (PlayerPrefs.GetInt("collisionsTimes") == 1 && countCollisions == 4)
        {
            review.SetActive(true);
        }

    }

    void OnEnable()
    {
        yourScore = PlayerPrefs.GetInt("score");
    }

    void GetScores()
    {
        PlayerListSerializer playersData = pr.GetPlayersScoresWorld();

        if (playersData != null)
        {
            scoresControl.GetComponent<ScoresControl>().SetScores(playersData);
        }

        Texture countryTexture = getCountryTexture();
        if (countryTexture != null)
        {
            yourCountryObject.transform.GetChild(1).GetComponent<RawImage>().texture = countryTexture;
        }

        yourHighScoreObject.GetComponent<TextMeshProUGUI>().text = yourScore.ToString() + " / " + yourHighScore.ToString();
        yourNameObject.GetComponent<TextMeshProUGUI>().text = yourName.ToString();
        yourPositionObject.GetComponent<TextMeshProUGUI>().text = yourPosition.ToString();
    }

    private Texture getCountryTexture()
    {
        Texture countryTexture = null;
        try
        {
            countryTexture = Resources.Load<Texture>("Icons/Flags/" + yourCountry.ToLower());
        }
        catch
        {
            countryTexture = null;
        }
        

        if (countryTexture)
        {
            yourCountryObject.transform.GetChild(1).GetComponent<RawImage>().texture = countryTexture;
            return countryTexture;
        }
        else
        {
            yourCountryObject.transform.GetChild(1).gameObject.SetActive(false);
            yourCountryObject.transform.GetChild(0).gameObject.SetActive(true);
            return null;
        }
    }
}
