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

        // Compute left to right
        float result = v1;
        result = ApplyOperator(result, o1, v2);
        result = ApplyOperator(result, o2, v3);
        result = ApplyOperator(result, o3, v4);
        result = ApplyOperator(result, o4, v5);

        // Show result
        resultText.text = "Result: " + result.ToString("0.##");

        if (currentEnemy != null && currentEnemy.currentHP != 0)
        {
            int enemyValue = currentEnemy.GetEnemyValue();
                if (Mathf.RoundToInt(result) == enemyValue)
                {
                    resultText.text += "\nEnemy Defeated!";
                    currentEnemy.TakeDamage(1);

                    // Only call AttackTurn if enemy still has HP
                    if (currentEnemy != null && currentEnemy.currentHP > 0)
                    {
                        currentEnemy.AttackTurn();
                    }
                }
                else
                {
                    resultText.text += $"\nAttack failed! Computed {result}, needed {enemyValue}.";
                    currentEnemy.AttackTurn();
                }
        }
        else
        {
            resultText.text += "\n(No enemy in range)";
            currentEnemy.DestroyEnemy();
            currentEnemy = null; // Clear reference
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
}
