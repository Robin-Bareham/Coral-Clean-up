using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset PlayerControls;

    [SerializeField]
    private Transform cameraRotation;

    [SerializeField] 
    private float moveForce = 10f;
    [SerializeField] 
    private float verticalForce = 10f;


    private Rigidbody rBody;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction riseAction;
    private InputAction sinkAction;
    private InputAction interactAction;

    private Vector2 moveInput;
    private int verticalInput;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        
        InputActionMap map = PlayerControls.FindActionMap("Player");

        moveAction = map.FindAction("Move");
        lookAction = map.FindAction("Look");
        riseAction = map.FindAction("Rise");
        sinkAction = map.FindAction("Sink");
        interactAction = map.FindAction("Interact");
    }

    private void OnEnable()
    {

        moveAction.Enable();
        lookAction.Enable();
        riseAction.Enable();
        sinkAction.Enable();
        interactAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        riseAction.Disable();
        sinkAction.Disable();
        interactAction.Disable();
    }

    private void Update()
    {
        // Read input every frame
        moveInput = moveAction.ReadValue<Vector2>();

        //Debug.Log("X = " + moveInput.x);
        //Debug.Log("Y = " + moveInput.y);
        //Debug.Log("Angular: x = " + rBody.angularVelocity.x + ", y = " + rBody.angularVelocity.y);
        //Debug.Log("Linear: x = " + rBody.linearVelocity.x + ", y = " + rBody.linearVelocity.y);        

        verticalInput = 0;
        if (riseAction.IsPressed())
        {
            verticalInput += 1;
        }
        if (sinkAction.IsPressed())
        {
            verticalInput -= 1;
        }
        HandleDeceleration();
    }

    private void FixedUpdate()
    {        
        //Movement handling here as rigidbody force is handled during physics step
        HandleMovement();
    }

    private void HandleMovement()
    {
        //Use of Rigidbody.AddForce to emulate underwater movement

        // Get orientation
        Vector3 forward = cameraRotation.forward;
        Vector3 right = cameraRotation.right;
        Vector3 up = cameraRotation.up;

        //Calculate movement relative to camera
        // Horizontal movement: X and Z axes
        Vector3 movement = (right * moveInput.x) + (forward * moveInput.y);
        // Vertical movement: Y axis
        movement += cameraRotation.up * verticalInput;

        // Apply force across all three axes
        // Left/Right, Up/Down, Forward/Back
        rBody.AddForce(movement.x * moveForce, movement.y * verticalForce, movement.z * moveForce, ForceMode.Force);
    }

    private void HandleDeceleration()
    {
        // Get the world space velocity from the Rigidbody
        Vector3 worldVelocity = rBody.linearVelocity;
        // Convert it into the camera's local coordinate space
        Vector3 relativeVelocity = cameraRotation.InverseTransformDirection(worldVelocity);
        // Interpret the axes relative to the camera's view
        float forwardSpeed = relativeVelocity.z; // Positive = moving with camera view, Negative = moving toward camera
        float rightSpeed = relativeVelocity.x;   // Positive = moving right, Negative = moving left
        float upSpeed = relativeVelocity.y;      // Positive = moving up, Negative = moving down

        //Debug.Log("Forward Speed: " + forwardSpeed);
        //Debug.Log("Right Speed: " + rightSpeed);
        //Debug.Log("Up Speed: " +  upSpeed);

        //Affect input relative to actual input and velocity
        if(moveInput.x == 0)
        {
            if(rightSpeed > 0f)
            {
                moveInput.x = -1f;
            }
            else if(rightSpeed < 0f)
            {
                moveInput.x = 1f;
            }
        }
        if (moveInput.y == 0)
        {
            if (forwardSpeed > 0f)
            {
                moveInput.y = -1f;
            }
            else if (forwardSpeed < 0f)
            {
                moveInput.y = 1f;
            }
        }
        if (verticalInput == 0)
        {
            if (upSpeed > 0f)
            {
                verticalInput = -1;
            }
            else if (upSpeed < 0f)
            {
                verticalInput = 1;
            }
        } 
    }

}
