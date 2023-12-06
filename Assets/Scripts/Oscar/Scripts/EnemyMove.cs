using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public float grado;
    public bool atacando;

    public GameObject target;
    Animator animator;
    public Quaternion angulo;

    public float leaderRadius = 7f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento();
    }
    public void Comportamiento()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 7)
        {
            animator.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
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
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 2.5f * Time.deltaTime);
                    animator.SetBool("walk", true);
                    break;

            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 2 && !atacando)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                animator.SetBool("walk", false);

                animator.SetBool("run", true);
                transform.Translate(Vector3.forward * 5 * Time.deltaTime);

                animator.SetBool("attack", false);
            }
            else
            {
                animator.SetBool("walk", false);
                animator.SetBool("run", false);

                animator.SetBool("attack", true);
                atacando = true;
            }
        }
    }

    public void FinalAnim()
    {
        animator.SetBool("attack", false);
        atacando = false;
    }
}