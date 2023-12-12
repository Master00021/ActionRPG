using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController playerCC;
    public Animator playerAnim;

    //cam move
    public Transform cam;

    //movement
    float horizontal;
    float vertical;

    bool walking = false;

    Vector3 direction;

    public float speed;
    private float turntime = 4f;

    public float sprint;


    //
    public Stamina staminacontroller;


    // Start is called before the first frame update
    void Start()
    {
        staminacontroller = GetComponent<Stamina>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0, vertical);

        direction = cam.forward * direction.z + cam.right * direction.x;
        direction.y = 0f;
        playerCC.Move(direction * Time.deltaTime * speed);

        if (direction != Vector3.zero)
        {
            
            float targetangle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetangle, 0f);

            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turntime);

            playerAnim.SetInteger("Movement", 1);

            walking = true;
        }
        else if(direction == Vector3.zero)
        {
            playerAnim.SetInteger("Movement", 0);
            walking = false;
        }


        walking = !Input.GetKeyDown(KeyCode.LeftShift);

        if(walking == true)
        {
            staminacontroller.isSprinting = false;
        }

        if(!walking && playerCC.velocity.sqrMagnitude > 0)
        {
            if(staminacontroller.playerSt > 0)
            {
                walking = false;
                staminacontroller.isSprinting = true;
                staminacontroller.Sprinting();
            }
            else
            {
                walking = true;
            }
        }
    }
    public void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            walking = false;
            playerCC.Move(direction * Time.deltaTime * sprint);
            playerAnim.SetInteger("Movement", 2);
        }
    }

    public void SprintSpeed(float Mspeed)
    {
        sprint = Mspeed;
    }
}
