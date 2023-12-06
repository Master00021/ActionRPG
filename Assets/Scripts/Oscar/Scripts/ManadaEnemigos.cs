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
        // Al inicio del juego, seleccionamos al líder si no hay uno asignado
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
            // Líder: Realizar acciones específicas del líder aquí
        }
        else
        {
            // No líder: Moverse dentro del rango del líder
            MoveWithinLeaderRange();
        }
    }

    private void MoveWithinLeaderRange()
    {
        if (leader != null)
        {
            // Obtener el script Comportamiento del líder
            EnemyMove leaderBehavior = leader.GetComponent<EnemyMove>();

            if (leaderBehavior != null)
            {
                // Moverse dentro del rango del líder tomando en cuenta la lógica del script Comportamiento
                if (Vector3.Distance(transform.position, leader.transform.position) > leaderBehavior.leaderRadius)
                {
                    // Moverse hacia el líder si está fuera del rango
                    Vector3 moveDirection = leader.transform.position - transform.position;
                    moveDirection.y = 0; // Ignorar cambios en la altura
                    moveDirection.Normalize();

                    // Ajustar la velocidad según tus necesidades
                    float moveSpeed = 5f;
                    transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
                }
            }
        }
    }
}