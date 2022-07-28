using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundsScript : MonoBehaviour
{
    [SerializeField] AudioClip[] hitSounds;
    [SerializeField] AudioClip[] rolloverSounds;
    [SerializeField] GameObject continueOrRestart;

    float timeScene = 0;

    private void Update()
    {
        LoadFinalScene();
    }

    private void LoadFinalScene()
    {
        float delay = 0.9f;
        if (timeScene > 0 && Time.time >= timeScene + delay && !continueOrRestart.GetComponent<ContinueOrRestart>().Continuation)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void playHit()
    {
        int sound = 0;// UnityEngine.Random.Range(0, 3);

        if (PlayerPrefs.GetString("sound") == "true")
        {
            AudioSource.PlayClipAtPoint(hitSounds[sound], Camera.main.transform.position);
        }
        
        //hitSounds[sound].Play();

        timeScene = Time.time;

    }

    internal void playRollover()
    {
        int sound = UnityEngine.Random.Range(0, 3);
        if (PlayerPrefs.GetString("sound") == "true")
        {
            AudioSource.PlayClipAtPoint(rolloverSounds[sound], Camera.main.transform.position);
        }
        //timeScene = Time.time;
    }
}
