using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BossTired : MonoBehaviour
{
    public float Timer;
    public float TiredTime;
    public BossData Bossdata;
    public Animator Anim;
    private void Awake()
    {
        Timer = TiredTime;
    }

    private void OnEnable()
    {
        Attack.OnBossTired += Tired;
        Boss.Onfallen += Fallen;
    }
    private void OnDisable()
    {
        Attack.OnBossTired -= Tired;
        Boss.Onfallen -= Fallen;
    }
    public void Tired()
    {
        if (Bossdata.Disabled) return;
        Bossdata.stamina = 0f;
        StartCoroutine(CO_Tired());

    }
    public IEnumerator CO_Tired()
    {
        Anim.CrossFade("idle", 0.4f);

        while(Bossdata.stamina <= 100f)
        {
            Bossdata.stamina += Bossdata.staminarecoverispeed * Time.deltaTime;
           Bossdata.IsAttacking = false;
            yield return null;

        }
        Bossdata.IsTired = false;
        



    }

    public void Fallen()
    {
        Bossdata.stamina = 0f;
        StartCoroutine(CO_Fallen());

    }
    public IEnumerator CO_Fallen()
    {
        while(Bossdata.stamina <= 100f)
        {
            Bossdata.stamina += Bossdata.staminarecoverispeed * Time.deltaTime;
           Bossdata.IsAttacking = false;
            yield return null;

        }
        Anim.CrossFade("idle", 0.4f);
        Bossdata.Isfallen = false;
        



    }
}
