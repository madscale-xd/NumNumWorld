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

        if (currentEnemy != null)
        {
            int enemyValue = currentEnemy.GetEnemyValue();
            if (result >= enemyValue)
            {
                resultText.text += "\nEnemy Defeated!";
                currentEnemy.DestroyEnemy();
                currentEnemy = null; // Clear reference
            }
            else
            {
                // If the result is less than the enemy's value, subtract from the enemy's health
                currentEnemy.TakeDamage((int)result);
                resultText.text += "\nEnemy takes damage!";
            }
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
}
