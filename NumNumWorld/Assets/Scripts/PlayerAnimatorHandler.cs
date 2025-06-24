using UnityEngine;
using System.Collections;

public class PlayerAnimatorHandler : MonoBehaviour
{
    public Animator animator;

    private bool isPlayingDeath = false;
    private bool isPlayingHurt = false;
    private bool isPlayingParry = false;
    private bool isPunching = false;

    private Coroutine currentRoutine;

    public void PlayIdle()
    {
        if (isPlayingDeath || isPlayingHurt || isPlayingParry || isPunching) return;
        if (!IsPlaying("Player_Idle"))
        {
            animator.Play("Player_Idle");
        }
    }

    public void PlayAttack()
    {
        if (isPlayingDeath || isPlayingHurt || isPlayingParry || isPunching) return;
        if (!IsPlaying("Player_Atk"))
        {
            animator.Play("Player_Atk");
        }
    }

    public void PlayDefense()
    {
        if (isPlayingDeath || isPlayingHurt || isPlayingParry || isPunching) return;
        if (!IsPlaying("Player_Def"))
        {
            animator.Play("Player_Def");
        }
    }

    public void PlayHurt()
    {
        if (isPlayingDeath) return;

        isPlayingHurt = true;

        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(PlayHurtThenReturnToIdle());
    }

    public void PlayDeath()
    {
        if (isPlayingDeath) return;

        isPlayingDeath = true;

        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(PlayDeathAndFreeze());
    }

    public void PlayParry()
    {
        if (isPlayingDeath || isPlayingHurt) return;

        isPlayingParry = true;

        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(PlayParryThenReturnToIdle());
    }

    public void PlayPunch()
    {
        if (isPlayingDeath || isPlayingHurt || isPlayingParry) return;

        isPunching = true;

        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(PlayPunchThenReturnToIdle());
    }

    private IEnumerator PlayHurtThenReturnToIdle()
    {
        animator.Play("Player_Hurt");

        yield return new WaitForSeconds(GetAnimationLength("Player_Hurt"));

        isPlayingHurt = false;

        if (!isPlayingDeath && !isPlayingHurt)
        {
            animator.Play("Player_Atk");
        }

        currentRoutine = null;
    }

    private IEnumerator PlayParryThenReturnToIdle()
    {
        animator.Play("Player_Parry");

        yield return new WaitForSeconds(GetAnimationLength("Player_Parry"));

        isPlayingParry = false;

        if (!isPlayingDeath && !isPlayingHurt)
        {
            animator.Play("Player_Atk");
        }

        currentRoutine = null;
    }

    private IEnumerator PlayPunchThenReturnToIdle()
    {
        animator.Play("Player_Punch");

        yield return new WaitForSeconds(GetAnimationLength("Player_Punch"));

        isPunching = false;

        if (!isPlayingDeath && !isPlayingHurt)
        {
            animator.Play("Player_Atk");
        }

        currentRoutine = null;
    }

    private IEnumerator PlayDeathAndFreeze()
    {
        animator.Play("Player_Death");

        yield return new WaitForSeconds(GetAnimationLength("Player_Death"));

        animator.enabled = false; // Freeze animation at last frame
        currentRoutine = null;
    }

    private float GetAnimationLength(string animationName)
    {
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        foreach (var clip in ac.animationClips)
        {
            if (clip.name == animationName)
                return clip.length;
        }
        return 1f;
    }

    private bool IsPlaying(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}
