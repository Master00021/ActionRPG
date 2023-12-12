using System;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    public List<Attack> BossAttacks;
    public Animator animator;
    public BossData BossData;

    private void Awake()
    {
        BossData.health = 1000f;
        BossData.stamina = 100f;
        BossData.healrecoverispeed = 10f;
        BossData.staminarecoverispeed = 7.5f;
        BossData.Inrage = false;
        BossData.invulnerable = false;
        BossData.IsTired = false;
        BossData.Isfallen = false;
        BossData.IsAttacking = false;
        BossData.IsWalking = false;
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Fallen();
        }
        BossData.health += BossData.healrecoverispeed;
        if (BossData.health >= 1000f)
        {
            BossData.health = 1000f;
        }
    }

    public void Attack(Collider other)
    {
        if (BossData.Isfallen == true) return;
        
        var nextAttack = BossAttacks[UnityEngine.Random.Range(0, BossAttacks.Count)];
        nextAttack.UseAttack(animator);
        print($"ataqu� con: {nextAttack.name}");

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
            if(BossData.IsTired == true && BossData.Isfallen == true)
            {

                return;
            }
            damageable.Damage(BossData.DamageBoss);
           
        }
    }
    public static event Action<float> OnDamagerecibe;
    public static event Action Onfallen;
    public void Damage(float damage)
    {
        BossData.health -= damage;
        OnDamagerecibe?.Invoke(damage);
    }
    public void Fallen()
    {
        
        BossData.Isfallen = true;
        BossData.IsAttacking = false;
        BossData.IsWalking = false;
        animator.CrossFade("death", 0.1f);
        Onfallen?.Invoke();
    }
}
