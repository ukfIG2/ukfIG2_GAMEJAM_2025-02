using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private bool _movementEnabled;
    public float moveSpeed = 100f;
    public float mouseSensitivity = 100f;

    public Transform cameraTransform;
    private Rigidbody rb;

    [SerializeField] private GameObject _canvas;
    private CanvasManager _canvasManager;

    private float xRotation = 0f;

    private void Awake()
    {
        _movementEnabled = true;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent unwanted rotations

        if (_canvas != null)
        {
            _canvasManager = _canvas.GetComponent<CanvasManager>();
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor
    }

    void Update()
    {
        HandleMouseLook();
    }

    void FixedUpdate()
    {
        if (_movementEnabled)
        {
            HandleMovement();
        }
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate player body left/right
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, mouseX, 0f));

        // Rotate camera up/down (clamped)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        Vector3 velocity = move * moveSpeed;
        velocity.y = rb.velocity.y; // Preserve gravity effect
        rb.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("FirstLevelTrigger") && _canvasManager != null)
        {
            if(_gameManager._firstTriggerIsEnabled)
            {
            _canvasManager.ShowFirstTriggerMessage();
            _gameManager.FirstTriggerMessageIsShown = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FirstLevelTrigger") && _canvasManager != null)
        {
            _canvasManager.HideFirstTriggerMessage();
            _gameManager.FirstTriggerMessageIsShown = false;
        }
    }

    public void DisableMovement()
    {
        _movementEnabled = false;
        rb.velocity = Vector3.zero; // Stop movement when disabled
    }

    public void EnableMovement()
    {
        _movementEnabled = true;
        
        // Reset rotation to (0,0,0) while keeping position
        rb.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

}
