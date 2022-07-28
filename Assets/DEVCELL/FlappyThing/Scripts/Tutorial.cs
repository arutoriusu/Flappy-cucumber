using System;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject touchVFX;
    [SerializeField] GameObject tapToFly;
    [SerializeField] GameObject arrowLeft;
    [SerializeField] GameObject arrowRight;
    [SerializeField] GameObject lines;

    private Vector3 fingerLeftPos;
    private Vector3 fingerRightPos;
    private float tutorialBlinkDelay;
    private float tutorialBlink;

    private void OnEnable()
    {
        if (PlayerPrefs.GetString("needTutorial") == "false")
        {
            gameObject.SetActive(false);
        }
    }

    void Start ()
    {
        tutorialBlinkDelay = 0.5f;
        tutorialBlink = Time.time;

        tapToFly.SetActive(true);
        arrowLeft.SetActive(true);
        arrowRight.SetActive(true);
        lines.SetActive(true);

        /*if (GLOBAL.needShowTutorial)
        { 
            fingerLeftPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2 - Screen.width/2/2, Screen.height/2/2, 10));
            fingerRightPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2 + Screen.width/2/2, Screen.height/2/2, 10));
            Instantiate(touchVFX, fingerLeftPos, transform.rotation);
            Instantiate(touchVFX, fingerRightPos, transform.rotation);
        }*/
    }

    private void Update()
    {
        if (tutorialBlink + tutorialBlinkDelay <= Time.time)
        {
            BlinkTutorial();
            tutorialBlink = Time.time;
        }
    }

    private void BlinkTutorial()
    {
        if (tapToFly.activeSelf)
        {
            tapToFly.SetActive(false);
            arrowLeft.SetActive(false);
            arrowRight.SetActive(false);
        }
        else
        {
            tapToFly.SetActive(true);
            arrowLeft.SetActive(true);
            arrowRight.SetActive(true);
        }
    }
}
