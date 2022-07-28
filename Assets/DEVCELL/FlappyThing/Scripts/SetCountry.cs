using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetCountry
{
    private String countryCode;

    public string CountryCode { get => countryCode; set => countryCode = value; }

    [Serializable]
    public class IpApiData
    {
        public string country_code;

        public static IpApiData CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<IpApiData>(jsonString);
        }
    }


    public IEnumerator SetCountryCode()
    {
        try
        {
            string ip = new System.Net.WebClient().DownloadString("https://api.ipify.org");
            string uri = $"https://ipapi.co/{ip}/json/";

            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                IpApiData ipApiData = IpApiData.CreateFromJSON(webRequest.downloadHandler.text);

                if (ipApiData != null)
                {
                    CountryCode = ipApiData.country_code;
                }
                else
                {
                    CountryCode = "-";
                }
            }
        }
        finally
        {

        }

    }
}
