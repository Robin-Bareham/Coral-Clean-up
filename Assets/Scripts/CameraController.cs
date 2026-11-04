using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float sensX = 100f;
    public float sensY = 100f;

    public Transform orientation;

    [SerializeField] private InputActionAsset playerControls;

    private InputAction lookAction;
    private Vector2 lookInput;

    private float xRotation;
    private float yRotation;

    private void Awake()
    {
        // Get the Look action from the Player action map
        InputActionMap map = playerControls.FindActionMap("Player");
        lookAction = map.FindAction("Look");
    }

    private void OnEnable()
    {
        lookAction.Enable();
    }

    private void OnDisable()
    {
        lookAction.Disable();
    }

    private void Start()
    {
        // Lock cursor and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Read mouse or gamepad look input
        lookInput = lookAction.ReadValue<Vector2>();

        // Calculate mouse movement
        float mouseX = lookInput.x * sensX * Time.deltaTime;
        float mouseY = lookInput.y * sensY * Time.deltaTime;

        // Update rotation values
        yRotation += mouseX;
        xRotation -= mouseY;

        // Clamp vertical rotation to prevent camera flipping
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate camera and player orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
