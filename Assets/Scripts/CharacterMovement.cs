using System.Collections;
//using DG.Tweening;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    public bool isGrounded;
    public float jumpHeight = 10;

    
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    public Vector3 direction;

    public int runningSpeed = 6;
    public int walkingSpeed = 3;
    [SerializeField] private CharacterAnimationController characterAnimationController;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float jump = Input.GetAxisRaw("Jump");

        direction = new Vector3(horizontal, jump, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
                characterAnimationController.PlayJump();
            }

            // if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
            // {
            //     //primalAnimationController.PlayBackflip();
            //
            //     // StartCoroutine(Jump());
            //     // IEnumerator Jump()
            //     // {
            //     //     yield return new WaitForSeconds(0.5f);
            //     //     velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            //     // }
            // }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runningSpeed;
                characterAnimationController.PlayRun();
            }
            else
            {
                speed = walkingSpeed;
                characterAnimationController.PlayWalk();
            }
        }
        else
        {
            characterAnimationController.PlayIdle();
        }

        if (!isGrounded)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            //isStabbing = true;
            //primalAnimationController.PlayStabbingOne();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //isStabbing = true;
            //primalAnimationController.PlayStabbingTwo();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            //isStabbing = true;
            //primalAnimationController.PlayStabbingThree();
        }
        
    }

    //calling from animation
    public void DisableStabbing()
    {
        //isStabbing = false;
    }
}