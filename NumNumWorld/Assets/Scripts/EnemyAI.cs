using UnityEngine;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    public int attackValue;

    public int rampingValue = 0;

    [Header("Enemy HP Settings")]
    public int maxHP = 3;
    public int currentHP;
    public TextMeshPro enemyHPText;

    [Header("Enemy Value Display")]
    public TextMeshPro enemyValueText;

    [Header("Enemy Attack Display")]
    public TextMeshPro enemyAttackText;

    [Header("Target To Destroy")]
    public GameObject targetToDestroy;

    [Header("References")]
    public AuraPanelTrigger auraTrigger;
    public PanelTrigger panelTrigger;
    public PlayerMovement playerMovement;

    [Header("Resettable Components")]
    public RandomDigitDrawer[] digitAssigners = new RandomDigitDrawer[20];
    public DigitDropSlot[] dropSlots = new DigitDropSlot[10];
    public OperatorCycleButton[] operatorButtons = new OperatorCycleButton[8];
    public TextMeshProUGUI resultText;

    [Header("Margin of Error Settings")]
    public int maxMarginOfError = 5; // Customize this in the Inspector
    public int appliedError = 0;     // For display/debugging

    [Header("Margin of Error Display")]
    public TextMeshPro enemyMarginText;


    private int enemyValue;

    void Start()
    {
        GenerateEnemyValue();
        currentHP = maxHP;
        UpdateHPDisplay();
        SetTextVisibility(false, false);
    }

    public void GenerateValues()
    {
        GenerateEnemyValue();
        currentHP = maxHP;
        UpdateHPDisplay();
    }

    public void GenerateEnemyValue()
    {
        int baseValue = Random.Range(8 + rampingValue, 100 + rampingValue);
        appliedError = Random.Range(-maxMarginOfError, maxMarginOfError + 1);
        enemyValue = baseValue+appliedError;

        if (enemyValue < 0) enemyValue = 0; // Clamp to avoid negatives

        if (enemyValueText != null)
        {
            enemyValueText.text = enemyValue.ToString();
        }

        if (enemyMarginText != null)
        {
            enemyMarginText.text = $"±{Mathf.Abs(appliedError)}";
        }
    }

    public int GetEnemyValue()
    {
        return enemyValue;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage; // Take 1 hit regardless of damage value
        currentHP = Mathf.Max(0, currentHP);
        UpdateHPDisplay();

        if (currentHP == 0)
        {
            DestroyEnemy();
        }
    }

    public void UpdateHPDisplay()
    {
        if (enemyHPText != null)
        {
            enemyHPText.text = $"{currentHP}/{maxHP}";
        }
    }

    public void DestroyEnemy()
    {
        if (targetToDestroy != null)
        {
            playerMovement.isStopped = false;
            FindObjectOfType<PrefabSpawner>().SpawnRandomPrefab();
            auraTrigger.TogglePanel();
            ResetNumbers();
            auraTrigger.ShowPanel();
            panelTrigger.ShowPanel();
            Destroy(targetToDestroy);
            Destroy(gameObject);
        }
    }

    public void ResetNumbers()
    {
        if (resultText != null)
            resultText.text = "Digits and operators reset!";
        foreach (DigitDropSlot slot in dropSlots)
        {
            slot.ResetSlot();
        }

        foreach (RandomDigitDrawer drawer in digitAssigners)
        {
            drawer.DrawRandomDigit();
        }

        foreach (OperatorCycleButton op in operatorButtons)
        {
            op.SetOperator("+");
        }
    }

    public void AttackTurn() // enemy attacking
    {
        auraTrigger.TogglePanel();
        panelTrigger.TogglePanel();
        ResetNumbers();
        attackValue = Random.Range(8 + rampingValue, 100 + rampingValue);

        if (enemyAttackText != null)
        {
            enemyAttackText.text = $"{attackValue}";
        }

        SetTextVisibility(attackVisible: true, defenseVisible: false);
    }

    public void DefendTurn() // enemy defending (happens first)
    {
        GenerateEnemyValue();
        auraTrigger.TogglePanel();
        panelTrigger.TogglePanel();
        ResetNumbers();

        if (enemyAttackText != null)
        {
            enemyAttackText.text = $"{attackValue}";
        }

        SetTextVisibility(attackVisible: false, defenseVisible: true);
    }

    public void AssignReferencesDynamically()
    {
        auraTrigger = FindObjectOfType<AuraPanelTrigger>();
        panelTrigger = FindObjectOfType<PanelTrigger>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        digitAssigners = FindObjectsOfType<RandomDigitDrawer>();
        dropSlots = FindObjectsOfType<DigitDropSlot>();
        operatorButtons = FindObjectsOfType<OperatorCycleButton>();

        resultText = FindObjectOfType<TextMeshProUGUI>();

        // Optional: you can log to confirm
        Debug.Log($"[EnemyAI] Found {digitAssigners.Length} digit assigners, {dropSlots.Length} slots, {operatorButtons.Length} ops.");
    }
    
    public void SetTextVisibility(bool attackVisible, bool defenseVisible)
    {
        if (enemyAttackText != null)
            enemyAttackText.gameObject.SetActive(attackVisible);

        if (enemyValueText != null)
            enemyValueText.gameObject.SetActive(defenseVisible);
    }
}
