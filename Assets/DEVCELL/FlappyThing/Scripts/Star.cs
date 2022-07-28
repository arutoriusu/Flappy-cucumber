using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] GameObject score;
    private bool isSelfSpawned;

    void Update()
    {

        transform.position = new Vector3(
            transform.position.x - 2f * Time.deltaTime,
            transform.position.y,
            transform.position.z);

        if (transform.position.x < 1 && !isSelfSpawned)
        {
            Instantiate(gameObject, new Vector3(transform.position.x + 12.75f, Random.Range(-1, 3), 0), Quaternion.Euler(0, 0, 0));
            isSelfSpawned = true;
        }

        if (transform.position.x < -22)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Thing")
        {
            Score.score += 1;
            Destroy(gameObject);
        }
    }
}
