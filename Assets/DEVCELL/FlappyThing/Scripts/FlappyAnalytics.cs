using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class FlappyAnalytics : MonoBehaviour
{
    public void onDisableSound()
    {
        Analytics.CustomEvent("disableSound");
    }

    public void onEnableSound()
    {
        Analytics.CustomEvent("enableSound");
    }

    public void onDisableMusic()
    {
        Analytics.CustomEvent("disableMusic");
    }

    public void onEnableMusic()
    {
        Analytics.CustomEvent("enableMusic");
    }

    public void sendTimeChooseNickname(float time)
    {
        Analytics.CustomEvent("timeChooseNickname", new Dictionary<string, object>
        {
            {"time", time.ToString()},
        });
    }

    public void deathOfThing()
    {
        Analytics.CustomEvent("deathOfThing");
    }

    public void DoubleTap()
    {
        Analytics.CustomEvent("DoubleTap");
    }

    public void SingleTap()
    {
        Analytics.CustomEvent("SingleTap");
    }

    public void GamingTime(float time)
    {
        Analytics.CustomEvent("gaming time", new Dictionary<string, object>
        {
            {"time", time.ToString()},
        });
    }

}
