using UnityEngine;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy HP Display")]
    public TextMeshPro enemyValueText;

    [Header("Target To Destroy")]
    public GameObject targetToDestroy;

    [Header("References")]
    public AuraPanelTrigger auraTrigger;
    public PlayerMovement playerMovement;

    [Header("Resettable Components")]
    public RandomDigitDrawer[] digitAssigners = new RandomDigitDrawer[10];
    public DigitDropSlot[] dropSlots = new DigitDropSlot[5];
    public OperatorCycleButton[] operatorButtons = new OperatorCycleButton[4];
    public TextMeshProUGUI resultText;

    private int enemyValue;

    void Start()
    {
        GenerateEnemyValue();
    }

    public void GenerateEnemyValue()
    {
        enemyValue = Random.Range(20, 51);
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
}
