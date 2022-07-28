using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public string username;
    public string country;
    public int highScore;
    public bool firstStart;
    public string googleId;

    public PlayerInfo(bool firstStart, string username, string country, int highScore, string googleId)
    {
        this.username = username;
        this.country = country;
        this.highScore = highScore;
        this.firstStart = firstStart;
        this.googleId = googleId;
    }

}
