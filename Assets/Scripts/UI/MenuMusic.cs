using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private static MenuMusic instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps music playing between scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates when returning to menu
        }
    }
}