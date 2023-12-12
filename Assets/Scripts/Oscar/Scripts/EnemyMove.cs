using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float radioDeteccion = 10f;
    public float distanciaAtaque = 2f;
    public float velocidadCaminar = 2.5f;
    public float velocidadCorrer = 5f;
    public int vidaMaxima = 50;
    private int vidaActual;

    public bool EsLider;
    public static GameObject leader;
    public static List<EnemyMove> enemigosConCodigo = new List<EnemyMove>();

    public GameObject player;
    private Animator animator;
    private Quaternion angulo;

    private bool atacando;
    private float cronometro;
    private int rutina;
    private float grado;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        vidaActual = vidaMaxima;
        if (!EsLider)
        {
            SeleccionarLider();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento();
    }

    void Comportamiento()
    {
        if (leader != null)
        {
            RutinaMovimiento();

            // Enemigos siguen al líder
            if (Vector3.Distance(transform.position, leader.transform.position) > 1)
            {
                MoverHaciaLider();
            }
            else
            {
                DetenerMovimiento();
            }

            // Enemigos atacan al jugador si está dentro del área de detección
            if (Vector3.Distance(transform.position, player.transform.position) <= radioDeteccion)
            {
                ManejarAtaque();
            }
        }
    }

    void MoverHaciaLider()
    {
        var lookPos = leader.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

        animator.SetBool("walk", true);
        animator.SetBool("run", false);

        transform.Translate(Vector3.forward * velocidadCaminar * Time.deltaTime);
    }

    void DetenerMovimiento()
    {
        animator.SetBool("walk", false);
        animator.SetBool("run", false);
    }

    void ManejarAtaque()
    {
        // Enemigos atacan al jugador si está dentro de la distancia de ataque
        if (Vector3.Distance(transform.position, player.transform.position) > distanciaAtaque)
        {
            MoverHaciaJugador();
        }
        else
        {
            DetenerMovimiento();
            animator.SetBool("attack", true);
            atacando = true;
        }
    }

    void MoverHaciaJugador()
    {
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

        animator.SetBool("walk", false);
        animator.SetBool("run", true);

        transform.Translate(Vector3.forward * velocidadCorrer * Time.deltaTime);

        animator.SetBool("attack", false);
    }

    void RutinaMovimiento()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 7)
        {
            DetenerMovimiento();
            cronometro += Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    animator.SetBool("walk", false);
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);                 
                    rutina++;
                    MoverHaciaAngulo();
                    break;
                case 2:
                    MoverHaciaAngulo();
                    break;
            }
        }
        else
        {
            DetenerMovimiento();
        }
    }
    void MoverHaciaAngulo()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
        transform.Translate(Vector3.forward * velocidadCaminar * Time.deltaTime);
        animator.SetBool("walk", true);
    }
    public void RecibirDaño(int cantidadDanio)
    {
        if (vidaActual > 0)
        {
            vidaActual -= cantidadDanio;

            if (vidaActual <= 0)
            {
                vidaActual = 0;
                Morir();
            }
        }
    }

    void SeleccionarLider()
    {
        // Encuentra todos los enemigos en la escena
        EnemyMove[] enemigos = FindObjectsOfType<EnemyMove>();

        // Si hay al menos un enemigo en la escena, selecciona aleatoriamente uno como líder
        if (enemigos.Length > 0)
        {
            int indiceLider = Random.Range(0, enemigos.Length);
            for (int i = 0; i < enemigos.Length; i++)
            {
                enemigos[i].EsLider = (i == indiceLider);
            }

            // Completa el targetLider una vez que se ha identificado el líder
            foreach (var enemigo in enemigos)
            {
                if (enemigo.EsLider)
                {
                    leader = enemigo.gameObject;
                    break;
                }
            }
        }
        player = GameObject.Find("Player");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger detectado con el jugador");
            RecibirDaño(10); // Puedes ajustar la cantidad de daño según tus necesidades
        }
    }

    public void FinalAnim()
    {
        animator.SetBool("attack", false);
        atacando = false;
    }
    void Morir()
    {
        DetenerMovimiento();
        animator.SetBool("die", true);
    }
}