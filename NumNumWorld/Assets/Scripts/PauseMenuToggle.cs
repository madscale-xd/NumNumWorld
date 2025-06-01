using UnityEngine;

public class PauseMenuToggle : MonoBehaviour
{
    [Header("Pause Menu Panel")]
    [SerializeField] private GameObject pausePanel;

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
        isPaused = !isPaused;

        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;  // Pause the game
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;  // Resume the game
        }
    }

    // Optional: Explicit Pause/Unpause (e.g., for buttons)
    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
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
