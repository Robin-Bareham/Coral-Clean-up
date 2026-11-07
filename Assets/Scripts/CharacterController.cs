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
    private InputAction photoAction;

    private Vector2 moveInput;
    private float verticalInput;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        
        InputActionMap map = PlayerControls.FindActionMap("Player");

        moveAction = map.FindAction("Move");
        lookAction = map.FindAction("Look");
        riseAction = map.FindAction("Rise");
        sinkAction = map.FindAction("Sink");
        interactAction = map.FindAction("Interact");
        photoAction = map.FindAction("Photo");
    }

    private void OnEnable()
    {

        moveAction.Enable();
        lookAction.Enable();
        riseAction.Enable();
        sinkAction.Enable();
        interactAction.Enable();
        photoAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        riseAction.Disable();
        sinkAction.Disable();
        interactAction.Disable();
        photoAction.Disable(); 
    }

    private void Update()
    {
        // Read input every frame
        moveInput = moveAction.ReadValue<Vector2>();

        verticalInput = 0f;
        if (riseAction.IsPressed())
        {
            verticalInput += 1f;
        }
        if (sinkAction.IsPressed())
        {
            verticalInput -= 1f;
        }
        if (photoAction.IsPressed()) 
        {
            Debug.Log("TAKING A PHOTO");
            string date = System.DateTime.Now.ToString();
            date = date.Replace("/", "-");
            date = date.Replace(" ", "_");
            date = date.Replace(":", "-");
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/Screenshots/trials_"+date+".png");
            //UnityEditor.AssetDatabase.Refresh();
        }
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
}
