using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    GameObject leftStick;


    Joystick joystick;
    Animator animator;
    CharacterController characterController;

    double randomIdleTimer = 15;
    double randomIdleTimerCurrent;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        joystick = leftStick.GetComponent<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        if(characterController.velocity.magnitude <= .1)
        {
            if (randomIdleTimerCurrent <= 0) RunRandomIdle();
            else randomIdleTimerCurrent -= Time.deltaTime;
        }
        Vector3 NextDir = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        if (NextDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(NextDir);   
        }
        characterController.Move(NextDir / 12);
        animator.SetFloat("Speed", characterController.velocity.magnitude);
    }

    public void RunRandomIdle()
    {
        randomIdleTimerCurrent = randomIdleTimer;
        animator.Play("idle" + Random.Range(1, 4));
    }
}
