using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public List<Attack> BossAttacks;
    public float DamageBoss;
    public Animator animator;
    public BossData BossData;

    public void Attack(Collider other)
    {
        var nextAttack = BossAttacks[Random.Range(0, BossAttacks.Count)];
        nextAttack.UseAttack(animator);
        print($"ataqu� con: {nextAttack.name}");

        if(nextAttack.name == "AttackInvulnerable")
        {
            BossData.invulnerable = true;
        }

        if(other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.Damage(DamageBoss);
        }
    }

}
