using UnityEngine;
using UnityEngine.SceneManagement; // Import this for scene loading
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isStopped = false;

    [Header("Reference to Compute Manager")]
    public ComputeManager computeManager; // Assign in Inspector
    public DEFENDComputeManager defComputeManager;

    [Header("Player Stats")]
    public int playerHP = 5;

    [Header("Text Display")]
    public TextMeshPro playerHPText; // Assign this in the Inspector (not UGUI)

    void Start()
    {
        UpdateHPDisplay();
    }

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
            defComputeManager.currentEnemy = enemy;
        }
    }

    public void TakeDamage(int amount)
    {
        playerHP -= amount;
        playerHP = Mathf.Max(playerHP, 0); // Clamp to 0
        UpdateHPDisplay();

        Debug.Log($"Player takes {amount} damage! Current HP: {playerHP}");

        if (playerHP <= 0)
        {
            Debug.Log("Player HP reached 0. Loading EndScene...");
            SceneManager.LoadScene("EndScene");
        }
    }

    public void DamagePlayer()
    {
        TakeDamage(1);
    }

    private void UpdateHPDisplay()
    {
        if (playerHPText != null)
        {
            playerHPText.text = $"{playerHP}";
        }
    }
}
