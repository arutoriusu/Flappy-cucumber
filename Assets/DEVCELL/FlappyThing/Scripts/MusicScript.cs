using UnityEngine;

public class MusicScript : MonoBehaviour
{
    [SerializeField] AudioSource music;

    void Start()
    {
        if (PlayerPrefs.GetString("music") != "true")
        {
            gameObject.SetActive(false);
        }
        
    }
    void Update()
    {
        
    }
}
