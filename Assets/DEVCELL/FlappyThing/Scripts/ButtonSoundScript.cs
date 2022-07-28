using UnityEngine;

public class ButtonSoundScript : MonoBehaviour
{
    [SerializeField] GameObject flappyAnalytics;

    private void Start ()
    {
        if (PlayerPrefs.GetString("sound") == "true")
        {
            
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        } else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void Sound()
    {
        if (PlayerPrefs.GetString("sound") == "true")
        {
            flappyAnalytics.GetComponent<FlappyAnalytics>().onDisableSound();
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            PlayerPrefs.SetString("sound", "false");
        } else
        {
            flappyAnalytics.GetComponent<FlappyAnalytics>().onEnableSound();
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            PlayerPrefs.SetString("sound", "true");
        }
    }
}
