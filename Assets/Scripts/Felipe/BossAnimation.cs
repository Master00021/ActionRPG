using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    public BossData Bossdata;

    public void AttackEnded() {
        Bossdata.IsAttacking = false;
    }
}
