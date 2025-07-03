using UnityEngine;
using System.IO;
using System.Collections;

public class SceneSaver : MonoBehaviour
{
    public string saveFileName = "scene_save.json";
    public PlayerMovement player;

    private string SavePath => Path.Combine(Application.persistentDataPath, saveFileName);

    public PrefabSpawner spawner;
    public EnemyData loadedEnemyData;

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
            EnemyTypeModifier typeMod = currentEnemy.GetComponent<EnemyTypeModifier>();
            EnemyAppendModifier appendMod = currentEnemy.GetComponent<EnemyAppendModifier>();

            EnemyData enemyData = new EnemyData
            {
                position = currentEnemy.transform.position,
                currentHP = currentEnemy.currentHP,
                maxHP = currentEnemy.maxHP,
                typeModifier = typeMod != null ? typeMod.enemyType.ToString() : "Prescriptiva",
                appendModifier = appendMod != null ? appendMod.enemyType.ToString() : "Addios",
                appendedNumber = appendMod != null ? appendMod.GetAppendedNumber() : 1
            };

            data.currentEnemy = enemyData;
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

        if (data.currentEnemy != null)
        {
            loadedEnemyData = data.currentEnemy; // ✅ Just store it for later
        }

        Debug.Log($"[SceneSaver] Loaded player position: {data.playerPosition}");
        Debug.Log($"[SceneSaver] Loaded player stats: {data.playerStats}");

        if (player == null)
        {
            Debug.LogWarning("[SceneSaver] Player reference is null!");
            return;
        }

        player.transform.position = data.playerPosition;
        player.LoadPlayerData(data.playerStats);

        if (data.currentEnemy != null)
        {
            EnemyAI currentEnemy = FindObjectOfType<EnemyAI>();
            if (currentEnemy != null)
            {
                currentEnemy.transform.position = data.currentEnemy.position;
                currentEnemy.maxHP = data.currentEnemy.maxHP;
                currentEnemy.currentHP = data.currentEnemy.currentHP;
                currentEnemy.UpdateHPDisplay();

                // Apply EnemyTypeModifier
                EnemyTypeModifier typeMod = currentEnemy.GetComponent<EnemyTypeModifier>();
                if (typeMod != null && System.Enum.TryParse(data.currentEnemy.typeModifier, out EnemyTypeModifier.EnemyType savedType))
                {
                    typeMod.LoadFromSave(savedType);
                }

                // Apply EnemyAppendModifier
                EnemyAppendModifier appendMod = currentEnemy.GetComponent<EnemyAppendModifier>();
                if (appendMod != null && System.Enum.TryParse(data.currentEnemy.appendModifier, out EnemyAppendModifier.EnemyType savedAppendType))
                {
                    appendMod.LoadFromSave(savedAppendType, data.currentEnemy.appendedNumber);
                }

                Debug.Log("[SceneSaver] Enemy stats and modifiers loaded.");
            }
            else
            {
                Debug.LogWarning("[SceneSaver] No in-scene enemy found to apply loaded data to.");
            }
            GetComponent<EnemyTypeVisualSwitcher>()?.ApplyVisualByType();
            GetComponent<EnemyAppendVisualSwitcher>()?.ApplyVisualByAppendType();
        }

        if (spawner != null)
        {
            spawner.killCount = data.killCount;
            Debug.Log($"[SceneSaver] Loaded kill count: {data.killCount}");
            spawner.UpdateKillCountUI();
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
        yield return new WaitForSeconds(0.5f);
        yield return null;  // Let the scene initialize
        SceneSaver sceneSaver = FindObjectOfType<SceneSaver>();
        if (sceneSaver != null)
        {
            Debug.Log("[SceneButtonManager] Calling LoadScene on SceneSaver");
            sceneSaver.LoadScene();
            GetComponent<EnemyTypeVisualSwitcher>()?.ApplyVisualByType();
            GetComponent<EnemyAppendVisualSwitcher>()?.ApplyVisualByAppendType();
        }
        else
        {
            Debug.LogWarning("[SceneButtonManager] SceneSaver not found!");
        }
    }

    public int GetKillCount()
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("[SceneSaver] No save file exists. Returning 0.");
            return 0;
        }

        string json = File.ReadAllText(SavePath);
        SceneSaveData data = JsonUtility.FromJson<SceneSaveData>(json);
        return data.killCount;
    }
}
