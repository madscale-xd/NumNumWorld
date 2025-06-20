using UnityEngine;

public class HeartDisplay : MonoBehaviour
{
    [Header("Heart Sprites")]
    public Sprite[] heartSprites; // 0 = full/good heart, 1 = empty/bad heart

    [Header("Heart Renderers")]
    public SpriteRenderer[] heartRenderers; // Assign your 5 heart GameObjects' SpriteRenderers here

    public void UpdateHearts(int currentHP, int maxHP)
    {
        for (int i = 0; i < heartRenderers.Length; i++)
        {
            if (i < currentHP)
            {
                heartRenderers[i].sprite = heartSprites[0]; // Full heart
            }
            else
            {
                heartRenderers[i].sprite = heartSprites[1]; // Empty heart
            }
        }
    }
}
