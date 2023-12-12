using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossAttackOne : Attack
{
    public override void UseAttack(Animator animator)
    {
        
        if (BossData.stamina >= 0.0f && BossData.IsTired == false && BossData.Isfallen == false)
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
            animator.CrossFade("attack1", 0.1f);

        }
        
        if (BossData.stamina <= 0.0f)
        {
            BossData.IsTired = true;
            OnBossTired?.Invoke();
        }
    }   
    
}
