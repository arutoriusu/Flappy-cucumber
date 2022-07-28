using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSerializer
{
    public string Name;
    public string Country;
    public int Highscore;
    public bool ID;
    public string GoogleID;

    public PlayerSerializer(string ID, string Name, string Country, int Highscore, string GoogleID)
    {
        this.Name = Name;
        this.Country = Country;
        this.Highscore = Highscore;
        this.GoogleID = GoogleID;
    }

    public PlayerSerializer(string Name, string Country, int Highscore, string GoogleID)
    {
        this.Name = Name;
        this.Country = Country;
        this.Highscore = Highscore;
        this.GoogleID = GoogleID;
    }

}

[System.Serializable]
public class PlayerSerializer2
{
    public string name;
    public string country;
    public int score;
    public string googleid;
    public string devcell;

    public PlayerSerializer2(string name, string country, int score, string googleid, string devcell)
    {
        this.name = name;
        this.country = country;
        this.score = score;
        this.googleid = googleid;
        this.devcell = devcell;
    }
}

[System.Serializable]
public class PlayerListSerializer
{
    public List<PlayerSerializer> data;

}

[System.Serializable]
public class PlayerMiniSerializer
{
    public string country;
    public string position;
    public int score;
}

