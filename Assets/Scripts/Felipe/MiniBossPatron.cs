using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MiniBossPatron : MonoBehaviour
{
    public MiniBossData miniboss;
    public float timer;
    public Animator Anim;
    public bool EnableTimer;
    public enum AttackPattern
    {
        Normal,
        SpecialAttack1,
        SpecialAttack2
    }

       
    public float normalAttackDelay = 2f;
    public float specialAttack1Delay = 4f;
    public float specialAttack2Delay = 3f;

    private AttackPattern currentAttackPattern;

    // L�gica de inicializaci�n
    void Start()
    {
        currentAttackPattern = AttackPattern.Normal;
        InvokeRepeating("PerformAttack", 0f, normalAttackDelay);
    }
    private void Awake()
    {

        timer = miniboss.exhaustedRecoveryTime;
        
    }
    // L�gica de actualizaci�n por cada frame
    void Update()
    {
        if(EnableTimer == true)
        {
            timer -= Time.deltaTime;      
        }

        if (timer <= 0)
        {
            PerformAttack();     
        }      
    }
    // L�gica para realizar ataques
    public void EnableTime()
    {
        EnableTimer = true;
    }
    public void DisableTime()
    {
        EnableTimer = false;
    }
    private void PerformAttack()
    {
        if (timer >= 0) return;

        // Comprobamos el patr�n de ataque actual y realizamos la acci�n correspondiente
        switch (currentAttackPattern)
        {
            case AttackPattern.Normal:
                NormalAttack();
                break;
            case AttackPattern.SpecialAttack1:
                SpecialAttack1();
                break;
            case AttackPattern.SpecialAttack2:
                SpecialAttack2();
                break;
            default:
                break;
        }
        timer = miniboss.exhaustedRecoveryTime;
        // Cambiamos al siguiente patr�n de ataque
        ChangeAttackPattern();
    }

    // L�gica para el ataque normal
    void NormalAttack()
    {
        Anim.SetBool("Patron1", true);
        Debug.Log("Ataque normal");     
    }

    // L�gica para el ataque especial 1
    void SpecialAttack1()
    {
        Anim.SetBool("Patron2", true);
        Debug.Log("Ataque especial 1");
        
    }

    // L�gica para el ataque especial 2
    void SpecialAttack2()
    {
        Anim.SetBool("Patron3", true);
        Debug.Log("Ataque especial 2");
    }

    // Cambia el patr�n de ataque seg�n las condiciones y estados del miniboss
    void ChangeAttackPattern()
    {
        if (miniboss.isEnraged)
        {
            // L�gica para patrones de ataque cuando el miniboss est� enfurecido
            // ...
            currentAttackPattern = (AttackPattern)Random.Range(1, 3); // Selecciona aleatoriamente entre SpecialAttack1 y SpecialAttack2
        }
        else if (miniboss.isExhausted)
        {
            // L�gica para patrones de ataque cuando el miniboss est� agotado
            // ...
            currentAttackPattern = AttackPattern.Normal; // Puedes ajustar esta l�gica seg�n tus necesidades
        }
        else
        {
            // L�gica para patrones de ataque normales
            // ...
            currentAttackPattern = AttackPattern.Normal; // Puedes ajustar esta l�gica seg�n tus necesidades
        }

        // Cancela las invocaciones anteriores y programa la pr�xima invocaci�n seg�n el nuevo patr�n de ataque
        CancelInvoke("PerformAttack");
        switch (currentAttackPattern)
        {
            case AttackPattern.Normal:
                InvokeRepeating("PerformAttack", 0f, normalAttackDelay);
                break;
            case AttackPattern.SpecialAttack1:
                InvokeRepeating("PerformAttack", 0f, specialAttack1Delay);
                break;
            case AttackPattern.SpecialAttack2:
                InvokeRepeating("PerformAttack", 0f, specialAttack2Delay);
                break;
            default:
                break;
        }
    }
}
