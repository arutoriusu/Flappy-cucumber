using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    private SetCountry sc;

    public SetCountry Sc { get => sc; set => sc = value; }

    private void OnEnable()
    {
        PlayerInfo pi = SaveSystem.LoadPlayerInfo();
        if (pi == null)
        {
            SceneManager.LoadScene(3);
        }
        Sc = new SetCountry();
        StartCoroutine(Sc.SetCountryCode());

        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetString("sound", "true");
        }

        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetString("music", "true");
        }

        PlayerPrefs.SetInt("countCollisions", 0);
    }
}
