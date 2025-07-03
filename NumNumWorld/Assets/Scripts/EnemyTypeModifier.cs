using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeModifier : MonoBehaviour
{
    public enum EnemyType { Prescriptiva, Restrictiva, Completativa }

    [Header("Enemy Type")]
    public EnemyType enemyType;

    [Header("Bonuses")]
    public int attackBonus = 0;
    public int defenseBonus = 0;
    private EnemyAppendModifier appendModifier;

    void Start()
    {
        // Dummy/default initialization with placeholders
        SetOperationBonus(new string[] { "+", "-", "*", "/" });
        appendModifier = GetComponent<EnemyAppendModifier>();
    }
    private bool wasLoadedFromSave = false;

    /// <summary>
    /// Used by SceneSaver to apply loaded values.
    /// </summary>
    public void LoadFromSave(EnemyType loadedType)
    {
        enemyType = loadedType;
    }


    // ✅ Call this with the 4 operators used in the equation
    public void SetOperationBonus(string[] usedOperators)
    {
        attackBonus = 0;
        defenseBonus = 0;

        switch (enemyType)
        {
            case EnemyType.Prescriptiva:
                if (CountMatchingOperations(usedOperators) >= 2)
                {
                    attackBonus = 1;
                    defenseBonus = 1;
                }
                break;

            case EnemyType.Restrictiva:
                if (CountMatchingOperations(usedOperators) < 3)
                {
                    attackBonus = 1;
                    defenseBonus = 1;
                }
                break;

            case EnemyType.Completativa:
                HashSet<string> requiredOps = new HashSet<string> { "+", "-", "×", "÷" };
                HashSet<string> opsUsed = new HashSet<string>(usedOperators);
                if (requiredOps.SetEquals(opsUsed))
                {
                    attackBonus = 2;
                    defenseBonus = 2;
                }
                break;
        }
    }

    // ✅ Optional: re-used by Prescriptiva and Restrictiva
    private int CountMatchingOperations(string[] usedOperators)
    {
        int count = 0;
        string enemyOp = GetEnemyOperation();
        foreach (string op in usedOperators)
        {
            if (op == enemyOp)
                count++;
        }
        return count;
    }

    public string GetEnemyOperation()
    {
        return appendModifier != null ? appendModifier.GetAppendedOperator() : "+";
    }

    public string GetOperationBonusText()
    {
        return $"{attackBonus} Bonus Damage / Heal +{defenseBonus} HP";
    }
}
