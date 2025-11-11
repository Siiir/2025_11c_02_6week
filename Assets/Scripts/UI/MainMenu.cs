using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // MAKE SURE it's whatever number is the level1Scene in build settings
    private readonly int _level1Scene = 1;
    public void PlayGame()
    {
        SceneManager.LoadScene(_level1Scene);
    }
    
    public void ExitGame()
    {
        
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}