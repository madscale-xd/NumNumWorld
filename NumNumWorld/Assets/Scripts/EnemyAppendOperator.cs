using UnityEngine;
using TMPro;

public class EnemyAppendModifier : MonoBehaviour
{
    public enum EnemyType { Addios, Menos, Exos, Dividos, Equalos }
    public EnemyType enemyType;

    [Header("Append Operators")]
    public string appendedOperator = "0"; // The operator to append (could be "+", "-", "*", "/")
    public int appendedNumber = 1; // Number to append to the operation

    [Header("UI Display")]
    public TextMeshPro appendDisplayText; // Assign this in the Inspector to show the appended operation

    void Start()
    {
        // Only apply append logic if we haven't loaded from a save
        if (!wasLoadedFromSave)
        {
            GenerateRandomAppend(); // generate and apply random operator + number
        }
        else
        {
            ApplyAppendModifier(); // just re-apply display logic
        }
    }

    private bool wasLoadedFromSave = false;

    /// <summary>
    /// Used by SceneSaver to apply loaded values.
    /// </summary>
    public void LoadFromSave(EnemyType loadedType, int loadedNumber)
    {
        enemyType = loadedType;
        appendedNumber = loadedNumber;
        wasLoadedFromSave = true;
        ApplyAppendModifier();
    }

    /// <summary>
    /// Randomly generates append operator and number based on enemy type.
    /// </summary>
    private void GenerateRandomAppend()
    {
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
                appendedOperator = "×";
                appendedNumber = Random.Range(1, 3);
                break;
            case EnemyType.Dividos:
                appendedOperator = "÷";
                appendedNumber = Random.Range(1, 3);
                break;
            case EnemyType.Equalos:
                appendedOperator = "+";
                appendedNumber = 0;
                break;
        }

        ApplyAppendModifier(); // make sure the display updates
    }

    /// <summary>
    /// Updates the append display based on current operator and number.
    /// </summary>
    public void ApplyAppendModifier()
    {
        appendedOperator = GetCurrentEnemyOperation();

        if (appendDisplayText != null)
        {
            appendDisplayText.text = $"{appendedOperator}{appendedNumber}";
        }
    }

    public string GetAppendedOperator() => appendedOperator;
    public int GetAppendedNumber() => appendedNumber;

    public string GetCurrentEnemyOperation()
    {
        return enemyType switch
        {
            EnemyType.Addios => "+",
            EnemyType.Menos => "-",
            EnemyType.Exos => "×",
            EnemyType.Dividos => "÷",
            EnemyType.Equalos => "+", // Equalos uses "+" for healing logic
            _ => "+"
        };
    }
}
