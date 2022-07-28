using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContinueTimer : MonoBehaviour
{
    [SerializeField] GameObject pauseScript;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject music;

    double timer;
    int count;
    bool runTimer = false;

    public bool RunTimer { get => runTimer; set => runTimer = value; }

    private void OnEnable()
    {
        pauseButton.GetComponent<Button>().interactable = true;
        count = 3;
        timer = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        gameObject.GetComponent<TextMeshProUGUI>().text = count.ToString();
    }

    void Update()
    {
        if (Time.timeScale == 1)
        {
            gameObject.SetActive(false);
        }

        if (timer + 1000 <= DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds)
        {
            count -= 1;
            gameObject.GetComponent<TextMeshProUGUI>().text = count.ToString();
            timer = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        if (count == 0)
        {
            if (PlayerPrefs.GetString("music") == "true")
                music.SetActive(true);
            Time.timeScale = 1;
            pauseScript.GetComponent<PauseContinue>().Continuation = false;
            RunTimer = false;
            count = 3;
            gameObject.SetActive(false);
        }
    }
}
