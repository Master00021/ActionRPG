using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTired : MonoBehaviour
{
    public float Timer;
    public float TiredTime;
    public BossData Bossdata;
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
        Bossdata.stamina = 0f;
        StartCoroutine(CO_Tired());

    }
    public IEnumerator CO_Tired()
    {
        while(Bossdata.stamina <= 100f) // estamina no timer
        {
            Bossdata.stamina += Bossdata.staminarecoverispeed * Time.deltaTime;
            yield return null;

        }
        Bossdata.IsTired = false;
        Timer = TiredTime;



    }
}
