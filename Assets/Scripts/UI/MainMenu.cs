using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // MAKE SURE ITS WHATEVER NUMBER IS THE LEVEL 1 SCENE IN BUILD SETTINGS
    private int _level1Scene = 1;
    public void PlayGame()
    {
        SceneManager.LoadScene(_level1Scene);
    }
    
    public void ExitGame()
    {
        Debug.Log("Exit button pressed!");
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}