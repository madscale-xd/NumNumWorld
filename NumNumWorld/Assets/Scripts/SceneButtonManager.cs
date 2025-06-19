using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour
{
    private AudioManager audioManager;

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
        AudioManager.Instance.PlayButton();
        Application.Quit();
    }

    public void LoadGame()
    {
        AudioManager.Instance.PlayButton();
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

    public void NewGame()
    {
        AudioManager.Instance.PlayButton();
        SceneManager.LoadScene("NumNumMain");
    }

    public void LoadRetry()
    {
        AudioManager.Instance.PlayButton();
        SceneManager.LoadScene("NumNumMain");
    }

    public void LoadMenu()
    {
         AudioManager.Instance.PlayButton();
        var tempSaver = new GameObject("TempSaver").AddComponent<SceneSaver>();
        tempSaver.ClearSave();
        Destroy(tempSaver.gameObject);

        // Keep listener active — it'll run, but won't load data (file is gone)
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToMenu()
    {
        AudioManager.Instance.PlayButton();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNewGame()
    {
         AudioManager.Instance.PlayButton();
        // Clear the existing save BEFORE reloading
        var tempSaver = new GameObject("TempSaver").AddComponent<SceneSaver>();
        tempSaver.ClearSave();
        Destroy(tempSaver.gameObject);

        // Keep listener active — it'll run, but won't load data (file is gone)
        SceneManager.LoadScene("NumNumMain");
    }
}
