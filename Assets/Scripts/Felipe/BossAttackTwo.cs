using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BossAttackTwo : Attack
{
    public override void UseAttack(Animator animator)
    {
        BossData.stamina -= StanminaSpent;
        if (BossData.stamina >= 0.0f && BossData.Isfallen == false && BossData.IsTired == false)
        {
            if (BossData.Inrage == true)
            {
                animator.speed = 1.3f;
                BossData.DamageBoss = RageDamage;
            }
            else
            {
                animator.speed = 1f;
                BossData.DamageBoss = Damage;
            }
            animator.CrossFade("attack2", 0.1f);

        }
        else
        {
            animator.CrossFade("idle", 0.1f);
            BossData.IsTired = true;
            OnBossTired?.Invoke();
        }
    }
 
}
