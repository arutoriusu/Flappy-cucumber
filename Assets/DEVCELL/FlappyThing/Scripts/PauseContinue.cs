using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseContinue : MonoBehaviour
{
    [SerializeField] GameObject popup;
    [SerializeField] GameObject continueTimer;
    [SerializeField] GameObject ads;
    [SerializeField] GameObject music;

    private bool continuation = false;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public bool Continuation { get => continuation; set => continuation = value; }

    public void MakePause()
    {
        music.SetActive(false);
        continueTimer.SetActive(false);
        popup.SetActive(true);
        Time.timeScale = 0;
    }

    public void PopupDisactive()
    {
        popup.SetActive(false);
    }

    public void MakeContinue()
    {
        PopupDisactive();
        continueTimer.SetActive(true);
        Continuation = true;
        //ads.GetComponent<AdsManager>().AdsShowed = false;
    }

    private void Update()
    {
        if (ads.GetComponent<AdsManager>().AdsShowed || 
            ads.GetComponent<AdsManager>().AdsFailed)
        {
            continueTimer.SetActive(true);
            Continuation = true;
            ads.GetComponent<AdsManager>().AdsShowed = false;
            ads.GetComponent<AdsManager>().AdsFailed = false;
        }
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
