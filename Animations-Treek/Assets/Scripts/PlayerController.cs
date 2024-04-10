//////////////////////////////////////////////
//Assignment/Lab/Project: Animations_Treek
//Name: Ahmed Treek
//Section: SGD.213.0021
//Instructor: Aurore Locklear
//Date: 4/4/2024
/////////////////////////////////////////////


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Vector3 move;
    private CharacterController character;
    private float runSpeed = 5;
    private float walkSpeed = 3f;
    [SerializeField] private Animator door;
    private float verticalVelocity;
    private float gravity = 9.8f;
    private float jumpForce = 5.0f;
    private bool canJump;

    void Start()
    {
        animator = GetComponent<Animator>();
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        move = new Vector3(horizontalInput, 0, verticalInput);

        if(move.magnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                character.Move(move * runSpeed * Time.deltaTime);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
            }
            else
            {
                character.Move(move * walkSpeed * Time.deltaTime);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }

       

        if(Input.GetKey(KeyCode.Space))
        {
            animator.SetTrigger("isJumping");
        }

        if (Input.GetKey(KeyCode.E))
        {
            animator.SetTrigger("isDancing");
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DoorTrigger")
        {
            door.SetTrigger("doorIsOpen");
        }
    }
}
