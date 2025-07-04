using UnityEngine;
using UnityEngine.SceneManagement; // Import this for scene loading
using TMPro;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isStopped = false;

    [Header("Reference to Compute Manager")]
    public ComputeManager computeManager; // Assign in Inspector
    public DEFENDComputeManager defComputeManager;

    [Header("Player Stats")]
    public int playerHP = 5;
    public int maxHP = 5;

    [Header("Text Display")]
    public TextMeshPro playerHPText; // Assign this in the Inspector (not UGUI)

    [Header("Health Display")]
    public HeartDisplay heartDisplay;

    private AudioManager audioManager;

    void Start()
    {
        UpdateHPDisplay();
    }

    void Update()
    {
        if (!isStopped)
        {
            // Move right (+x direction)
            FindObjectOfType<PlayerAnimatorHandler>().PlayIdle();
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Aura"))
        {
            isStopped = true;
            FindObjectOfType<PlayerAnimatorHandler>().PlayAttack();
        }

        EnemyAI enemy = other.GetComponent<EnemyAI>();
        if (enemy != null && computeManager != null)
        {
            computeManager.currentEnemy = enemy;
            defComputeManager.currentEnemy = enemy;
            GetComponent<AuraPanelTrigger>().ShowPanel();
            GetComponent<PanelTrigger>().ShowPanel();
            enemy.AssignReferencesDynamically();
            GetComponent<AuraPanelTrigger>().HidePanel();
            GetComponent<PanelTrigger>().HidePanel();
            enemy.GenerateValues();
            enemy.SetTextVisibility(attackVisible: false, defenseVisible: true);
        }
    }

    private IEnumerator HandlePlayerDeathAndSceneLoad()
    {
        // Stop BGM
        AudioManager.Instance.bgmSource.Stop();

        // Play death SFX
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxPlayerDeath);

        // Wait for SFX duration
        yield return new WaitForSeconds(AudioManager.Instance.sfxPlayerDeath.length);

        // Now load EndScene
        SceneManager.LoadScene("EndScene");
    }


    public void TakeDamage(int amount)
    {
        playerHP -= amount;
        playerHP = Mathf.Max(playerHP, 0); // Clamp to 0
        UpdateHPDisplay();
        FindObjectOfType<PlayerAnimatorHandler>().PlayHurt();

        Debug.Log($"Player takes {amount} damage! Current HP: {playerHP}");

        if (playerHP <= 0)
        {
            FindObjectOfType<PlayerAnimatorHandler>().PlayDeath();
            Debug.Log("Player HP reached 0. Loading EndScene...");
            SceneSaver sceneSaver = FindObjectOfType<SceneSaver>();
            if (sceneSaver != null)
            {
                sceneSaver.SaveScene();
            }
            StartCoroutine(HandlePlayerDeathAndSceneLoad());
            // SceneManager.LoadScene("EndScene");
        }
    }

    public void DamagePlayer()
    {
        AudioManager.Instance.PlayMinusHealthSFX();
        TakeDamage(1);
    }

    private void UpdateHPDisplay()
    {
        if (playerHPText != null)
        {
            playerHPText.text = $"{playerHP}/{maxHP}";
        }

        if (heartDisplay != null)
        {
            heartDisplay.UpdateHearts(playerHP, maxHP);
        }
    }

    public void HealPlayer(int amount)
    {
        playerHP = Mathf.Min(playerHP + amount, maxHP);
        AudioManager.Instance.PlayPlayerHealSFX();
        UpdateHPDisplay(); // Optional method to update health bar/text
    }

    public PlayerData GetPlayerData()
    {
        return new PlayerData
        {
            playerHP = this.playerHP,
            maxHP = this.maxHP
        };
    }

    public void LoadPlayerData(PlayerData data)
    {
        this.playerHP = data.playerHP;
        this.maxHP = data.maxHP;
        UpdateHPDisplay();
    }

}
