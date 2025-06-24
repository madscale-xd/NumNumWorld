using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public Vector3 position;
    public int currentHP;
    public int maxHP;

    // Add these for saving types
    public string typeModifier;  // e.g., "Prescriptiva"
    public string appendModifier; // e.g., "Addios"
    public int appendedNumber;   // save the actual random value used
}