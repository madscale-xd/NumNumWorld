using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [Header("Prefab Options")]
    [SerializeField] private List<GameObject> prefabChoices = new List<GameObject>();

    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnPoint; // Optional: set in inspector

    void Start()
    {
        SpawnRandomPrefab();
    }

    // Call this from EnemyAI.DestroyEnemy() or anywhere else
    public void SpawnRandomPrefab()
    {
        if (prefabChoices == null || prefabChoices.Count == 0)
        {
            Debug.LogWarning("No prefabs assigned to PrefabSpawner.");
            return;
        }

        int randomIndex = Random.Range(0, prefabChoices.Count);
        GameObject prefabToSpawn = prefabChoices[randomIndex];

        Instantiate(prefabToSpawn, spawnPoint != null ? spawnPoint.position : transform.position, Quaternion.identity);
    }
}
