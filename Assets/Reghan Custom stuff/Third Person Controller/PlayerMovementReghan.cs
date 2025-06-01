using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementReghan : MonoBehaviour{

    public Animator anim;
    public float rotSpeed = 145f;
    public float moveSpeed = 1f;
    public float sprintSpeed = 2f;
    public float jumpHeight = 3f;
    private Rigidbody rb;

    //private bool canJump = true;
    //private float jumpCooldown = 2f;
    //private float jumpTimer = 0f;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.angularDamping = 10f;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        anim.applyRootMotion = false;
    }

    // Update is called once per frame
    void Update(){

        ForwardMovement();

        Turning();

        Actions();
        CheckIfKnockedOver();
    }

    private void ForwardMovement(){
        float moveDirectionZ = 0f;
        if(Input.GetKey("w")){
            anim.SetBool("Walking", true);
            if (Input.GetKey(KeyCode.LeftShift)){
                anim.SetBool("Running", true);
                moveDirectionZ = sprintSpeed;
            } else{
                anim.SetBool("Running", false);
                moveDirectionZ = moveSpeed;
            }
        } else if (Input.GetKeyUp("w")) {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
        }
        Vector3 move = transform.forward * moveDirectionZ * Time.deltaTime;
        rb.MovePosition(rb.position + move);
    }

    private void Turning(){
        if (Input.GetKey("a")) {
            transform.Rotate(0, -rotSpeed * 15 * Time.deltaTime, 0, Space.World);
            anim.SetBool("Turn Left", true);
        } else if (Input.GetKey("d")) {
            transform.Rotate(0, rotSpeed * 15 * Time.deltaTime, 0, Space.World);
            anim.SetBool("Turn Right", true);
        } else {
            anim.SetBool("Turn Left", false);
            anim.SetBool("Turn Right", false);
        }
    }

    private void Actions(){
        if(Input.GetKeyDown("e")){
            anim.SetBool("Waving", true);
        } else if(Input.GetKeyUp("e")){
            anim.SetBool("Waving", false);
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
