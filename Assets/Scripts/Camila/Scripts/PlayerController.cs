 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public StaminaControl stamina;

    public CharacterController playerCC;
    public Animator playerANIM;

    //cam movement
    public Transform cam;

    //movement
    float horizontal;
    float vertical;

    Vector3 direction;

    public bool walking;

    public float speed;
    private float turntime = 4f;

    //+
    public float sprint;

    //dash
    public float dashforce = 10f;
    public float dashtime = 0.5f;
    public bool dashing = false;


    public bool parry = false;

    //vida


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0, vertical);

        direction = cam.forward * direction.z + cam.right * direction.x;
        direction.y = 0f;
        playerCC.Move(direction * Time.deltaTime * speed);

        if (walking = true && direction != Vector3.zero)
        {
            float targetangle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetangle, 0f);

            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turntime);

        }
        else if (direction == Vector3.zero)
        {
            playerANIM.SetInteger("Movement", 0);
        }

        //sprint
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    walking = false;
        //    stamina.SprintingMove();
        //    playerCC.Move(direction * Time.deltaTime * sprint);
        //    //playerANIM.SetBool("Running", true);

        //    stamina.sprinting = true;

        //    //baja stamina
        //}
        //else
        //{
        //    walking = true;
        //    playerCC.Move(direction * Time.deltaTime * speed);
        //    //playerANIM.SetBool("Running", false);
        //    stamina.sprinting = false;

        //}

        if(walking == true)
        {
            stamina.sprinting = false;
            playerANIM.SetInteger("Movement", 1);

        }

        if (!walking && playerCC.velocity.sqrMagnitude > 0)
        {
            if (stamina.playerstamina > 0)
            {
                walking = false;
                stamina.sprinting = true;
                stamina.SprintingMove();
            }
            else
            {
                walking = true;
                playerANIM.SetInteger("Movement", 1);
            }
        }


        //dash
        //si el mov es distinto a 0 gasta stamina y si se está moviendo y además sprint, se gasta más stamina, si sprint y no se mueve, entonces no gasta
        if (Input.GetKeyDown(KeyCode.Space) && (horizontal != 0 || vertical != 0) && dashing == false)
        {
            stamina.ActionCost();
        }
        

        //parry

        if (Input.GetButton("Fire2"))
        {
            stamina.ActionCost();
            playerANIM.SetBool("Block", true);
            
        }
        else
        {
            playerANIM.SetBool("Block", false);
            parry = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            stamina.SprintingMove();
        }



    }

    public void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            walking = false;
            //stamina.SprintingMove();
            playerCC.Move(direction * Time.deltaTime * sprint);
            playerANIM.SetInteger("Movement", 2);

            //stamina.sprinting = true;

            //baja stamina
        }
        //else
        //{
        //    walking = true;
        //    playerCC.Move(direction * Time.deltaTime * speed);
        //    playerANIM.SetBool("Running", false);
        //    stamina.sprinting = false;

        //}
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (horizontal != 0 || vertical != 0) && dashing == false)
        {
            dashing = true;

            Vector3 velocity = playerCC.velocity;

            Vector3 dashvector = transform.forward * dashforce;
            playerCC.Move(dashvector * Time.deltaTime);

            playerCC.Move(velocity * Time.deltaTime);
            dashing = false;
        }
    }

    public void Block()
    {
        

        //cuando esta bloqueando y le pegan, resta una cantidad determinada de estamina, de lo contrario, quita vida.
    }

}
