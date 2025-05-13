using UnityEngine;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    public int attackValue;

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

    private int enemyValue;

    void Start()
    {
        GenerateEnemyValue();
        currentHP = maxHP;
        UpdateHPDisplay();
    }

    public void GenerateEnemyValue()
    {
        enemyValue = Random.Range(16, 512);
        if (enemyValueText != null)
            enemyValueText.text = enemyValue.ToString();
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

    private void UpdateHPDisplay()
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
            auraTrigger.TogglePanel();
            Destroy(targetToDestroy);
            ResetNumbers();
        }
    }

    public void ResetNumbers()
    {
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

        if (resultText != null)
            resultText.text = "Digits and operators reset!";
    }

    public void AttackTurn()
    {
        auraTrigger.TogglePanel();
        panelTrigger.TogglePanel();
        ResetNumbers();
        attackValue = Random.Range(8, 256);
        if (enemyAttackText != null)
        {
            enemyAttackText.text = $"{attackValue}";
        }
    }

    public void DefendTurn()
    {
        auraTrigger.TogglePanel();
        panelTrigger.TogglePanel();
        ResetNumbers();
        GenerateEnemyValue();
        if (enemyAttackText != null)
        {
            enemyAttackText.text = $"{attackValue}";
        }
    }
}
