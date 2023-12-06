using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public abstract class Attack : MonoBehaviour
{
    public BossData BossData;
    public float StanminaSpent;
    public abstract void UseAttack(Animator animator);
   
    

}
