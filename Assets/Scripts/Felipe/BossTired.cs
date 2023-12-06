using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTired : MonoBehaviour
{
    public float Timer;
    public float TiredTime;
    public BossData BossData;
    private void Awake()
    {
        Timer = TiredTime;
    }

    private void OnEnable()
    {
        Attack.OnBossTired += Tired;
    }
    private void OnDisable()
    {
        Attack.OnBossTired -= Tired;
    }

    public void Tired()
    {
        StartCoroutine(CO_Tired());
    }
    public IEnumerator CO_Tired()
    {
        while(Timer >= 0.0f) // estamina no timer
        {
            Timer -= Time.deltaTime;
            yield return null;
        }
        BossData.IsTired = false;
        Timer = TiredTime;



    }
}
