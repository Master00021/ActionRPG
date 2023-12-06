using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRage : MonoBehaviour
{
    public BossData BossData;
    public float AcumulateDamage;
    public float TimerAcumulateDamage;
    public float Timer;
    public float DamageToRage;
    public bool CorrutineStart;

    private void Awake()
    {
        Timer = TimerAcumulateDamage;
    }
    public void OnEnable()
    {
        Boss.OnDamagerecibe += CalculateDamageRecibeInTime;
    }
    public void OnDisable()
    {
        Boss.OnDamagerecibe -= CalculateDamageRecibeInTime;
    }
    public void CalculateDamageRecibeInTime(float Damage)
    {
        AcumulateDamage += Damage;
        print("caculate");
        StartCoroutine(CO_TimetoResetDamage());
      
    }
    public IEnumerator CO_TimetoResetDamage()
    {
        CorrutineStart = true;
        if (CorrutineStart)
        {
           
            while (Timer > 0.0f && AcumulateDamage != 0.0f)
            {
                Timer -= Time.deltaTime;
                if(AcumulateDamage > DamageToRage)
                {
                    BossData.Inrage = true;
                }
                yield return null;
            }
            AcumulateDamage = 0.0f;
            Timer = TimerAcumulateDamage;
            CorrutineStart = false;

        }
      
        
    }
}
