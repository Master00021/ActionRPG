using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossAttackOne : Attack
{
    public override void UseAttack(Animator animator)
    {
        animator.Play("Patron1");
    }
    
}
