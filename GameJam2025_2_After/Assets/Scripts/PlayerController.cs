using System;
using UnityEngine;
using UnityEngine.Android;

public class PlayerController : MonoBehaviour
{
    private bool _somethingIsMissing;
    [SerializeField] CanvasManager _canvasManager;

    /// for shooting
    [SerializeField] private GameObject projectilePrefab; // Assign in the Inspector
    [SerializeField] private Transform shootPoint; // Empty GameObject at camera position
    [SerializeField] private float minShootForce = 1f;  // Min force
    [SerializeField] private float maxShootForce = 10f; // Max force
    private float currentShootForce;
    private bool increasingForce = true; // Toggle direction
    private bool isCharging = false; // Track if we are holding LMB
    /// for shooting

    private const float MoveSpeed = 1f;
    private const float MouseSensitivity = 1f;
    private const float Gravity = -9.81f;

    private CharacterController _controller;
    private Vector3 _velocity;
    private float _rotationX = 0f;

    [SerializeField] private bool canRotate = true; // Controls whether the player can rotate the camera
    [SerializeField] private bool isRotationMode = false; // Toggle between movement and rotation modes

    [SerializeField] private Transform cameraTransform;  // Assign the child camera in the Inspector
    [SerializeField] private Boolean _stopMoving;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        _somethingIsMissing = false;
        if (_canvasManager == null) { Debug.LogWarning("CanvasManager empty, FIX NOW!!!"); _somethingIsMissing = true; }
        if (_controller == null) { Debug.LogWarning("Controller empty, FIX NOW!!!"); _somethingIsMissing = true; }
        if (projectilePrefab == null) { Debug.LogWarning("ProjectilePrefab empty, FIX NOW!!!"); _somethingIsMissing = true; }
        if (shootPoint == null) { Debug.LogWarning("ShootPoint empty, FIX NOW!!!"); _somethingIsMissing = true; }
        if (_somethingIsMissing) { Application.Quit(); }

        currentShootForce = minShootForce; // Start at min force
        _stopMoving = false;
    }

    void Start()
    {
        LockCursor(); // Lock the cursor at the start    
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMode();
        }*/
        if (_stopMoving) return; // Prevent movement and gravity when stopped

        if (!isRotationMode)
        {
            MovePlayer();
        }

        if (canRotate)
        {
            if (isRotationMode)
            {
                RotateObject();
                HandleShooting();

            }
            else
            {
                RotatePlayer();
            }
        }

    }

    private void MovePlayer()
    {
        if (isRotationMode) return; // Prevent movement and gravity in rotation mode

        float moveX = Input.GetAxis("Horizontal"); // A, D movement
        float moveZ = Input.GetAxis("Vertical");   // W, S movement

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        _controller.Move(move * MoveSpeed * Time.deltaTime);

        // Apply Gravity (only when not in rotation mode)
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

    void RotateObject()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity;

        // Rotate entire object
        transform.Rotate(Vector3.up * mouseX);  // Rotate left/right
        transform.Rotate(Vector3.right * -mouseY); // Rotate up/down
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        canRotate = false; // Disable rotation
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        canRotate = true; // Enable rotation
    }

    public void ToggleMode()
    {
        isRotationMode = !isRotationMode;

        if (isRotationMode)
        {
            Debug.Log("Switched to Rotation Mode");
            _controller.enabled = false;  // Disable CharacterController to prevent movement issues
        }
        else
        {
            Debug.Log("Switched to Movement Mode");
            _controller.enabled = true;   // Re-enable CharacterController
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("01_LevelTrigger") && _canvasManager.Is_1_TriggerEnabled())
        {
            _canvasManager.Show_002_1_EventTriggerMessage();
        }
        else if (other.CompareTag("02_LevelTrigger") && _canvasManager.Is_2_TriggerEnabled())
        {
            _canvasManager.Show_002_2_EventTriggerMessage();
        }
        else if (other.CompareTag("03_LevelTrigger") && _canvasManager.Is_3_TriggerEnabled())
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

    /// for shooting
    private void HandleShooting()
    {
        if (Input.GetMouseButton(0)) // Holding LMB
        {
            isCharging = true;
            ChangeShootForce();
        }

        if (Input.GetMouseButtonUp(0)) // Released LMB
        {
            isCharging = false;
            Shoot();
        }
    }

    private void ChangeShootForce()
    {
        if (increasingForce)
        {
            currentShootForce += Time.deltaTime * 5f; // Adjust speed of force change
            if (currentShootForce >= maxShootForce)
            {
                currentShootForce = maxShootForce;
                increasingForce = false;
            }
        }
        else
        {
            currentShootForce -= Time.deltaTime * 5f;
            if (currentShootForce <= minShootForce)
            {
                currentShootForce = minShootForce;
                increasingForce = true;
            }
        }

        Debug.Log($"Charging: {currentShootForce}");
    }

    private void Shoot()
    {
        if (projectilePrefab == null || shootPoint == null)
        {
            Debug.LogWarning("Projectile prefab or shoot point is not set!");
            return;
        }

        // Instantiate projectile at shoot point
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Get the shooting direction (camera forward + 10 degrees upwards)
        Vector3 shootDirection = cameraTransform.forward;
        shootDirection = Quaternion.Euler(10f, 0f, 0f) * shootDirection; // Add 10° upward tilt

        // Apply force to the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(shootDirection * currentShootForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Projectile does not have a Rigidbody!");
        }

        Debug.Log($"Shot fired with force: {currentShootForce}");

        // Reset force for next shot
        currentShootForce = minShootForce;
        increasingForce = true;
    }
    /// for shooting

    public void DisableRotation()
    {
        canRotate = false; // Prevent any rotation input
        isRotationMode = false; // Ensure it's not in rotation mode
        _rotationX = 0f; // Reset camera rotation
        cameraTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        _stopMoving = true;
        Debug.Log("Rotation fully disabled!");

    }


}
