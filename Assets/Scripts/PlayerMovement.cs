using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Variables
    public CharacterController controller;
    public float speed = 12f;
    Vector3 velocity;
    public float gravity = -9.81f;

    //Ensures player moves to the ground
    public bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //Controls
    public KeyCode crouchKey = KeyCode.LeftControl;
    public bool shouldCrouch => Input.GetKeyDown(crouchKey);

    //Crouching and standing
    public float crouchHeight = 0.5f;
    public float standHeight = 2f;  
    public float timeToCrouch = 0.5f; 
    public Vector3 camCenterPoint = new Vector3(0,0,0);
    public Vector3 camCrouchPoint = new Vector3(0,0.5f,0);
    public bool isCrouching = false;
    public bool duringCrouchAnimation;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }



        //Get input variables from Input Manager
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward*z; //Unity returns -1 or 1 --> so if value is negative will move left
        controller.Move(move * speed * Time.deltaTime); //Moving towards vector3 above, also moving framerate independant

        //Gravity without rigid body
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleJump() {

    }

}
