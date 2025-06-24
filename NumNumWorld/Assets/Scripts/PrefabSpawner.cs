using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrefabSpawner : MonoBehaviour
{
    [Header("Prefab Options")]
    [SerializeField] private List<GameObject> prefabChoices = new List<GameObject>();

    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnPoint;

    [Header("Global Size Factor")]
    [SerializeField] private float prefabScaleFactor = 1f;

    [Header("Kill Count")]
    public int killCount = -1;

    [Header("Kill Count UI")]
    [SerializeField] private TextMeshProUGUI killCountText;

    [Header("Kill Thresholds (Editable)")]
    public int threshold1 = 5;
    public int threshold2 = 10;
    public int threshold3 = 20;
    public int threshold4 = 30;
    public int threshold5 = 40;
    public int threshold6 = 50;
    public int threshold7 = 60;
    public int threshold8 = 70;
    public int threshold9 = 80;
    public int threshold10 = 100;

    [Header("Dynamic Difficulty Settings")]
    [SerializeField] private List<int> maxErrorByStage = new List<int> {15, 13, 10, 7, 4, 2, 1};
    [SerializeField] private List<int> maxHPByStage = new List<int> { 3, 5, 7, 9, 12, 16, 20, 25, 35, 50 };

    void Start()
    {
        SpawnRandomPrefab(); // Also calls UpdateKillCountUI
    }

    public void SpawnRandomPrefab()
    {
        killCount++;

        List<GameObject> validChoices = new List<GameObject>();

        // Always include Element 0–2
        validChoices.AddRange(prefabChoices.GetRange(0, 3));

        if (killCount >= threshold2 && prefabChoices.Count >= 6)
            validChoices.AddRange(prefabChoices.GetRange(3, 3));

        if (killCount >= threshold3 && prefabChoices.Count >= 9)
            validChoices.AddRange(prefabChoices.GetRange(6, 3));

        if (killCount >= threshold4 && prefabChoices.Count >= 12)
            validChoices.AddRange(prefabChoices.GetRange(9, 3));

        if (validChoices.Count == 0)
        {
            Debug.LogWarning("No valid prefabs available for this kill count.");
            return;
        }

        int randomIndex = Random.Range(0, validChoices.Count);
        GameObject prefabToSpawn = validChoices[randomIndex];

        GameObject spawned = Instantiate(
            prefabToSpawn,
            spawnPoint != null ? spawnPoint.position : transform.position,
            Quaternion.identity
        );

        spawned.transform.localScale *= prefabScaleFactor;

        // Configure enemy stats
        EnemyAI ai = spawned.GetComponent<EnemyAI>();
        if (ai != null)
        {
            int errorValue = GetErrorForKillCount();
            ai.maxMarginOfError = errorValue;
            int hpValue = GetHPForKillCount();
            ai.maxHP = hpValue;
            ai.currentHP = hpValue;
            ai.rampingValue = killCount;
            ai.UpdateHPDisplay();
            ai.rampMult = GetMultForKillCount();
        }

        Debug.Log($"[PrefabSpawner] Spawned: {prefabToSpawn.name}, Kill Count: {killCount}");

        // ✅ Update UI
        UpdateKillCountUI();
    }

    public void UpdateKillCountUI()
    {
        if (killCountText != null)
            killCountText.text = $"{killCount}";
    }

    private int GetErrorForKillCount()
    {
        if (killCount < threshold1)
            return maxErrorByStage[0];
        else if (killCount < threshold3)
            return maxErrorByStage[1];
        else if (killCount < threshold4)
            return maxErrorByStage[2];
        else if (killCount < threshold5)
            return maxErrorByStage[3];
        else if (killCount < threshold6)
            return maxErrorByStage[4];
        else if (killCount < threshold8)
            return maxErrorByStage[5];
        else
            return maxErrorByStage[6];
    }

    private int GetHPForKillCount()
    {
        if (killCount < threshold1)
            return maxHPByStage[0];
        else if (killCount < threshold2)
            return maxHPByStage[1];
        else if (killCount < threshold3)
            return maxHPByStage[2];
        else if (killCount < threshold4)
            return maxHPByStage[3];
        else if (killCount < threshold5)
            return maxHPByStage[4];
        else if (killCount < threshold6)
            return maxHPByStage[5];
        else if (killCount < threshold7)
            return maxHPByStage[6];
        else if (killCount < threshold8)
            return maxHPByStage[7];
        else if (killCount < threshold9)
            return maxHPByStage[8];
        else
            return maxHPByStage[9];
    }

    private int GetMultForKillCount()
    {
        if (killCount < threshold1)
            return 1;
        else if (killCount < threshold4)
            return 2;
        else if (killCount < threshold8)
            return 3;
        else
            return 4;
    }
}
