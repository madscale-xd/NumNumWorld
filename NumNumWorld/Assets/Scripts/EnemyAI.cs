using UnityEngine;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    public int attackValue; 
    [Header("Enemy HP Display")]
    public TextMeshPro enemyValueText;

    [Header("Enemy Attack Display")]
    public TextMeshPro enemyAttackText; // <- Add this in the Inspector

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
    }

    public void GenerateEnemyValue()
    {
        enemyValue = Random.Range(2000, 5000);
        if (enemyValueText != null)
            enemyValueText.text = enemyValue.ToString();
    }

    public int GetEnemyValue()
    {
        return enemyValue;
    }

    public void TakeDamage(int damage)
    {
        enemyValue -= damage;
        enemyValue = Mathf.Max(0, enemyValue);
        if (enemyValueText != null)
            enemyValueText.text = enemyValue.ToString();

        if (enemyValue == 0)
        {
            DestroyEnemy();
        }
    }

    public void DestroyEnemy()
    {
        if (targetToDestroy != null)
        {
            playerMovement.isStopped = false;
            auraTrigger.TogglePanel();
            Destroy(targetToDestroy);
            ResetNumbers(); // Reset everything here
        }
    }

    public void ResetNumbers()
    {
        // Reset drop slots
        foreach (DigitDropSlot slot in dropSlots)
        {
            slot.ResetSlot();
        }

        // Redraw digits
        foreach (RandomDigitDrawer drawer in digitAssigners)
        {
            drawer.DrawRandomDigit();
        }

        // Reset operators
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
        attackValue = Random.Range(8, 256); // You can tweak this range
        if (enemyAttackText != null)
        {
            enemyAttackText.text = $"{attackValue}";
        }

        // You can later extend this to actually apply damage to the player, etc.
    }

    public void DefendTurn()
    {
        auraTrigger.TogglePanel();
        panelTrigger.TogglePanel();
        ResetNumbers();
        if (enemyAttackText != null)
        {
            enemyAttackText.text = $"{attackValue}";
        }

        // You can later extend this to actually apply damage to the player, etc.
    }
}
