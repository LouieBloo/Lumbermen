using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimatorHelper : MonoBehaviour
{
    public Transform projectileSpawnPosition;
    public Animator animator;
    private Action attackCallback;
    private Action rangePrepCallback;

    public void subscribeAttack(Action attackCallback)
    {
        this.attackCallback = attackCallback;
    }

    public void subscribeRangePrep(Action rangePrepCallback)
    {
        this.rangePrepCallback = rangePrepCallback;
    }

    public void attackFinished()
    {
        if (attackCallback != null)
        {
            attackCallback();
        }
    }

    public void rangePrepFinished()
    {
        if (rangePrepCallback != null)
        {
            rangePrepCallback();
        }
    }

    public void setAttackAnimationSpeed(float speed)
    {
        animator.SetFloat("AttackSpeed", speed);
    }
}
