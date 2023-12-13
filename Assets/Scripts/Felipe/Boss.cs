using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable, ITrap, IParalyze
{
    public List<Attack> BossAttacks;
    public Animator animator;
    public BossData BossData;

    private void Awake()
    {
        print("Use 'F' to shotdown the Mini-Boss");
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
        BossData.Disabled = false;
    }

    public void FixedUpdate()
    {
        if (BossData.Disabled) return;

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
        if (BossData.Disabled) return;
        if (BossData.Isfallen == true) return;
        
        var nextAttack = BossAttacks[UnityEngine.Random.Range(0, BossAttacks.Count)];
        nextAttack.UseAttack(animator);

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
        if (BossData.Disabled) return;
        BossData.Isfallen = true;
        BossData.IsAttacking = false;
        BossData.IsWalking = false;
        animator.CrossFade("death", 0.1f);
        Onfallen?.Invoke();
    }

    public void Stun(Rigidbody rigidbody, float stunTime) {
        rigidbody.isKinematic = true;

        BossData.Disabled = true;

        StartCoroutine(CO_WaitForStunTimeEnd());

        IEnumerator CO_WaitForStunTimeEnd() {
            while (stunTime > 0.0f) {
                stunTime -= Time.deltaTime;
                yield return null;
            }
            rigidbody.isKinematic = false;
            BossData.Disabled = false;
        }
    }

    public void Reactivate(TrapConfiguration configuration, GameObject trap, float timeToReactivate) {

        StartCoroutine(CO_WaitToReactivate());

        BossData.Disabled = true;

        IEnumerator CO_WaitToReactivate() {
            while (timeToReactivate > 0.0f) {
                timeToReactivate -= Time.deltaTime;
                yield return null;
            }
            trap.SetActive(true);
            BossData.Disabled = false;
        }
    }

    public void Paralyze(ParalysisConfiguration configuration, Rigidbody rigidbody, float timeToStop, bool activated) {
        StartCoroutine(CO_TimeToEndParalysis());

        BossData.Disabled = true;

        IEnumerator CO_TimeToEndParalysis() {
            rigidbody.isKinematic = true;
            while (timeToStop > 0.0f) {
                timeToStop -= Time.deltaTime;
                yield return null;
            }
            rigidbody.isKinematic = false;
            activated = false;
            BossData.Disabled = false;
        }
    }

}
