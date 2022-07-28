using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScriptObject : MonoBehaviour
{
    public void Restart()
    {
        Score.score = 0;
        SceneManager.LoadScene(1);
    }
}
