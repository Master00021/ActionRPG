using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BossAttackTwo : Attack
{
    public override void UseAttack(Animator animator)
    {
     
        animator.Play("Patron2");


    }
 
}
