using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Variables
    public Camera playercam;
    public CharacterController controller;
    public float speed = 12f;
    public float crouchSpeed = 5f;
    Vector3 velocity;
    public float gravity = -9.81f;

    //Ensures player moves to the ground
    public bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //Controls
    public KeyCode crouchKey = KeyCode.LeftControl;

    //Crouching and standing
    private bool canCrouch = true;
    private bool shouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation; //Must be grounded and not doing the crouch animation && isGrounded
    private float crouchHeight = 0.5f;
    private float standHeight = 2f;
    private float timeToCrouch = 0.5f;
    private Vector3 camCenterPoint = new Vector3(0,0,0);
    private Vector3 camCrouchPoint = new Vector3(0,0.5f,0);
    private bool isCrouching = false;
    private bool duringCrouchAnimation;

    //Jumping
    public float jumpHeight = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Extra toggle for crouching enable or not.. not rly needed.
        if (canCrouch)
        {
            HandleCrouch();
        }

        
        //Artificial gravity without physics engine
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }



        //Get input variables from Input Manager
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward*z; //Unity returns -1 or 1 --> so if value is negative will move left

        //Move slower while crouching?
        if (!isCrouching)
        {
            controller.Move(move * speed * Time.deltaTime); //Moving towards vector3 above, also moving framerate independant
        }
        else
        {
            controller.Move(move * crouchSpeed * Time.deltaTime);
        }

        
        //Gravity without rigid body
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); //-2 of the gravity
        }


    }

    void HandleCrouch()
    {
        if (shouldCrouch)
        {
            StartCoroutine(CrouchStand());
        }
    }

    void HandleJump() {

    }

    private IEnumerator CrouchStand()
    {

        //Ensure player can't standup above obstacle -- using raycast
        if (isCrouching && Physics.Raycast(playercam.transform.position, Vector3.up, 1f))
            yield break;

        //Starting crouch
        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standHeight : crouchHeight; //If already crouching, target height to get to is standing, vice versa
        float currentHeight = controller.height;
        Vector3 targetCenter = isCrouching? camCenterPoint : camCrouchPoint;
        Vector3 currentCenter = controller.center;

        while(timeElapsed < timeToCrouch)
        {
            controller.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed/timeToCrouch); //Lerps from current to target within time
            controller.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime; //Increment current time by time taken
            yield return null;
        }

        //Ensures the lerp isn't stuck somewhere between
        controller.height = targetHeight;
        controller.center = targetCenter;

        isCrouching = !isCrouching;

        //Ending crouch
        duringCrouchAnimation = false;
    }

}
