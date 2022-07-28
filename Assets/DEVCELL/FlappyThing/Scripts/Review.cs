using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Review : MonoBehaviour
{
    [SerializeField] GameObject reviewObject;

    public void Rate()
    {
        reviewObject.SetActive(false);
        Application.OpenURL("market://details?id=com.AlekseyNesin.FlappyThing");
    }

    public void Close()
    {
        reviewObject.SetActive(false);
    }
}
