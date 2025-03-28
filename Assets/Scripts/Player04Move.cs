using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player04Move : MonoBehaviour
{

    private Vector3 velocity;
    private Vector3 playerMovementInput;
    private Vector3 playerMouseInput;
    private float xRotate;

    [SerializeField] private Transform playerCamera;
    [SerializeField] private CharacterController controller;
    [Space]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float sensitivity;
    [SerializeField] private float gravity = -9.81f;

    void Start() { 

    }
    void Update()
    {
        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(playerMovementInput);
        if (controller.isGrounded)
        {
            velocity.y = -1f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = jumpForce;
            }
        }
        else
        {
            velocity.y -= gravity * -2F * Time.deltaTime;
        }
        controller.Move(moveVector * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }

    private void MovePlayerCamera()
    {
        xRotate -= playerMouseInput.y * sensitivity;
        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotate, 0f, 0f);
    }


    //---------------------------------------
    // private CharacterController _controller;
    // private Animator _animator;

    // private float inputX;
    // private float inputZ;
    // private Vector3 v_movement;
    // private Vector3 v_velocity;
    // private float moveSpeed;
    // private float gravity;

    // void Start()
    // {
    //     GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
    //     _controller = tempPlayer.GetComponent<CharacterController>();
    //     _animator = tempPlayer.transform.GetChild(0).GetComponent<Animator>();

    //     moveSpeed = 4.0f;
    //     gravity = 0.5;
    // }

    // void Update()
    // {
    //     inputX = Input.GetAxis("Horizontal");
    //     inputZ = Input.GetAxis("Vertical");
    // }

    // private void FixedUpdate()
    // {
    //     v_movement = _controller.transform.forward * inputZ;
    //     _controller.transform.Rotate(Vector3.up * inputX * (100.f * Time.deltaTime));
    //     _controller.Move(v_movement * moveSpeed * Time.deltaTime);
    // }
    //-------------------------------------------------------------------------------------------------
    //  private CharacterController controller;
    //     private Vector3 playerVelocity;
    //     private bool groundedPlayer = true;
    //     private float playerSpeed = 2.0f;
    //     private float jumpHeight = 1.0f;
    //     private float gravityValue = -9.81f;

    //     private void Start()
    //     {
    //         controller = gameObject.GetComponent<CharacterController>(); //AddComponent<CharacterController>();
    //     }

    //     void Update()
    //     {
    //         groundedPlayer = controller.isGrounded;
    //         if (groundedPlayer && playerVelocity.y < 0)
    //         {
    //             playerVelocity.y = 0f;
    //         }

    //         Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    //         controller.Move(move * Time.deltaTime * playerSpeed);

    //         if (move != Vector3.zero)
    //         {
    //             gameObject.transform.forward = move;
    //         }

    //         // Changes the height position of the player..
    //         if (Input.GetButtonDown("Jump") && groundedPlayer)
    //         {
    //             playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    //         }

    //         playerVelocity.y += gravityValue * Time.deltaTime;
    //         controller.Move(playerVelocity * Time.deltaTime);
    //     }

    //---------------------------------------------------------------------------------------------------------------
    // float moveSpeed = 1;
    // float rotationSpeed = 1;
    // float runningSpeed;
    // float vaxis, haxis;
    // public bool isJumping, isJumpingAlt, isGrounded = false;
    // Vector3 movement;

    // void Start()
    // {
    //     Debug.Log("Initialized: (" + this.name + ")");
    // }


    // void FixedUpdate()
    // {
    //     /*  Controller Mappings */
    //     vaxis = Input.GetAxis("Vertical");
    //     haxis = Input.GetAxis("Horizontal");
    //     isJumping = Input.GetButton("Jump");
    //     isJumpingAlt = Input.GetKey(KeyCode.Joystick1Button0);

    //     //Simplified...
    //     runningSpeed = vaxis;


    //     if (isGrounded)
    //     {
    //         movement = new Vector3(0, 0f, runningSpeed * 8);        // Multiplier of 8 seems to work well with Rigidbody Mass of 1.
    //         movement = transform.TransformDirection(movement);      // transform correction A.K.A. "Move the way we are facing"
    //     }
    //     else
    //     {
    //         movement *= 0.70f;                                      // Dampen the movement vector while mid-air
    //     }

    //     GetComponent<Rigidbody>().AddForce(movement * moveSpeed);   // Movement Force


    //     if ((isJumping || isJumpingAlt) && isGrounded)
    //     {
    //         Debug.Log(this.ToString() + " isJumping = " + isJumping);
    //         GetComponent<Rigidbody>().AddForce(Vector3.up * 150);
    //     }



    //     if ((Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f) && !isJumping && isGrounded)
    //     {
    //         if (Input.GetAxis("Vertical") >= 0)
    //             transform.Rotate(new Vector3(0, haxis * rotationSpeed, 0));
    //         else
    //             transform.Rotate(new Vector3(0, -haxis * rotationSpeed, 0));

    //     }

    // }

    // void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log("Entered");
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = true;
    //     }
    // }

    // void OnCollisionExit(Collision collision)
    // {
    //     Debug.Log("Exited");
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = false;
    //     }
    // }
    //-------------------------------
    // public CharacterController controller;
    // public float walkSpeed = 5f;
    // public float runSpeed = 10f;
    // public float jumpHeight = 2f;
    // public float gravity = -9.81f;
    // private Vector3 velocity;
    // private bool isGrounded;
    // void Start()
    // {
    //     controller = GetComponent<CharacterController>();
    // }
    // void Update()
    // {
    //     isGrounded = controller.isGrounded;
    //     if (isGrounded && velocity.y < 0) { velocity.y = 0f; }
    //     float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
    //     float moveX = Input.GetAxis("Horizontal") * moveSpeed;
    //     float moveZ = Input.GetAxis("Vertical") * moveSpeed;
    //     Vector3 move = transform.right * moveX + transform.forward * moveZ;
    //     controller.Move(move * Time.deltaTime);
    //     if (Input.GetButtonDown("Jump") && isGrounded) { velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity); }
    //     velocity.y += gravity * Time.deltaTime;
    //     controller.Move(velocity * Time.deltaTime);
    // }
    // //---------------------------------------------




    //  public float speed = 5.0f;
    //     public float jumpHeight = 2.0f;
    //     private CharacterController controller;
    //     private Vector3 moveDirection = Vector3.zero;
    //     private float gravity = 9.81f;

    //     void Start()
    //     {
    //         controller = GetComponent<CharacterController>();
    //     }

    //     void Update()
    //     {
    //         if (controller.isGrounded)
    //         {
    //             float moveHorizontal = Input.GetAxis("Horizontal");
    //             float moveVertical = Input.GetAxis("Vertical");

    //             moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
    //             moveDirection *= speed;

    //             if (Input.GetButton("Jump"))
    //             {
    //                 moveDirection.y = Mathf.Sqrt(jumpHeight * 2.0f * gravity);
    //             }
    //         }

    //         moveDirection.y -= gravity * Time.deltaTime;
    //         controller.Move(moveDirection * Time.deltaTime);
    //     }
}
