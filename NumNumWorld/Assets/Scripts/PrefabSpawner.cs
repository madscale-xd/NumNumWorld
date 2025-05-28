using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [Header("Prefab Options")]
    [SerializeField] private List<GameObject> prefabChoices = new List<GameObject>();

    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnPoint;

    [Header("Global Scale Factor")]
    [SerializeField] private float prefabScaleFactor = 1f;

    [Header("Kill Count")]
    public int killCount = -1;

    [Header("Kill Thresholds (Editable)")]
    public int threshold1 = 5;   // e.g., 0–4 → elements 0–2
    public int threshold2 = 10;  // e.g., 5–9 → + elements 3–5
    public int threshold3 = 20;  // e.g., 10–19 → + elements 6–8
    public int threshold4 = 30;  // e.g., 20–29 → + elements 9–11

    [Header("Dynamic Difficulty Settings")]
    [SerializeField] private List<int> maxErrorByStage = new List<int> { 10, 7, 5, 3, 1 };
    [SerializeField] private List<int> maxHPByStage = new List<int> { 3, 5, 7, 9, 12 };


    void Start()
    {
        SpawnRandomPrefab();
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

        // Only spawn once and store the result
        GameObject spawned = Instantiate(prefabToSpawn, spawnPoint != null ? spawnPoint.position : transform.position, Quaternion.identity);
        spawned.transform.localScale *= prefabScaleFactor;

        // Apply maxMarginOfError dynamically
        EnemyAI ai = spawned.GetComponent<EnemyAI>();
        if (ai != null)
        {
            int errorValue = GetErrorForKillCount();
            ai.maxMarginOfError = errorValue;
            Debug.Log($"[PrefabSpawner] Applied maxMarginOfError: {errorValue}");
            int hpValue = GetHPForKillCount();
            ai.maxHP = hpValue;
            ai.currentHP = hpValue; // Set currentHP to match for a fresh enemy
            ai.UpdateHPDisplay();   // Update the UI if needed
        }


        Debug.Log($"[PrefabSpawner] Spawned: {prefabToSpawn.name}, Kill Count: {killCount}");
    }

    private int GetErrorForKillCount()
    {
        if (killCount < threshold1)
            return maxErrorByStage[0];
        else if (killCount < threshold2)
            return maxErrorByStage[1];
        else if (killCount < threshold3)
            return maxErrorByStage[2];
        else if (killCount < threshold4)
            return maxErrorByStage[3];
        else
            return maxErrorByStage[4];
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
        else
            return maxHPByStage[4];
    }
}
