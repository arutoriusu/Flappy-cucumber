using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool isSelfSpawned;
    void Update()
    {
        transform.position = new Vector3(
                                        transform.position.x - 2f * Time.deltaTime,
                                        transform.position.y,
                                        transform.position.z);

        if (transform.position.x < 1 && !isSelfSpawned)
        {
            isSelfSpawned = true;
            Instantiate(gameObject, new Vector3(transform.position.x + 12.75f, -5f, 0f), new Quaternion(0, 0, 0, 0));
        }

        if (transform.position.x < -17)
        {
            Destroy(gameObject);
        }
    }
    
}
