using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suntail{
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag; 
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    private Animator animator;
    private CharacterController characterController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
        readyToJump = true;

    }

    private void Update()
    {
        //ground check
        //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight*0.5f+0.2f,whatIsGround);
        grounded = Physics.Raycast(transform.position, Vector3.down,whatIsGround);

        MyInput();
        SpeedControl();

        //handle drag
        if(grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
        
        if(Input.GetKey(KeyCode.LeftShift)){
            animator.SetBool("isRunning",true);
                moveSpeed = 9;
        }
        else{
            animator.SetBool("isRunning",false);
            if(moveSpeed == 9.0){
                moveSpeed = 4;
            } 
        }

        if(Input.GetKey(KeyCode.T)){
            animator.SetBool("isDance",true);
        }
        else{
            animator.SetBool("isDance",false);
        }

    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump

        if(Input.GetKey(jumpKey) && readyToJump) // && grounded
        {
            readyToJump = false;
            animator.SetBool("isJumping",true);
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown); 
        }
        else{
            animator.SetBool("isJumping",false);
        }
    }
    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if(moveDirection != Vector3.zero)
        {
            animator.SetBool("isMoving",true);
            //on grounded
            if(grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
            else if(!grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }
        }
        else
        {
            animator.SetBool("isMoving",false);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
    
}
}

