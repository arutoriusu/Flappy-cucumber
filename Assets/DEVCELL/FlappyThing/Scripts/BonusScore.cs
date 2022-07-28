using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScore : MonoBehaviour
{
    private float bonusScoreShowStartTime;
    private float bonusScoreShowTime;

    public float BonusScoreShowStartTime { get => bonusScoreShowStartTime; set => bonusScoreShowStartTime = value; }

    void Start()
    {
        bonusScoreShowTime = 2f;
    }


    void Update()
    {
        if (BonusScoreShowStartTime + bonusScoreShowTime < Time.time)
        {
            BonusScoreShowStartTime = 0;
            gameObject.SetActive(false);
        }
    }
}
