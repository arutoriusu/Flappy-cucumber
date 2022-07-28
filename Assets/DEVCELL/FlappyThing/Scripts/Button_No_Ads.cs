using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_No_Ads : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("no_ads") == "true")
        {
            gameObject.SetActive(false);
        }
    }
}
