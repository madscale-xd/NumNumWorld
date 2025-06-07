using UnityEngine;
using System.IO;
using System.Collections;

public class SceneSaver : MonoBehaviour
{
    public string saveFileName = "scene_save.json";
    public PlayerMovement player;

    private string SavePath => Path.Combine(Application.persistentDataPath, saveFileName);

    public PrefabSpawner spawner;

    void Awake()
    {
        if (player == null)
            player = FindObjectOfType<PlayerMovement>();
        if (spawner == null)
            spawner = FindObjectOfType<PrefabSpawner>();
        StartCoroutine("LoadAfterDelay");
    }

    public void SaveScene()
    {
        SceneSaveData data = new SceneSaveData
        {
            playerPosition = player.transform.position,
            playerStats = player.GetPlayerData()
        };

        EnemyAI currentEnemy = FindObjectOfType<EnemyAI>();
        if (currentEnemy != null)
        {
            data.currentEnemy = new EnemyData
            {
                currentHP = currentEnemy.currentHP,
                maxHP = currentEnemy.maxHP,
                position = currentEnemy.transform.position
            };
        }
        if (spawner != null)
        {
            data.killCount = spawner.killCount;
        }
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log($"[SceneSaver] Saved scene to {SavePath}");
    }

    public void LoadScene()
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("[SceneSaver] No save file found.");
            return;
        }

        string json = File.ReadAllText(SavePath);
        SceneSaveData data = JsonUtility.FromJson<SceneSaveData>(json);

        Debug.Log($"[SceneSaver] Loaded player position: {data.playerPosition}");
        Debug.Log($"[SceneSaver] Loaded player stats: {data.playerStats}");

        if (player == null)
        {
            Debug.LogError("[SceneSaver] Player reference is null!");
            return;
        }

        player.transform.position = data.playerPosition;
        player.LoadPlayerData(data.playerStats);

        EnemyAI currentEnemy = FindObjectOfType<EnemyAI>();
        if (currentEnemy != null && data.currentEnemy != null)
        {
            currentEnemy.transform.position = data.currentEnemy.position;
            currentEnemy.currentHP = data.currentEnemy.currentHP;
            currentEnemy.maxHP = data.currentEnemy.maxHP;
            currentEnemy.UpdateHPDisplay();
        }

        if (spawner != null)
        {
            spawner.killCount = data.killCount;
            Debug.Log($"[SceneSaver] Loaded kill count: {data.killCount}");
        }

        Debug.Log("[SceneSaver] Scene data loaded.");
    }

    public void ClearSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("[SceneSaver] Save file deleted.");
        }
    }
    
    private IEnumerator LoadAfterDelay()
    {
        yield return null;  // Let the scene initialize
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
}
