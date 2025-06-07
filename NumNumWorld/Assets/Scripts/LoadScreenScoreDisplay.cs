using UnityEngine;
using TMPro;

public class LoseScreenScoreDisplay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start()
    {
        SceneSaver sceneSaver = FindObjectOfType<SceneSaver>();
        int killCount = sceneSaver != null ? sceneSaver.GetKillCount() : 0;
        scoreText.text = killCount.ToString();

        HighScoreManager.SaveHighScoreIfGreater(killCount);

        int highScore = HighScoreManager.LoadHighScore();
        highScoreText.text = highScore.ToString();
    }
}
