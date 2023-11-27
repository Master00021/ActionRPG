using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossData : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float energy = 100f;
    public float maxEnergy = 100f;
    public float Velocity;
    public float damageThresholdForEnrage = 20f;
    public float enrageDuration = 10f;
    public float exhaustedRecoveryTime = 5f;
    public float damageThresholdForKnockdown = 50f;
    public float knockdownDuration = 5f;

    public bool IsAttackin;
    public bool ReturnSpawn;
    public bool InSpawn;
    public bool CanAttack;
    public bool isEnraged = false;
    public bool isExhausted = false;
    public bool isKnockedDown = false;

    // Lógica de inicialización
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    // Lógica de actualización por cada frame
    void Update()
    {
        if(currentHealth <= 50f)
        {
            isEnraged = true;
        }
        if (isKnockedDown)
        {
            // Lógica de estar derribado
            // ...

            // Comprobamos si el tiempo de derribo ha terminado
            if (Time.time >= knockdownDuration)
            {
                isKnockedDown = false;
                // Lógica para levantarse después de estar derribado
                // ...
            }
        }
        else if (isEnraged)
        {
            // Lógica de estar enfurecido
            // ...


            // Comprobamos si el tiempo de enfurecimiento ha terminado
            if (Time.time >= enrageDuration)
            {
                isEnraged = false;
                // Lógica para volver al estado normal después de enfurecerse
                // ...
            }
        }
        else if (isExhausted)
        {
            // Lógica de estar agotado
            // ...

            // Comprobamos si la energía se ha recuperado completamente
            if (energy >= maxEnergy)
            {
                isExhausted = false;
                // Lógica para volver al estado normal después de recuperar la energía
                // ...
            }
        }
        else
        {
            // Lógica de comportamiento normal del miniboss
            // ...

            // Comprobamos si el miniboss debería entrar en estado enfurecido
            if (currentHealth <= damageThresholdForEnrage)
            {
                Enrage();
            }

            // Comprobamos si el miniboss debería entrar en estado agotado
            if (energy <= 0)
            {
                Exhaust();
            }
        }
    }

    // Lógica para recibir daño
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Comprobamos si el miniboss debería ser derribado
        if (currentHealth <= damageThresholdForKnockdown)
        {
            Knockdown();
        }
    }

    // Lógica para entrar en estado enfurecido
    void Enrage()
    {
        isEnraged = true;
        // Lógica de entrar en estado enfurecido
        // ...
    }

    // Lógica para entrar en estado agotado
    void Exhaust()
    {
        isExhausted = true;
        energy = 0; // Puedes ajustar cómo gestionas la energía aquí
        // Lógica de entrar en estado agotado
        // ...
    }

    // Lógica para ser derribado
    void Knockdown()
    {
        isKnockedDown = true;
        currentHealth = 0; // Puedes ajustar cómo gestionas el daño aquí
        // Lógica de ser derribado
        // ...
    }
}

