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
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private CharacterController character;
    private float runSpeed = 5;
    private float walkSpeed = 3f;
    public float gravityValue = -30.81f;
    private float jumpHeight = .02f;
    private Vector3 playerVelocity;
    [SerializeField] private Animator door;
   
    void Start()
    {
        animator = GetComponent<Animator>();
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //gets the horizontal and vertical inputs
        float verticalInput = Input.GetAxis("Vertical");
        move = new Vector3(horizontalInput, 0, verticalInput); //calculates when the player moves

        if(move.magnitude > 0) //if the the player is moving
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                character.Move(move * runSpeed * Time.deltaTime); //when the player holds the left shift button, calculates the run speed
                animator.SetBool("isWalking", false); //sets the walking animation to false
                animator.SetBool("isRunning", true); //sets the running animation to true
            }
            else
            {
                character.Move(move * walkSpeed * Time.deltaTime); //calculates the walk speed
                animator.SetBool("isRunning", false); //sets the running animation to false
                animator.SetBool("isWalking", true); //sets the walking animation to true
            }
        }
        else
        {
            animator.SetBool("isWalking", false); //sets walk and run animation to false
            animator.SetBool("isRunning", false);
        }

        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer); //calculates the check if the player is grounded

        if (Input.GetKey(KeyCode.Space) && isGrounded) //if the player presses space and is grounded
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue); //calculates the jump
            animator.SetTrigger("isJumping"); //triggers the jump animation
        }

        playerVelocity.y += gravityValue * Time.deltaTime; //applies the gravity
        character.Move(playerVelocity * Time.deltaTime);// player jumps

        if (Input.GetKey(KeyCode.E))
        {
            animator.SetTrigger("isDancing"); //when the player presses E, the dance animation is triggered
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit(); //if the player presses escape, the game closes
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DoorTrigger")
        {
            door.SetTrigger("doorIsOpen"); //when the player enters the trigger box, trigger the door open animaton.
        }
    }
}
