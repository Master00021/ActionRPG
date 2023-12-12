using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using System;
public abstract class Attack : MonoBehaviour
{
    public static Action OnBossTired;

    public BossData BossData;
    public float StanminaSpent;
    public abstract void UseAttack(Animator animator);

    public float Damage;
    public float RageDamage;

}
