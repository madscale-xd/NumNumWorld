using UnityEngine;
using System.IO;

public class HighScoreManager : MonoBehaviour
{
    private static string FileName => "high_score.json";
    private static string SavePath => Path.Combine(Application.persistentDataPath, FileName);

    public static int LoadHighScore()
    {
        if (!File.Exists(SavePath))
        {
            return 0;
        }

        string json = File.ReadAllText(SavePath);
        HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);
        return data.highScore;
    }

    public static void SaveHighScoreIfGreater(int score)
    {
        int currentHigh = LoadHighScore();
        if (score > currentHigh)
        {
            HighScoreData newData = new HighScoreData { highScore = score };
            string json = JsonUtility.ToJson(newData, true);
            File.WriteAllText(SavePath, json);
            Debug.Log($"[HighScoreManager] New high score saved: {score}");
        }
    }

    public static void ClearHighScore()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("[HighScoreManager] High score file deleted.");
        }
    }
}
