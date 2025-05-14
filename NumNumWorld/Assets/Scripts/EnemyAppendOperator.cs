using UnityEngine;
using TMPro;

public class EnemyAppendModifier : MonoBehaviour
{
    public enum EnemyType { Addios, Menos, Exos, Dividos, Equalos }
    public EnemyType enemyType;

    [Header("Append Operators")]
    public string appendedOperator = "+"; // The operator to append (could be "+", "-", "*", "/")
    public int appendedNumber = 1; // Number to append to the operation

    [Header("UI Display")]
    public TextMeshPro appendDisplayText; // Assign this in the Inspector to show the appended operation

    void Start()
    {
        // Call to update the operator and number in the UI display
        GetAppendedOperation(); // This will set the appended operator and update the UI text
    }

    // You can modify this logic to apply different behaviors depending on the enemy type
    public string GetAppendedOperation()
    {
        // Customize how operations are appended based on the enemy type
        switch (enemyType)
        {
            case EnemyType.Addios:
                appendedOperator = "+";
                appendedNumber = Random.Range(1, 10);
                break;
            case EnemyType.Menos:
                appendedOperator = "-";
                appendedNumber = Random.Range(1, 10);
                break;
            case EnemyType.Exos:
                appendedOperator = "*";
                appendedNumber = Random.Range(1, 3);
                break;
            case EnemyType.Dividos:
                appendedOperator = "/";
                appendedNumber = Random.Range(1, 3);
                break;
            case EnemyType.Equalos:
                appendedOperator = "+";
                appendedNumber = 0;
                break;
        }

        // Update the UI to show the operator and number
        if (appendDisplayText != null)
        {
            appendDisplayText.text = $"{appendedOperator}{appendedNumber}";
        }

        return $"{appendedOperator}{appendedNumber}";
    }

    public string GetAppendedOperator()
    {
        return appendedOperator;
    }

    public int GetAppendedNumber()
    {
        return appendedNumber;
    }

    // ✨ NEW METHOD: Get operation symbol used by enemy type
    public string GetCurrentEnemyOperation()
    {
        switch (enemyType)
        {
            case EnemyType.Addios: return "+";
            case EnemyType.Menos: return "-";
            case EnemyType.Exos: return "*";
            case EnemyType.Dividos: return "/";
            case EnemyType.Equalos: return "+"; // Equalos uses "+" for healing logic
            default: return "+";
        }
    }
}
