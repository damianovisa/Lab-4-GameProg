using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public Vector3 gravity;
    public Vector3 playerVelocity;

    public bool isOnGround = false;
    public float gravityValue = -9.81f;

    public float walkSpeed = 5;
    public float runSpeed = 8; 
    

    private CharacterController controller;
    private Animator animator;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    public void Update()
    {
        ProcessMovement();
    }
    public void LateUpdate()
    {
       
       UpdateAnimator();
       
    }
    void DisableRootMotion()
    {
        animator.applyRootMotion = false;  
    }
    void UpdateAnimator()
    {
        isOnGround = controller.isGrounded; 
        // TODO 
        Vector3 characterXandZmotion = new Vector3(playerVelocity.x,0.0f,playerVelocity.z);
        if(Mathf.Abs(Input.GetAxis("Horizontal"))>0.0f || Mathf.Abs(Input.GetAxis("Vertical"))>0.0f){
            if(Input.GetButton("Fire3")){//Left Shift
                animator.SetFloat("Speed",1.0f);
            }else{
                animator.SetFloat("Speed",0.5f);
            }
        }else{
            animator.SetFloat("Speed",0.0f);
        }
        if(Input.GetButtonUp("Fire1")){
            animator.applyRootMotion = true;
            animator.SetTrigger("DoRoll");
        }
        animator.SetBool("IsGrounded",isOnGround);
    }

    void ProcessMovement()
    { 
        // Moving the character foward according to the speed
        float speed = GetMovementSpeed();

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Turning the character
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        playerVelocity =  move * Time.deltaTime * speed;
        // Making sure we dont have a Y velocity if we are grounded
        // controller.isGrounded tells you if a character is grounded ( IE Touches the ground)
       
        
        

        // Since there is no physics applied on character controller we have this applies to reapply gravity
        playerVelocity.y = gravityValue * Time.deltaTime;
        
        controller.Move(playerVelocity);
        isOnGround = controller.isGrounded; 

    }

    float GetMovementSpeed()
    {
        if (Input.GetButton("Fire3"))// Left shift
        {
            return runSpeed;
        }
        else
        {
            return walkSpeed;
        }
    }
}
