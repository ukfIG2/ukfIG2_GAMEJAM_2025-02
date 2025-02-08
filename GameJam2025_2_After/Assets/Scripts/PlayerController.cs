using UnityEngine;
using UnityEngine.Android;

public class PlayerController : MonoBehaviour
{
    private bool _somethingIsMissing;
    [SerializeField]    CanvasManager _canvasManager;
    private const float MoveSpeed = 1f;
    private const float MouseSensitivity = 1f;
    private const float Gravity = -9.81f;

    private CharacterController _controller;
    private Vector3 _velocity;
    private float _rotationX = 0f;

    private bool canRotate = true; // Controls whether the player can rotate the camera

    [SerializeField]    private Transform cameraTransform;  // Assign the child camera in the Inspector

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        _somethingIsMissing = false;
        if(_canvasManager == null) {Debug.LogWarning("CanvasManager empty, FIX NOW!!!"); _somethingIsMissing = true;}
        if(_controller == null) {Debug.LogWarning("Controller empty, FIX NOW!!!"); _somethingIsMissing = true;}
    }

    void Start()
    {
        LockCursor(); // Lock the cursor at the start    
    }

    void Update()
    {
        MovePlayer();
        
        if (canRotate) {RotatePlayer();}

        
        // Press Q to lock the cursor and enable rotation
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            LockCursor();
        }
        
        // Press E to unlock the cursor and disable rotation
        if (Input.GetKeyDown(KeyCode.E))
        {
            UnlockCursor();
        }*/
    }   

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal"); // A, D movement
        float moveZ = Input.GetAxis("Vertical");   // W, S movement

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        _controller.Move(move * MoveSpeed * Time.deltaTime);

        // Apply Gravity
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        _velocity.y += Gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity;

        // Rotate player body horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera vertically (up/down)
        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f); // Prevent flipping
        cameraTransform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        canRotate = false; // Disable rotation
    }

    // Function to lock the cursor and enable camera rotation
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        canRotate = true; // Enable rotation
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("01_LevelTrigger") && _canvasManager.Is_1_TriggerEnabled())
        {
            _canvasManager.Show_002_1_EventTriggerMessage();
        }
        else if (other.CompareTag("02_LevelTrigger") && _canvasManager.Is_2_TriggerEnabled())
        {
            _canvasManager.Show_002_2_EventTriggerMessage();
        }
        else if (other.CompareTag("03_LevelTrigger")  && _canvasManager.Is_3_TriggerEnabled())
        {
            _canvasManager.Show_002_3_EventTriggerMessage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("01_LevelTrigger"))
        {
            _canvasManager.Hide_002_1_EventTriggerMessage();
        }
        else if (other.CompareTag("02_LevelTrigger"))
        {
            _canvasManager.Hide_002_2_EventTriggerMessage();
        }
        else if (other.CompareTag("03_LevelTrigger"))
        {
            _canvasManager.Hide_002_3_EventTriggerMessage();
        }
    }


}
