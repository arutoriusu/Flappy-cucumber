using UnityEngine;

public class ButtonMusicScript : MonoBehaviour
{
    [SerializeField] GameObject flappyAnalytics;

    private void Start ()
    {
        if (PlayerPrefs.GetString("music") == "true")
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        } else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void Music()
    {
        if (PlayerPrefs.GetString("music") == "true")
        {
            flappyAnalytics.GetComponent<FlappyAnalytics>().onDisableMusic();
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            PlayerPrefs.SetString("music", "false");
        }
        else
        {
            flappyAnalytics.GetComponent<FlappyAnalytics>().onEnableMusic();
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            PlayerPrefs.SetString("music", "true");
        }
    }
}
