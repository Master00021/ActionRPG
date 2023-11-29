using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BossAttackTree : Attack
{

    public override void UseAttack(Animator animator)
    {

        animator.CrossFade("attack4_kick", 0.1f);
    }
   
}
