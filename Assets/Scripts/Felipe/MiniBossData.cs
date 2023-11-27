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

    // L�gica de inicializaci�n
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    // L�gica de actualizaci�n por cada frame
    void Update()
    {
        if(currentHealth <= 50f)
        {
            isEnraged = true;
        }
        if (isKnockedDown)
        {
            // L�gica de estar derribado
            // ...

            // Comprobamos si el tiempo de derribo ha terminado
            if (Time.time >= knockdownDuration)
            {
                isKnockedDown = false;
                // L�gica para levantarse despu�s de estar derribado
                // ...
            }
        }
        else if (isEnraged)
        {
            // L�gica de estar enfurecido
            // ...


            // Comprobamos si el tiempo de enfurecimiento ha terminado
            if (Time.time >= enrageDuration)
            {
                isEnraged = false;
                // L�gica para volver al estado normal despu�s de enfurecerse
                // ...
            }
        }
        else if (isExhausted)
        {
            // L�gica de estar agotado
            // ...

            // Comprobamos si la energ�a se ha recuperado completamente
            if (energy >= maxEnergy)
            {
                isExhausted = false;
                // L�gica para volver al estado normal despu�s de recuperar la energ�a
                // ...
            }
        }
        else
        {
            // L�gica de comportamiento normal del miniboss
            // ...

            // Comprobamos si el miniboss deber�a entrar en estado enfurecido
            if (currentHealth <= damageThresholdForEnrage)
            {
                Enrage();
            }

            // Comprobamos si el miniboss deber�a entrar en estado agotado
            if (energy <= 0)
            {
                Exhaust();
            }
        }
    }

    // L�gica para recibir da�o
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Comprobamos si el miniboss deber�a ser derribado
        if (currentHealth <= damageThresholdForKnockdown)
        {
            Knockdown();
        }
    }

    // L�gica para entrar en estado enfurecido
    void Enrage()
    {
        isEnraged = true;
        // L�gica de entrar en estado enfurecido
        // ...
    }

    // L�gica para entrar en estado agotado
    void Exhaust()
    {
        isExhausted = true;
        energy = 0; // Puedes ajustar c�mo gestionas la energ�a aqu�
        // L�gica de entrar en estado agotado
        // ...
    }

    // L�gica para ser derribado
    void Knockdown()
    {
        isKnockedDown = true;
        currentHealth = 0; // Puedes ajustar c�mo gestionas el da�o aqu�
        // L�gica de ser derribado
        // ...
    }
}

