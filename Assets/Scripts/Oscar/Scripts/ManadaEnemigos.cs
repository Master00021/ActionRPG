using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManadaEnemigos : MonoBehaviour
{
    public bool isLeader = false;
    public float leaderRadius = 7f;

    private static GameObject leader;

    private void Start()
    {
        // Al inicio del juego, seleccionamos al l�der si no hay uno asignado
        if (leader == null)
        {
            leader = gameObject;
            isLeader = true;
        }
    }

    private void Update()
    {
        if (isLeader)
        {
            // L�der: Realizar acciones espec�ficas del l�der aqu�
        }
        else
        {
            // No l�der: Moverse dentro del rango del l�der
            MoveWithinLeaderRange();
        }
    }

    private void MoveWithinLeaderRange()
    {
        if (leader != null)
        {
            // Obtener el script Comportamiento del l�der
            EnemyMove leaderBehavior = leader.GetComponent<EnemyMove>();

            if (leaderBehavior != null)
            {
                // Moverse dentro del rango del l�der tomando en cuenta la l�gica del script Comportamiento
                if (Vector3.Distance(transform.position, leader.transform.position) > leaderBehavior.leaderRadius)
                {
                    // Moverse hacia el l�der si est� fuera del rango
                    Vector3 moveDirection = leader.transform.position - transform.position;
                    moveDirection.y = 0; // Ignorar cambios en la altura
                    moveDirection.Normalize();

                    // Ajustar la velocidad seg�n tus necesidades
                    float moveSpeed = 5f;
                    transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
                }
            }
        }
    }
}