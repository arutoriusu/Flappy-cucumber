using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    PlayerRequest pr = new PlayerRequest();
    public static int score = 0;
    public static bool isFirstStarSpawned = false;

    [SerializeField] GameObject scoreText;
    [SerializeField] GameObject tutorial;

    void Start()
    {
        pr = new PlayerRequest();
        InvokeRepeating("IncrementScore", 5.5f, 2.8f);
        RefreshScoreText();
    }

    void Update()
    {
        RefreshScoreText();
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerInfo pi = SaveSystem.LoadPlayerInfo();
        int highScore = 0;

        if (pi == null)
        {
            highScore = pi.highScore;
        }

        if (score > highScore)
        {
            SaveSystem.SavePlayerInfo(pi.firstStart, pi.username, pi.country, score, pi.googleId);
            if (score > 9)
            {
                pr.SendDataToServer(score);
            }
        }
    }

    public void RefreshScoreText()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    void IncrementScore()
    {
        score += 1;
        if (score > 5)
        {
            tutorial.SetActive(false);
        }
    }
}
