using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossAttackOne : Attack
{
    public override void UseAttack(Animator animator)
    {
        BossData.stamina -= StanminaSpent;
        if (BossData.stamina >= 0.0f)
        {
            animator.CrossFade("attack1", 0.1f);

        }
        else
        {
            animator.CrossFade("idle", 0.1f);
            BossData.IsTired = true;
            OnBossTired?.Invoke();
        }
    }   
    
}
