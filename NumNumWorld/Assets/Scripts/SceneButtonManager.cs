using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene"); // Replace with your actual gameplay scene name
    }

    public void LoadRetry()
    {
        SceneManager.LoadScene("EndMenu");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
