using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject pauseScript;
    [SerializeField] GameObject continueOrRestart;

    private bool pauseGame;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!continueOrRestart.activeSelf)
            {
                pauseScript.GetComponent<PauseContinue>().MakePause();
            }
        }
    }

    private void OnApplicationPause(bool pause)
    {
        //pauseScript.GetComponent<PauseContinue>().MakePause();
    }

    private void OnApplicationQuit()
    {
        pauseScript.GetComponent<PauseContinue>().MakePause();
    }
}
