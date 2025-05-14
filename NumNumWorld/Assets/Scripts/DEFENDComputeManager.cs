using UnityEngine;
using TMPro;

public class DEFENDComputeManager : MonoBehaviour
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

    [Header("Player Reference")]
    public PlayerMovement player;

    public void OnDefendComputeButtonPressed()
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

        // Count how many times the enemy's operation appears
        int operationsUsed = CountEnemyOperations(o1, o2, o3, o4);  // Keep this in scope

        // Get the bonus for attack and defense
        string bonus = enemyTypeModifier.GetOperationBonus(operationsUsed);

        // Compute left to right
        float result = v1;
        result = ApplyOperator(result, o1, v2);
        result = ApplyOperator(result, o2, v3);
        result = ApplyOperator(result, o3, v4);
        result = ApplyOperator(result, o4, v5);

        // Now apply the appended operation
        if (currentEnemy != null && enemyAppendModifier != null)
        {
            // Get the appended operator and number from the enemy's append modifier
            string appendedOperator = enemyAppendModifier.GetAppendedOperator();
            int appendedNumber = enemyAppendModifier.GetAppendedNumber();

            // Apply the appended operation to the result
            result = ApplyOperator(result, appendedOperator, appendedNumber);
        }

        resultText.text = "Defense Result: " + result.ToString("0.##");

        if (currentEnemy != null)
        {
            int expected = currentEnemy.attackValue;
            int margin = Mathf.Abs(currentEnemy.appliedError);

            // Operations used should be already available here
            if (result >= expected - margin && result <= expected + margin)
            {
                resultText.text += "\nPerfect block!";
                Debug.Log("Player successfully blocked the attack.");

                // Apply defense bonus based on enemy type
                if (enemyTypeModifier != null && player != null)
                {
                    int healBonus = enemyTypeModifier.defenseBonus;
                    if (healBonus > 0)
                    {
                        resultText.text += $"\nYou heal +{healBonus} HP!";
                        player.HealPlayer(healBonus);
                    }
                }
            }
            else
            {
                resultText.text += $"\nFailed block! You take 1 damage.";
                Debug.Log($"Player takes damage! Enemy attack was {expected} (±{margin}), you computed {result}");

                if (player != null)
                {
                    player.DamagePlayer();
                }
            }

            currentEnemy.DefendTurn();
        }
        else
        {
            resultText.text += "\n(No enemy in range)";
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
