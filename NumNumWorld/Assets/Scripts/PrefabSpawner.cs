using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [Header("Prefab Options")]
    [SerializeField] private List<GameObject> prefabChoices = new List<GameObject>();

    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnPoint;

    [Header("Kill Count")]
    public int killCount = -1;

    [Header("Kill Thresholds (Editable)")]
    public int threshold1 = 5;   // e.g., 0–4 → elements 0–2
    public int threshold2 = 10;  // e.g., 5–9 → + elements 3–5
    public int threshold3 = 20;  // e.g., 10–19 → + elements 6–8
    public int threshold4 = 30;  // e.g., 20–29 → + elements 9–11

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

        Instantiate(prefabToSpawn, spawnPoint != null ? spawnPoint.position : transform.position, Quaternion.identity);

        Debug.Log($"[PrefabSpawner] Spawned: {prefabToSpawn.name}, Kill Count: {killCount}");
    }
}
