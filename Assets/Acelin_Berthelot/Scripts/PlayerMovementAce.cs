using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator anim;
    public float rotSpeed = 145f;
    public float moveSpeed = 1f;
    public float sprintSpeed = 2f;
    public float jumpHeight = 3f;
    private Rigidbody rb;

    private bool canJump = true;  
    private float jumpCooldown = 2f;  
    private float jumpTimer = 0f;  

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.angularDamping = 10f;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        anim.applyRootMotion = false;  
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleJumpCooldown();
        CheckIfKnockedOver();
    }

    private void HandleMovement()
{
    float moveDirectionZ = 0f;

    // Check if the player is pressing "W" to move forward
    if (Input.GetKey("w"))
    {
        // If LeftShift is NOT held, it's walking
        if (!Input.GetKey(KeyCode.LeftShift)) // LeftShift is false
        {
            anim.SetBool("Walking", true);  // Trigger Walking animation
            anim.SetBool("Running", false); // Ensure Running is off
            moveDirectionZ = moveSpeed;    // Set movement speed to walking speed
        }
        else // If LeftShift is held, it's running
        {
            anim.SetBool("Walking", false); // Ensure Walking is off
            anim.SetBool("Running", true);  // Trigger Running animation
            moveDirectionZ = sprintSpeed;  // Set movement speed to sprint speed
        }
    }
    else
    {
        anim.SetBool("Walking", false);  // If "W" is not pressed, reset Walking animation
        anim.SetBool("Running", false);  // If "W" is not pressed, reset Running animation
    }

    // Apply movement using Rigidbody (move the character with physics)
    Vector3 move = transform.forward * moveDirectionZ * Time.deltaTime;
    rb.MovePosition(rb.position + move);

    // Handle turning (left and right)
    if (Input.GetKey("a"))
    {
        transform.Rotate(0, -rotSpeed * Time.deltaTime, 0, Space.World);
        anim.SetBool("Turn Left", true);
    }
    else if (Input.GetKey("d"))
    {
        transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.World);
        anim.SetBool("Turn Right", true);
    }
    else
    {
        anim.SetBool("Turn Left", false);
        anim.SetBool("Turn Right", false);
    }
}



    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
            canJump = false; 
            anim.SetTrigger("Jump");
            jumpTimer = 0f;  
        }
    }

    private void HandleJumpCooldown()
    {
        if (!canJump)
        {
            jumpTimer += Time.deltaTime;

            if (jumpTimer >= jumpCooldown)
            {
                canJump = true; 
            }
        }
    }


    private bool knockedOver = false;

    private void CheckIfKnockedOver()
    {
        float xRotation = Mathf.Abs(transform.rotation.eulerAngles.x);
        float zRotation = Mathf.Abs(transform.rotation.eulerAngles.z);

        if ((xRotation > 80f || zRotation > 80f) && !knockedOver)
        {
            knockedOver = true;
            ResetRotation();
        }
        else if (knockedOver)
        {
            knockedOver = false;
        }
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
