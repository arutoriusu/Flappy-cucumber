using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControl : MonoBehaviour
{
    [SerializeField] GameObject tutorial;

    private void Update()
    {
        if (PlayerPrefs.GetString("needTutorial") == "true" && Score.score < 4 && !tutorial.activeSelf)
        {
            tutorial.SetActive(true);
        }
    }
}
