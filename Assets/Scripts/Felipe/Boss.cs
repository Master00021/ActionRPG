using System;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    public List<Attack> BossAttacks;
    public float DamageBoss;
    public Animator animator;
    public BossData BossData;

    private void Awake()
    {
        BossData.health = 1000f;
        BossData.stamina = 100f;
        BossData.invulnerable = false;
        BossData.Inrage = false;
        BossData.healrecoverispeed = 10f;
        BossData.staminarecoverispeed = 7.5f;
    }

    public void FixedUpdate()
    {
        BossData.health += BossData.healrecoverispeed;
        if (BossData.health >= 1000f)
        {
            BossData.health = 1000f;
        }
        

    }

    public void Attack(Collider other)
    {
        var nextAttack = BossAttacks[UnityEngine.Random.Range(0, BossAttacks.Count)];
        nextAttack.UseAttack(animator);
        print($"ataqué con: {nextAttack.name}");

        if(nextAttack.name == "Ataque 3")
        {
            BossData.invulnerable = true;
        }
        else
        {
            BossData.invulnerable = false;
        }

        if(other.TryGetComponent<IDamageable>(out var damageable))
        {
            if(BossData.stamina <= 0.0f)
            {

                return;
            }
            damageable.Damage(DamageBoss);
           
        }
    }
    public static event Action<float> OnDamagerecibe;

    public void Damage(float damage)
    {
        BossData.health -= damage;
        OnDamagerecibe?.Invoke(damage);
    }
}
