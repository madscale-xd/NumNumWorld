using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
    }

    void Update()
    {
        // Pressing Escape will return to the Main Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("NumNumMain"); // Replace with your actual gameplay scene name
    }

    public void LoadRetry()
    {
        SceneManager.LoadScene("NumNumMain");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu"); // You can change this to any "back" destination
    }
}
