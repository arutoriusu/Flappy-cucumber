using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterScript : MonoBehaviour
{
    [SerializeField] GameObject nameplaceObject;
    [SerializeField] GameObject bigLetters;
    [SerializeField] GameObject smallLetters;

    // Start is called before the first frame update
    void Start()
    {
        //OpenBigLetters();
    }

    public void OpenSmallLetters()
    {
        bigLetters.SetActive(false);
        smallLetters.SetActive(true);
    }

    public void OpenBigLetters()
    {
        bigLetters.SetActive(true);
        smallLetters.SetActive(false);
    }

    public void Del()
    {
        if (nameplaceObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Length > 0)
        {
            nameplaceObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = nameplaceObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Remove(nameplaceObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Length - 1);
        }
        
    }

    private bool LimitName()
    {
        if (nameplaceObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Length > 9)
        {
            return false;
        }
        return true;
    }

    public void TypeLetter()
    {
        nameplaceObject.transform.GetChild(1).gameObject.SetActive(false);
        if (!LimitName())
        {
            return;
        }
        string letter = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<TextMeshProUGUI>().text;
        nameplaceObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += letter;
    }
}
