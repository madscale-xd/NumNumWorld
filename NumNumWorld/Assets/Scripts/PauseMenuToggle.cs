using UnityEngine;

public class PauseMenuToggle : MonoBehaviour
{
    [Header("Pause Menu Panel")]
    [SerializeField] private GameObject pausePanel;
    private AudioManager audioManager;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Hello");
            TogglePause();
        }
    }

    public void TogglePause()
    {
        // AudioManager.Instance.PlayButtonSFX();
        isPaused = !isPaused;

        if (isPaused)
        {
            pausePanel.SetActive(true);
            StartCoroutine(SetPauseTimeScale(0f));
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private System.Collections.IEnumerator SetPauseTimeScale(float timeScale)
    {
        yield return null;  // wait one frame so audio starts playing
        Time.timeScale = timeScale;
    }


    // Optional: Explicit Pause/Unpause (e.g., for buttons)
    public void PauseGame()
    {
        // AudioManager.Instance.PlayButtonSFX();
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        // AudioManager.Instance.PlayButtonSFX();
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void OnDestroy()
    {
        // Ensure time resumes if object is destroyed
        Time.timeScale = 1f;
    }
}
