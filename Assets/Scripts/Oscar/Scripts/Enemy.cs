using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float attackRadius = 3f;
    public float playerAttackRange = 2.5f;
    public float movementSpeed = 3f;
    public float fleeDuration = 3f;
    private float attackCooldown = 2f;
    public float distanceBehindLeader = 2f;
    public float surroundDistance = 5f;
    public float stopFollowingDistance = 8f;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    public bool isLeader = false;
    public bool isFleeing = false;
    private Vector3 fleeTarget;
    private float originalSpeed;
    private bool isAttackOnCooldown = false;
    private bool isMyTurn = false;
    private bool isSurroundingPlayer = false;

    private static Enemy currentLeader;    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        originalSpeed = movementSpeed;

        // Obtener todos los enemigos en la escena
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        // Contar cu�ntos enemigos hay
        int totalEnemies = allEnemies.Length;

        // Asignar l�der entre 3 o 4 enemigos, solo si no hay l�der actual
        if (currentLeader == null)
        {
            isLeader = Random.Range(0, totalEnemies) < Mathf.Min(4, totalEnemies);
            currentLeader = isLeader ? this : null; // Asignar el l�der actual si este enemigo es el l�der
        }

        // Imprimir en la consola si este enemigo es el l�der
        Debug.Log(gameObject.name + " es el l�der: " + isLeader);
    }

    void Update()
    {
        if (isFleeing)
        {
            Flee();
            return;
        }

        if (isLeader)
        {
            CheckPlayerDetection();
            MoveLeader();
        }
        else
        {
            FollowLeader();
            if (isMyTurn)
            {
                if (!isSurroundingPlayer)
                {
                    Move();
                }
                else
                {
                    SurroundPlayer();
                }
            }
        }
    }

    void Move()
    {
        // Moverse hacia el jugador usando NavMeshAgent
        navMeshAgent.SetDestination(player.position);

        // Si el jugador est� dentro del rango de ataque del enemigo, atacar
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < playerAttackRange && !isAttackOnCooldown)
        {
            Attack();
        }

        // Verificar si el enemigo deber�a dejar de seguir al l�der y rodear al jugador
        if (distanceToPlayer < attackRadius)
        {
            isSurroundingPlayer = true;
        }
    }
    void MoveLeader()
    {
        // Calcular la posici�n alrededor del jugador
        Vector3 surroundPosition = CalculateSurroundPosition(player.position);

        // Moverse hacia la posici�n alrededor del jugador usando NavMeshAgent
        navMeshAgent.SetDestination(surroundPosition);

        // Si el jugador est� dentro del rango de ataque del l�der, atacar
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < playerAttackRange && !isAttackOnCooldown)
        {
            Attack();
        }
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            // Jugador detectado, proceder a atacar
            // (el ataque real se maneja en la funci�n Move)
        }
    }

    void Attack()
    {
        // Implementar l�gica de ataque, por ejemplo, reducir la salud del jugador
        Debug.Log(gameObject.name + " est� atacando al jugador");

        // Iniciar el cooldown de ataque
        isAttackOnCooldown = true;
        Invoke("ResetAttackCooldown", attackCooldown);

        // Pasar el turno al siguiente enemigo
        StartCoroutine(NextEnemyTurn());
    }

    void ResetAttackCooldown()
    {
        // Reiniciar el cooldown de ataque despu�s del tiempo de reutilizaci�n
        isAttackOnCooldown = false;
    }

    void Flee()
    {
        // Implementar l�gica de huida, moverse lejos de la �ltima posici�n conocida del jugador
        navMeshAgent.SetDestination(fleeTarget);

        // Verificar si ha transcurrido el tiempo de huida
        if (Time.time > fleeDuration)
        {
            isFleeing = false;
            isLeader = Random.Range(0f, 1f) > 0.5f; // Asignar un nuevo l�der
            movementSpeed = originalSpeed; // Restablecer la velocidad de movimiento
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isLeader && collision.gameObject.tag == "Player")
        {
            // El l�der fue asesinado, iniciar la huida
            isFleeing = true;
            fleeTarget = transform.position + Random.onUnitSphere * 10f; // Huir en una direcci�n aleatoria
            movementSpeed *= 2; // Aumentar la velocidad durante la huida
        }
    }
    public bool IsLeader()
    {
        return isLeader;
    }
    void FollowLeader()
    {
        if (currentLeader != null)
        {
            // Calcular la posici�n alrededor del jugador, evitando colisiones
            Vector3 targetPosition = CalculateSurroundPosition(player.position);

            // Moverse hacia la posici�n alrededor del jugador usando NavMeshAgent
            navMeshAgent.SetDestination(targetPosition);
        }       
    }
    Vector3 CalculateSurroundPosition(Vector3 targetPosition)
    {
        // Calcular la posici�n alrededor del objetivo, evitando colisiones con otros seguidores
        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = 0; // Asegurar que la direcci�n est� en el plano horizontal

        // Calcular la posici�n objetivo alrededor del jugador
        Vector3 surroundPosition = targetPosition + randomDirection * distanceBehindLeader;

        // Utilizar NavMesh.SamplePosition para encontrar una posici�n v�lida en el NavMesh
        NavMeshHit hit;
        NavMesh.SamplePosition(surroundPosition, out hit, distanceBehindLeader, NavMesh.AllAreas);

        return hit.position;
    }
    void SurroundPlayer()
    {
        // Calcular la posici�n alrededor del jugador
        Vector3 surroundPosition = player.position + Random.onUnitSphere * surroundDistance;

        // Moverse hacia la posici�n alrededor del jugador usando NavMeshAgent
        navMeshAgent.SetDestination(surroundPosition);

        // Si el jugador est� fuera del rango de ataque, dejar de rodearlo
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > playerAttackRange)
        {
            isSurroundingPlayer = false;
        }
    }
    void AttackPlayer()
    {
        // Realizar ataque solo si no est� en cooldown
        if (!isAttackOnCooldown)
        {
            // Implementar l�gica de ataque, por ejemplo, reducir la salud del jugador
            Debug.Log("Atacando al jugador");

            // Iniciar el cooldown de ataque
            isAttackOnCooldown = true;
            Invoke("ResetAttackCooldown", attackCooldown);

            // Pasar el turno al siguiente enemigo
            StartCoroutine(NextEnemyTurn());
        }
    }
    IEnumerator NextEnemyTurn()
    {
        // Esperar un breve tiempo antes de pasar el turno al siguiente enemigo
        yield return new WaitForSeconds(1f);

        // Buscar todos los enemigos en la escena
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        // Encontrar el �ndice de este enemigo en la lista
        int myIndex = System.Array.IndexOf(allEnemies, this);

        // Calcular el �ndice del pr�ximo enemigo en la lista (circularmente)
        int nextIndex = (myIndex + 1) % allEnemies.Length;

        // Establecer el turno al pr�ximo enemigo
        allEnemies[nextIndex].isMyTurn = true;
        isMyTurn = false;
    }
    IEnumerator AttackPlayerTurn()
    {
        // Realizar ataque solo si no est� en cooldown
        if (!isAttackOnCooldown)
        {
            // Implementar l�gica de ataque, por ejemplo, reducir la salud del jugador
            Debug.Log(gameObject.name + " est� atacando al jugador");

            // Iniciar el cooldown de ataque
            isAttackOnCooldown = true;
            Invoke("ResetAttackCooldown", attackCooldown);

            // Pasar el turno al siguiente enemigo despu�s de un breve tiempo
            yield return new WaitForSeconds(1f);

            // Buscar todos los enemigos en la escena
            Enemy[] allEnemies = FindObjectsOfType<Enemy>();

            // Encontrar el �ndice de este enemigo en la lista
            int myIndex = System.Array.IndexOf(allEnemies, this);

            // Calcular el �ndice del pr�ximo enemigo en la lista (circularmente)
            int nextIndex = (myIndex + 1) % allEnemies.Length;

            // Establecer el turno al pr�ximo enemigo
            allEnemies[nextIndex].isMyTurn = true;
            isMyTurn = false;
        }
    }
    void CheckPlayerDetection()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            // Jugador detectado, proceder a atacar
            if (distanceToPlayer < playerAttackRange && !isAttackOnCooldown)
            {
                StartCoroutine(AttackPlayerTurn());
            }
        }
    }
}