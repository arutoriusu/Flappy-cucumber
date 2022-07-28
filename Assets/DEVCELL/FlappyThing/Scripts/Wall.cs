using UnityEngine;

public class Wall: MonoBehaviour
{
    public GameObject[] walls;
    private bool isSelfSpawned;
    [SerializeField] GameObject score;
    [SerializeField] GameObject star;

    void Update ()
    {
        transform.position = new Vector3(
                                        transform.position.x - 2f * Time.deltaTime,
                                        transform.position.y,
                                        transform.position.z);

        if (transform.position.x < 1 && !isSelfSpawned)
        {
            isSelfSpawned = true;
            int rnd = Random.Range(0, walls.Length);
            GameObject objToSpawn = walls[rnd];
            Instantiate(objToSpawn, new Vector3(transform.position.x + 12.75f, -2.9f, 5f), objToSpawn.transform.rotation);
        }


        if (transform.position.x < -12)
        {
            if (Score.score > 48 && !Score.isFirstStarSpawned)
            {
                Score.isFirstStarSpawned = true;
                Instantiate(star, new Vector3(transform.position.x + 18.25f, 0, 0), star.transform.rotation);
            }
        }

        if (transform.position.x < -17)
        {
            Destroy(gameObject);
        }


    }
}
