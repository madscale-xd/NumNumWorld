using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        Cursor.visible = true;
    }

    void Update()
    {
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
        SceneManager.LoadScene("NumNumMain"); // Load your gameplay scene
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "NumNumMain")
        {
            StartCoroutine(LoadAfterDelay());
        }
    }

    private IEnumerator LoadAfterDelay()
    {
        yield return null;  // wait one frame for scene objects to initialize
        SceneSaver sceneSaver = FindObjectOfType<SceneSaver>();
        if (sceneSaver != null)
        {
            Debug.Log("[SceneButtonManager] Calling LoadScene on SceneSaver");
            sceneSaver.LoadScene();
        }
        else
        {
            Debug.LogWarning("[SceneButtonManager] SceneSaver not found!");
        }
    }

    public void SaveAndReturnToMenu()
    {
        SceneSaver sceneSaver = FindObjectOfType<SceneSaver>();
        if (sceneSaver != null)
        {
            sceneSaver.SaveScene();  // Save current game state
        }
        SceneManager.LoadScene("MainMenu");  // Return to main menu scene
    }

    public void Newas()
    {
        SceneManager.LoadScene("NumNumMain");
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
        SceneManager.LoadScene("MainMenu");
    }
}
