using UnityEngine;
using System.Collections;

public class EnemyTypeVisualSwitcher : MonoBehaviour
{
    [Header("Target Renderer")]
    public SpriteRenderer targetRenderer;

    [Header("Sprites by Enemy Type")]
    public Sprite prescriptivaSprite;
    public Sprite restrictivaSprite;
    public Sprite completativaSprite;

    private void Start()
    {
        StartCoroutine(ApplyVisualNextFrame());
    }

    private IEnumerator ApplyVisualNextFrame()
    {
        yield return null; // Wait 1 frame to let other scripts finish (like loading enemyType)
        ApplyVisualByType();
    }

    public void ApplyVisualByType()
    {
        EnemyTypeModifier typeMod = GetComponent<EnemyTypeModifier>();
        if (typeMod == null || targetRenderer == null)
        {
            Debug.LogWarning("[EnemyTypeVisualSwitcher] Missing references.");
            return;
        }

        switch (typeMod.enemyType)
        {
            case EnemyTypeModifier.EnemyType.Prescriptiva:
                targetRenderer.sprite = prescriptivaSprite;
                break;

            case EnemyTypeModifier.EnemyType.Restrictiva:
                targetRenderer.sprite = restrictivaSprite;
                break;

            case EnemyTypeModifier.EnemyType.Completativa:
                targetRenderer.sprite = completativaSprite;
                break;

            default:
                Debug.LogWarning("[EnemyTypeVisualSwitcher] Unknown type.");
                break;
        }

        Debug.Log($"[EnemyTypeVisualSwitcher] Set sprite for {typeMod.enemyType}");
    }
}
