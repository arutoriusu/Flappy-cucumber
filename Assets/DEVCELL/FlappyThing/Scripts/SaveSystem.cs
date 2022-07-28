using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    static string path = Application.persistentDataPath + "/player.lol";

    public static void SavePlayerInfo(bool firstStart, string username, string country, int highScore, string googleId)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerInfo info = new PlayerInfo(firstStart, username, country, highScore, googleId);

        formatter.Serialize(stream, info);
        stream.Close();
    }

    public static PlayerInfo LoadPlayerInfo()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerInfo data = formatter.Deserialize(stream) as PlayerInfo;
            
            stream.Close();
            return data;
        }
        else
        {
            //Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
