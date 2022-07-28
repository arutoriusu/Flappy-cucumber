using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoresControl: MonoBehaviour
{
    [SerializeField] List<GameObject> playersScores;
    [SerializeField] List<GameObject> playersNames;
    [SerializeField] List<GameObject> playersCountries;
    [SerializeField] List<GameObject> playersPositions;

    private bool[] showPlayersScore = { true, true, true, true, true, true, true, true, true, true };

    public List<GameObject> PlayersScores { get => playersScores; set => playersScores = value; }
    public List<GameObject> PlayersNames { get => playersNames; set => playersNames = value; }
    public List<GameObject> PlayersCountries { get => playersCountries; set => playersCountries = value; }
    public bool[] ShowPlayersScore { get => showPlayersScore; set => showPlayersScore = value; }
    public List<GameObject> PlayersPositions { get => playersPositions; set => playersPositions = value; }

    public void SetScores(PlayerListSerializer playersData)
    {
        if (playersData != null)
        {
            //for (int i = 0; i < playersData.data.Count; i++)
            for (int i = 0; i < 10; i++)
            {
                if (ShowPlayersScore[i] == true && i <= playersData.data.Count-1)
                {
                    var countryTexture = Resources.Load<Texture>("Icons/Flags/" + playersData.data[i].Country.ToLower());

                    if (countryTexture)
                    {
                        PlayersCountries[i].transform.GetChild(1).GetComponent<RawImage>().texture = countryTexture;
                        PlayersCountries[i].transform.GetChild(1).gameObject.SetActive(true);
                        PlayersCountries[i].transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else
                    {
                        PlayersCountries[i].transform.GetChild(1).GetComponent<RawImage>().texture = null;
                        PlayersCountries[i].transform.GetChild(1).gameObject.SetActive(false);
                        PlayersCountries[i].transform.GetChild(0).gameObject.SetActive(true);
                    }

                    PlayersNames[i].GetComponent<TextMeshProUGUI>().text = playersData.data[i].Name;
                    PlayersScores[i].GetComponent<TextMeshProUGUI>().text = playersData.data[i].Highscore + "";
                }
                else
                {
                    PlayersCountries[i].SetActive(false);
                    PlayersNames[i].SetActive(false);
                    PlayersScores[i].SetActive(false);
                    PlayersPositions[i].SetActive(false);
                }
                
            }
        }
    }

    private void Update()
    {
        float modifier = ((float)Screen.height/(float)Screen.width) * 5.88f;
        //Debug.Log("mod = " + modifier);
        for (int i = 0; i < 10; i++)
        {
            if (i > modifier * 2f - 11) showPlayersScore[i] = false;
            else showPlayersScore[i] = true;
        }
        for (int i = 0; i < 10; i++)
        {
            if (ShowPlayersScore[i] == true)
            {
                PlayersCountries[i].SetActive(true);
                PlayersNames[i].SetActive(true);
                PlayersScores[i].SetActive(true);
                PlayersPositions[i].SetActive(true);
            }
            else
            {
                PlayersCountries[i].SetActive(false);
                PlayersNames[i].SetActive(false);
                PlayersScores[i].SetActive(false);
                PlayersPositions[i].SetActive(false);
            }
        }
    }
}
