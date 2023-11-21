using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MiniBossPatron : MonoBehaviour
{
    public MiniBossData miniboss;
    public float timer;
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

    // Lógica de inicialización
    void Start()
    {
        currentAttackPattern = AttackPattern.Normal;
        InvokeRepeating("PerformAttack", 0f, normalAttackDelay);
    }
    private void Awake()
    {

        timer = miniboss.exhaustedRecoveryTime;
        
    }
    // Lógica de actualización por cada frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            PerformAttack();
       
        }
        
    }
    private void Cooldown()
    {

    }
    // Lógica para realizar ataques
    private void PerformAttack()
    {
        if (timer >= 0) return;

        // Comprobamos el patrón de ataque actual y realizamos la acción correspondiente
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
        // Cambiamos al siguiente patrón de ataque
        ChangeAttackPattern();
    }

    // Lógica para el ataque normal
    void NormalAttack()
    {
        
        Debug.Log("Ataque normal");
    }

    // Lógica para el ataque especial 1
    void SpecialAttack1()
    {
        // Lógica del primer ataque especial
        // ...
        Debug.Log("Ataque especial 1");
    }

    // Lógica para el ataque especial 2
    void SpecialAttack2()
    {
        // Lógica del segundo ataque especial
        // ...
        Debug.Log("Ataque especial 2");
    }

    // Cambia el patrón de ataque según las condiciones y estados del miniboss
    void ChangeAttackPattern()
    {
        if (miniboss.isEnraged)
        {
            // Lógica para patrones de ataque cuando el miniboss está enfurecido
            // ...
            currentAttackPattern = (AttackPattern)Random.Range(1, 3); // Selecciona aleatoriamente entre SpecialAttack1 y SpecialAttack2
        }
        else if (miniboss.isExhausted)
        {
            // Lógica para patrones de ataque cuando el miniboss está agotado
            // ...
            currentAttackPattern = AttackPattern.Normal; // Puedes ajustar esta lógica según tus necesidades
        }
        else
        {
            // Lógica para patrones de ataque normales
            // ...
            currentAttackPattern = AttackPattern.Normal; // Puedes ajustar esta lógica según tus necesidades
        }

        // Cancela las invocaciones anteriores y programa la próxima invocación según el nuevo patrón de ataque
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
