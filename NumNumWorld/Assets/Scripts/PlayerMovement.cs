using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isStopped = false;

    [Header("Reference to Compute Manager")]
    public ComputeManager computeManager; // Assign in Inspector

    void Update()
    {
        if (!isStopped)
        {
            // Move right (+x direction)
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Aura"))
        {
            isStopped = true;
        }

        EnemyAI enemy = other.GetComponent<EnemyAI>();
        if (enemy != null && computeManager != null)
        {
            computeManager.currentEnemy = enemy;
        }
    }
}