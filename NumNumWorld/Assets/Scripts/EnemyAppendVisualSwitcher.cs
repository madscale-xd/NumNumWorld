using UnityEngine;
using System.Collections;

public class EnemyAppendVisualSwitcher : MonoBehaviour
{
    [Header("Target Animator")]
    public Animator targetAnimator;

    [Header("Animations by Append Type")]
    public RuntimeAnimatorController addiosController;
    public RuntimeAnimatorController menosController;
    public RuntimeAnimatorController exosController;
    public RuntimeAnimatorController dividosController;
    public RuntimeAnimatorController equalosController; // Optional

    private void Start()
    {
        StartCoroutine(ApplyVisualNextFrame());
    }

    private IEnumerator ApplyVisualNextFrame()
    {
        yield return null; // Wait a frame to ensure enemyType is loaded from save
        ApplyVisualByAppendType();
    }

    public void ApplyVisualByAppendType()
    {
        EnemyAppendModifier appendMod = GetComponent<EnemyAppendModifier>();
        if (appendMod == null || targetAnimator == null)
        {
            Debug.LogWarning("[EnemyAppendVisualSwitcher] Missing references.");
            return;
        }

        switch (appendMod.enemyType)
        {
            case EnemyAppendModifier.EnemyType.Addios:
                targetAnimator.runtimeAnimatorController = addiosController;
                break;

            case EnemyAppendModifier.EnemyType.Menos:
                targetAnimator.runtimeAnimatorController = menosController;
                break;

            case EnemyAppendModifier.EnemyType.Exos:
                targetAnimator.runtimeAnimatorController = exosController;
                break;

            case EnemyAppendModifier.EnemyType.Dividos:
                targetAnimator.runtimeAnimatorController = dividosController;
                break;

            case EnemyAppendModifier.EnemyType.Equalos:
                if (equalosController != null)
                {
                    targetAnimator.runtimeAnimatorController = equalosController;
                }
                else
                {
                    Debug.Log("[EnemyAppendVisualSwitcher] Equalos animation controller not assigned. Keeping existing animator.");
                }
                break;

            default:
                Debug.LogWarning("[EnemyAppendVisualSwitcher] Unknown append type.");
                break;
        }

        Debug.Log($"[EnemyAppendVisualSwitcher] Applied animator for {appendMod.enemyType}");
    }
}
