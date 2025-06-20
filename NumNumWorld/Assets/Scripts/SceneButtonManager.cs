using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour
{
    private AudioManager audioManager;
    public GameObject pauseMenuCanvas;

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
            Debug.Log("hahaha");
        }
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayButtonSFX();
        Application.Quit();
    }

    public void LoadGame()
    {
        AudioManager.Instance.PlayButtonSFX();
        SceneManager.LoadScene("NumNumMain"); 
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
        yield return null;  
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
            sceneSaver.SaveScene(); 
        }
        SceneManager.LoadScene("MainMenu");  
    }

    public void NewGame()
    {
        AudioManager.Instance.PlayButtonSFX();
        SceneManager.LoadScene("NumNumMain");
    }

    public void LoadRetry()
    {
        AudioManager.Instance.PlayButtonSFX();
        SceneManager.LoadScene("NumNumMain");
    }

    public void LoadMenu()
    {
        AudioManager.Instance.PlayButtonSFX();
        var tempSaver = new GameObject("TempSaver").AddComponent<SceneSaver>();
        tempSaver.ClearSave();
        Destroy(tempSaver.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToMenu()
    {
        AudioManager.Instance.PlayButtonSFX();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNewGame()
    {
        AudioManager.Instance.PlayButtonSFX();
        var tempSaver = new GameObject("TempSaver").AddComponent<SceneSaver>();
        tempSaver.ClearSave();
        Destroy(tempSaver.gameObject);

        SceneManager.LoadScene("NumNumMain");
    }

    public void TogglePauseMenu()
    {
        AudioManager.Instance.PlayButtonSFX();
        if (pauseMenuCanvas != null)
        {
            bool isActive = pauseMenuCanvas.activeSelf;
            pauseMenuCanvas.SetActive(!isActive);
            Time.timeScale = isActive ? 1f : 0f; 
        }
    }
}
