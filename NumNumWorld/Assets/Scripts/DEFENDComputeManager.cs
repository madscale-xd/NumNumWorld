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

        // Compute left to right
        float result = v1;
        result = ApplyOperator(result, o1, v2);
        result = ApplyOperator(result, o2, v3);
        result = ApplyOperator(result, o3, v4);
        result = ApplyOperator(result, o4, v5);

        resultText.text = "Defense Result: " + result.ToString("0.##");

        if (currentEnemy != null)
        {
            int expected = currentEnemy.attackValue;

            if (Mathf.Approximately(result, expected))
            {
                resultText.text += "\nPerfect block!";
                Debug.Log("Player successfully blocked the attack.");
            }
            else
            {
                resultText.text += "\nFailed block! You take 1 damage.";
                Debug.Log($"Player takes damage! Enemy attack was {expected}, you computed {result}");

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
}
