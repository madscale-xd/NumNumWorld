using UnityEngine;
using TMPro;

public class ComputeManager : MonoBehaviour
{
    [Header("Drop Slots")]
    public DigitDropSlot dropSlot1;
    public DigitDropSlot dropSlot2;
    public DigitDropSlot dropSlot3;
    public DigitDropSlot dropSlot4;
    public DigitDropSlot dropSlot5;

    [Header("Operators")]
    public OperatorCycleButton operator1;
    public OperatorCycleButton operator2;
    public OperatorCycleButton operator3;
    public OperatorCycleButton operator4;

    [Header("Result")]
    public TextMeshProUGUI resultText;

    [Header("Enemy Reference")]
    public EnemyAI currentEnemy;
    private EnemyTypeModifier enemyTypeModifier => currentEnemy?.GetComponent<EnemyTypeModifier>();
    private EnemyAppendModifier enemyAppendModifier => currentEnemy?.GetComponent<EnemyAppendModifier>();

    [Header("Turn-based Panels")]

    public AuraPanelTrigger auraTrigger;


    public void OnComputeButtonPressed()
    {
        // Get values
        int v1 = dropSlot1.lockedInValue;
        int v2 = dropSlot2.lockedInValue;
        int v3 = dropSlot3.lockedInValue;
        int v4 = dropSlot4.lockedInValue;
        int v5 = dropSlot5.lockedInValue;

        // Get operators
        string o1 = operator1.GetCurrentOperator();
        string o2 = operator2.GetCurrentOperator();
        string o3 = operator3.GetCurrentOperator();
        string o4 = operator4.GetCurrentOperator();

        // Count matching operations used (this needs to happen before applying bonuses)
        int operationsUsed = CountEnemyOperations(o1, o2, o3, o4);

        // Get the bonus for attack and defense
        string bonus = enemyTypeModifier.GetOperationBonus(operationsUsed);

        // Compute left to right
        float result = v1;
        result = ApplyOperator(result, o1, v2);
        result = ApplyOperator(result, o2, v3);
        result = ApplyOperator(result, o3, v4);
        result = ApplyOperator(result, o4, v5);

        // Now apply the appended operation
        if (currentEnemy != null && currentEnemy.currentHP != 0)
        {
            int enemyValue = currentEnemy.GetEnemyValue();
            int margin = Mathf.Abs(currentEnemy.appliedError);

            // Apply operation-based bonus (updates enemyTypeModifier.attackBonus internally)
            int bonusDamage = 0;
            if (enemyTypeModifier != null)
            {
                enemyTypeModifier.GetOperationBonus(operationsUsed); // This will update the bonusDamage
                bonusDamage = enemyTypeModifier.attackBonus;
            }

            if (result >= enemyValue - margin && result <= enemyValue + margin)
            {
                resultText.text += "\nEnemy Defeated!";

                int totalDamage = 1 + bonusDamage;
                currentEnemy.TakeDamage(totalDamage);

                resultText.text += $"\nDealt {totalDamage} damage!";

                // Only call AttackTurn if enemy still has HP
                if (currentEnemy != null && currentEnemy.currentHP > 0)
                {
                    currentEnemy.AttackTurn();
                }
            }
            else
            {
                resultText.text += $"\nAttack failed! Computed {result}, needed {enemyValue} (±{margin}).";
                currentEnemy.AttackTurn();
            }
        }
        else
        {
            resultText.text += "\n(No enemy in range)";
            if (currentEnemy != null)
            {
                currentEnemy.DestroyEnemy();
                currentEnemy = null; // Clear reference
            }
        }
    }

    private float ApplyOperator(float a, string op, int b)
    {
        switch (op)
        {
            case "+": return a + b;
            case "-": return a - b;
            case "×": return a * b;
            case "÷":
                if (b == 0)
                {
                    resultText.text = "Error: Divide by 0";
                    throw new System.DivideByZeroException();
                }
                return a / b;
            default: return a;
        }
    }
    private int CountEnemyOperations(string o1, string o2, string o3, string o4)
    {
        // Assuming currentEnemy is assigned and has a method to get its operation type (e.g., Addios)
        string enemyOperation = enemyAppendModifier.GetCurrentEnemyOperation();  // This method would return "+" for Addios, "-" for Menos, etc.

        int count = 0;

        // Check how many times the enemy's operation is used in the current selection
        if (o1 == enemyOperation) count++;
        if (o2 == enemyOperation) count++;
        if (o3 == enemyOperation) count++;
        if (o4 == enemyOperation) count++;

        return count;
    }
}
