using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float force = 100f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && gameObject != null)
        {
            Destroy(gameObject);
        }
        
    }

    public void AddMoreForce(float moreForce)
    {
        this.force += moreForce;
    }
}
