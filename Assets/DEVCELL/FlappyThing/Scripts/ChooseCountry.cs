using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCountry : MonoBehaviour
{
    private PlayerRequest pr;
    private TextMeshProUGUI textTitle;
    //[SerializeField] private Transform panelTransform;
    [SerializeField] private Button buttonPrev;
    [SerializeField] private Button buttonNext;

    /*    [SerializeField] List<GameObject> playersScores;
        [SerializeField] List<GameObject> playersNames;
        [SerializeField] List<GameObject> playersCountries;*/

    [SerializeField] GameObject scoresControl;

    bool isWorld = true;

    private void Start()
    {
        pr = new PlayerRequest();
        textTitle = transform.GetComponentInChildren<TextMeshProUGUI>();
        buttonPrev.onClick.AddListener(Click_Prev);
        buttonNext.onClick.AddListener(Click_Next);
    }

    //Click_Prev
    public void Click_Prev()
    {
        ChangeText();
    }

    //Click_Next
    public void Click_Next()
    {
        ChangeText();
    }

    private void ChangeText()
    {
        isWorld = !isWorld;
        if (isWorld)
        {
            textTitle.text = "World";
            PlayerListSerializer playersData = pr.GetPlayersScoresWorld();

            if (playersData != null)
            {
                scoresControl.GetComponent<ScoresControl>().SetScores(playersData);
            }
        }
        else
        {
            PlayerInfo pi = SaveSystem.LoadPlayerInfo();
            PlayerListSerializer playersData = pr.GetPlayersScoresCountry(pi.country);

            if (playersData != null)
            {
                scoresControl.GetComponent<ScoresControl>().SetScores(playersData);
            }
            
            if (pi.country == null || pi.country == "")
            {
                textTitle.text = "Country";
            }
            else
            {
                textTitle.text = pi.country;
            }
        }

    }
}
