using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class PlayerRequest
{
    string devcellFavouriteWord = "ilovedevcellcompany";

    public PlayerMiniSerializer GetPlayerMiniData(string googleId)
    {
        try
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constants.serverUrl + "/flappyindex.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"googleid\":\"" + googleId + "\"}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JsonUtility.FromJson<PlayerMiniSerializer>(result);
            }
        }
        catch
        {
            return null;
        }
    }

    public void SendDataToServer(int score)
    {
        try
        {
            PlayerInfo pi = SaveSystem.LoadPlayerInfo();
            string yourName = pi.username;
            string yourCountry = pi.country;
            string googleId = pi.googleId;

            byte[] hash = Encoding.ASCII.GetBytes(yourName + yourCountry + score + devcellFavouriteWord);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string devcell = "";
            foreach (var b in hashenc)
            {
                devcell += b.ToString("x2");
            }

            string json = JsonUtility.ToJson(new PlayerSerializer2(yourName, yourCountry, score, googleId, devcell));
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constants.serverUrl + "/flappypost.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
        catch
        {

        }
    }

    public void SendDataToServer(string yourName, string yourCountry, int score, string googleId)
    {
        try
        {
            /*var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(yourName + yourCountry + score + devcellFavouriteWord));
            string devcell = Convert.ToBase64String(hash);*/
            byte[] hash = Encoding.ASCII.GetBytes(yourName + yourCountry + score + devcellFavouriteWord);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string devcell = "";
            foreach (var b in hashenc)
            {
                devcell += b.ToString("x2");
            }

            string json = JsonUtility.ToJson(new PlayerSerializer2(yourName, yourCountry, score, googleId, devcell));

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constants.serverUrl + "/flappypost.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
        catch
        {

        }
    }

    public PlayerListSerializer GetPlayersScoresCountry(string country)
    {
        try
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constants.serverUrl + "/flappypostflag.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"country\":\"" + country + "\"}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string stringPlayers = streamReader.ReadToEnd();

                PlayerListSerializer playersData = JsonUtility.FromJson<PlayerListSerializer>(stringPlayers);

                return playersData;

            }
        }
        catch
        {
            return null;
        }

    }

    public PlayerListSerializer GetPlayersScoresWorld()
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Constants.serverUrl + "/flappyget.php");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format.
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            string stringPlayers = readStream.ReadToEnd();
            PlayerListSerializer playersData = JsonUtility.FromJson<PlayerListSerializer>(stringPlayers);


            response.Close();
            readStream.Close();
            receiveStream.Close();

            return playersData;
        }
        catch
        {
            return null;
        }
        

    }
}
