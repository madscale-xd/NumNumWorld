using UnityEngine;

public class EnemyTypeModifier : MonoBehaviour
{
    public enum EnemyType { Prescriptiva, Restrictiva, Alternativa }

    [Header("Enemy Type")]
    public EnemyType enemyType; // Set this in the inspector to determine which type of bonus to apply

    [Header("Bonuses")]
    public int attackBonus = 0;  // Set the bonus damage based on the condition
    public int defenseBonus = 0; // Set the healing bonus based on the condition
    
    void Start()
    {
        // Call to update the operator and number in the UI display
        GetOperationBonus(1); // This will set the appended operator and update the UI text
    }

    public string GetOperationBonus(int operationCount)
    {
        // Determine the bonus based on the enemy type and the operation count
        switch (enemyType)
        {
            case EnemyType.Prescriptiva:
                if (operationCount >= 3)
                {
                    attackBonus = 1;  // +1 bonus damage
                    defenseBonus = 1; // Heal +1 HP
                }
                break;

            case EnemyType.Restrictiva:
                if (operationCount < 2)
                {
                    attackBonus = 1;  // +1 bonus damage
                    defenseBonus = 1; // Heal +1 HP
                }
                break;

            case EnemyType.Alternativa:
                if (operationCount == 4)
                {
                    attackBonus = 2;  // +2 bonus damage
                    defenseBonus = 2; // Heal +2 HP
                }
                break;
        }

        return $"{attackBonus} Bonus Damage / Heal +{defenseBonus} HP";
    }
}
