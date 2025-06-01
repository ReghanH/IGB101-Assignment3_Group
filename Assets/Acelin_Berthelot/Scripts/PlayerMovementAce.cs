using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAce : MonoBehaviour
{
    public Animator anim;
    public float rotSpeed = 145f;
    public float moveSpeed = 1f;
    public float sprintSpeed = 2f;
    public float jumpHeight = 3f;
    private Rigidbody rb;

    private bool canJump = true;
    private bool isJumping = false;
    private bool isFalling = false;
    private float jumpStateTimer = 0f;
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

        if (!isGrounded && !isFalling && rb.linearVelocity.y < -1f)


        {
            isFalling = true;
            anim.SetTrigger("Falling");
        }

        if (isGrounded && isFalling)
        {
            anim.SetTrigger("JumpLand");
            ResetJumpState();
        }

    }

    private void HandleMovement()
    {
        float moveDirectionZ = 0f;

        if (Input.GetKey("w"))
        {
            if (!Input.GetKey(KeyCode.LeftShift)) // Walking
            {
                anim.SetBool("Walking", true);
                anim.SetBool("Running", false);
                moveDirectionZ = moveSpeed;
            }
            else // Running
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Running", true);
                moveDirectionZ = sprintSpeed;
            }

            anim.SetBool("WalkBack", false); // Ensure WalkBack is off
        }
        else if (Input.GetKey("s")) // Walking backward
        {
            anim.SetBool("WalkBack", true);
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);

            Vector3 moveBack = -transform.forward * (moveSpeed * 0.5f) * Time.deltaTime;
            rb.MovePosition(rb.position + moveBack);
        }
        else // No movement keys
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("WalkBack", false);
        }

        // Only apply forward movement (W)
        if (Input.GetKey("w"))
        {
            Vector3 move = transform.forward * moveDirectionZ * Time.deltaTime;
            rb.MovePosition(rb.position + move);
        }

        // Turning
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

        if (!Input.GetKey("w") && !Input.GetKey("s"))
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("WalkBack", false);
        }

    }




    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
            canJump = false;
            isJumping = true;
            jumpStateTimer = 0f;

            if (anim.GetBool("Running"))
            {
                anim.SetTrigger("RunJump");
            }
            else if (anim.GetBool("Walking"))
            {
                anim.SetTrigger("WalkJump");
            }
            else
            {
                anim.SetTrigger("IdleJump");
            }
        }

        if (isJumping)
        {
            jumpStateTimer += Time.deltaTime;

        }
    }

    
private void ResetJumpState()
    {
        isJumping = false;
        isFalling = false;
        jumpTimer = 0f;
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

    private bool isGrounded = false;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
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
