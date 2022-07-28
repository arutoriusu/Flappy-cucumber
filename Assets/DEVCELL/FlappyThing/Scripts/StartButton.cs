using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] GameObject startScript;

    public void OpenGameScene()
    {
        string countryCode = startScript.GetComponent<StartScript>().Sc.CountryCode;
        if (countryCode != null && countryCode != "-")
            PlayerPrefs.SetString("player_country", countryCode);
        Score.score = 0;
        SceneManager.LoadScene(1);
    }
}
