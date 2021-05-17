using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
   /*
    protected Joystick joystick;

    public float speed = 6;
    public float jumpSpeed = 2;
    public float gravity = 20;
    public bool isgrounded = false;

    Vector3 movement;
    Quaternion rotation = Quaternion.identity;
    Vector3 moveDirection = Vector3.zero;

    public Animator animator;

    public CharacterController characterController;



    // Start is called before the first frame update
    void Start()
    {
       
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        joystick = FindObjectOfType<Joystick>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      //  Debug.Log(timerIdle);


        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if (joystick.Horizontal != 0)
        {
            v = joystick.Vertical;
            h = joystick.Horizontal;
        }


        Vector3 m = Quaternion.Euler(0, 0, 0) * new Vector3(h, 0, v).normalized;//move direction

        Vector3 dir = transform.InverseTransformDirection(m);
        float Turn = Mathf.Atan2(dir.x, dir.z);
        transform.Rotate(0, Turn * 20, 0);

        if (characterController.isGrounded)
        {

            movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            if (joystick.Horizontal != 0)
            {
                movement = new Vector3(joystick.Horizontal, movement.y, joystick.Vertical);
            }
           
            if (Input.GetKey(KeyCode.Space))
                movement.y = jumpSpeed;
        }

        else
        {
           

           // movement = new Vector3(Input.GetAxis("Horizontal"), movement.y, Input.GetAxis("Vertical"));
           
        }


        movement.y -= 3 * Time.deltaTime;
        if (joystick.Horizontal == 0) { 
        if(Mathf.Abs(h) > 0.5 && Mathf.Abs(v) > 0.5)
            {
                movement.x *= 0.8f;
                movement.z *= 0.8f;
            }
        }
        characterController.Move(movement * speed * Time.fixedDeltaTime);
        animator.SetFloat("Speed", movement.magnitude);
        

    }

    public void RunRandomIdle()
    {
        animator.Play("idle" + Random.Range(1, 4));
    }

    public void IdleEnd()
    {
        animator.Play("Blend Tree Movement");
    }


    */
}
