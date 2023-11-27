using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MiniBossPatron : MonoBehaviour
{
    public MiniBossData MiniData;
    public MiniBossDetect MiniDetect;
    public Animator Anim;
    public GameObject Player;
    public GameObject Spawn;
    
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
       
    }

    // Lógica de actualización por cada frame
    void Update()
    {
        if (MiniData.ReturnSpawn == true)
        {
            MiniData.exhaustedRecoveryTime -= Time.deltaTime;
            if (MiniData.exhaustedRecoveryTime == 0)
            {
                MiniData.exhaustedRecoveryTime = 0;
            }
        }
        if (MiniDetect.StayAlert == true && MiniData.CanAttack == true && MiniData.IsAttackin == false)
        {
            Anim.SetFloat("Walk", 0);
            CurretPattern();
            print("currentpater");
        }
        if (MiniData.CanAttack == true && MiniData.IsAttackin == false)
        {
            
            Anim.SetFloat("Walk", 0);
            PerformAttack();
            print("performatack");
        }
        if (MiniData.ReturnSpawn == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Spawn.transform.position, MiniData.Velocity * Time.deltaTime);
            MiniData.CanAttack = false;
        }
        if (transform.position == Spawn.transform.position)
        {
            MiniData.ReturnSpawn = false;
            MiniData.InSpawn = true;
            if (MiniData.exhaustedRecoveryTime == 0)
            {
                MiniData.IsAttackin = false;

            }

            
        }
       

        
    }
    private void CurretPattern()
    {
        currentAttackPattern = AttackPattern.Normal;
        InvokeRepeating("PerformAttack", 0f, normalAttackDelay);
    }
    // Lógica para realizar ataques

    private void PerformAttack()
    {
       

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
        //timer = MiniData.exhaustedRecoveryTime;
        // Cambiamos al siguiente patrón de ataque
        ChangeAttackPattern();
    }

    // Lógica para el ataque normal
    void NormalAttack()
    {
        if (MiniData.IsAttackin == true)
            return;
        MiniData.IsAttackin = true;
        Anim.SetBool("Patron1", true);
        Debug.Log("Ataque normal");     
    }

    // Lógica para el ataque especial 1
    void SpecialAttack1()
    {
        if (MiniData.IsAttackin == true)
            return;
        MiniData.IsAttackin = true;
        Anim.SetBool("Patron2", true);
        Debug.Log("Ataque especial 1");
        
    }

    // Lógica para el ataque especial 2
    void SpecialAttack2()
    {
        if (MiniData.IsAttackin == true)
            return;
        MiniData.IsAttackin = true;
        Anim.SetBool("Patron3", true);
        Debug.Log("Ataque especial 2");
    }

    // Cambia el patrón de ataque según las condiciones y estados del miniboss
    void ChangeAttackPattern()
    {
        if (MiniData.isEnraged && (MiniDetect.StayAlert = true))
        {
            // Lógica para patrones de ataque cuando el miniboss está enfurecido
            // ...
            currentAttackPattern = (AttackPattern)Random.Range(1, 3); // Selecciona aleatoriamente entre SpecialAttack1 y SpecialAttack2
        }
        else if (MiniData.isExhausted)
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
    public void Return()
    {
       
        MiniData.ReturnSpawn = true;
    }
    public void NoAttack()
    {
        MiniData.CanAttack = false;
       
    }
}
