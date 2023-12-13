using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BossAttackTree : Attack
{

    public override void UseAttack(Animator animator)
    {
        if (BossData.Disabled) return;
        if (BossData.stamina >= 0.0f && BossData.IsTired == false && BossData.Isfallen == false && BossData.IsAttacking == false)
        {
            BossData.IsAttacking = true;
            BossData.IsWalking = false;

            BossData.stamina -= StanminaSpent;

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
            animator.CrossFade("Attack4_kick", 0.1f);

        }
        else {
            BossData.IsAttacking = false;
        }


        if (BossData.stamina <= 0.0f)
        {
            BossData.IsTired = true;
            OnBossTired?.Invoke();
        }

    }
   
}
