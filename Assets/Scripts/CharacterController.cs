using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset PlayerControls;
    private bool _Jump;
    private bool _Crouch;
    private bool Running = false;
    private float Speed = 0;
    private Rigidbody rBody;

    [HideInInspector]
    public bool losingStamina = false;
    [HideInInspector]
    public bool canJump = true;
    [HideInInspector]
    public bool isMoving = true;
    [HideInInspector]
    public bool isGrounded = true;
    [HideInInspector]
    public bool isUnderWater = true;

    public float walkSpeed = 10;
    public float runSpeed = 15;
    public float AirModifier = 1.5f;
    public float fakeFriction = 0.86f;
    public float jumpForce = 40;
    public float isGroundCheck = 1;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction crouchAction;
    private InputAction interactAction;
    private Vector2 moveInput;
    private Vector2 lookInput;

    private void Awake()
    {
        InputActionMap map = PlayerControls.FindActionMap("Player");
        moveAction = map.FindAction("Move");
        lookAction = map.FindAction("Look");
        jumpAction = map.FindAction("Jump");
        crouchAction = map.FindAction("Crouch");
        interactAction = map.FindAction("Interact");
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        crouchAction.Enable();
        interactAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        crouchAction.Disable();
        interactAction.Disable();
    }

    void Update()
    {
        //Speed Modifier
        if (Running == true && isMoving && isGrounded)
        {
            Speed = runSpeed;
        }
        else if (Running && isMoving && isGrounded)
        {
            Speed = walkSpeed;
        }
        else
        {
            Speed = walkSpeed / AirModifier;
        }

        isGrounded = Physics.Raycast(transform.position, -Vector3.up, isGroundCheck);
        //Running = Input.GetKey(KeyCode.LeftShift);
        _Jump = jumpAction.ReadValue<bool>();
        float h = moveInput.x;
        float v = moveInput.y;

        if (h >= 0.1 || h <= -0.1 || v >= 0.1 || v <= -0.1 || _Jump)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        //Limit Speed
        if (rBody.linearVelocity.magnitude > Speed)
        {
            rBody.linearVelocity = rBody.linearVelocity.normalized * Speed;
        }

        //Movement
        this.rBody.linearVelocity = new Vector3(this.rBody.linearVelocity.x * fakeFriction, this.rBody.linearVelocity.y, this.rBody.linearVelocity.z * fakeFriction);
        Vector3 move = this.rBody.linearVelocity; //Weird workaround that works?
        Vector3 MovementForce = new Vector3(move.x, 0, move.z);
        MovementForce.Normalize();
        Vector3 MoveForce = transform.TransformDirection(MovementForce);

        if (isMoving)
        {
            rBody.AddForce(MoveForce * Speed);
        }

        if (isGrounded && canJump && _Jump || isUnderWater)
        {
            rBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
}
