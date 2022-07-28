using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundControl : MonoBehaviour
{
    [SerializeField] GameObject ground;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ground, new Vector3(0, -5f, 0), new Quaternion(0, 0, 0, 0));
        //Instantiate(ground, new Vector3(7.2f, -4f, -0.01f), new Quaternion(0, 0, 0, 0));
        //Instantiate(ground, new Vector3(-7.2f, -4f, -0.01f), new Quaternion(0, 0, 0, 0));
    }
}
