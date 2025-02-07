using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private bool _movementEnabled;
    public float moveSpeed = 100f;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    public Transform cameraTransform;
    private Rigidbody rb;
    private bool isGrounded;

    [SerializeField] private GameObject _canvas;
    private CanvasManager _canvasManager;

    private void Awake()
    {
        _movementEnabled = true;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent unwanted rotations

        if (_canvas != null)
        {
            _canvasManager = _canvas.GetComponent<CanvasManager>();
        }
    }

    void Update()
    {
        
            // Mouse Look
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit camera rotation

            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Camera up/down
            transform.Rotate(Vector3.up * mouseX); // Player body left/right
        
        
    }

    void FixedUpdate()
    {
        if(_movementEnabled)
        {
            // Movement
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("FirstLevelTrigger") && _canvasManager != null)
        {
            _canvasManager.ShowFirstTriggerMessage();
            _gameManager.FirstTriggerMessageIsShown = true; 
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
    }

    public void EnableMovement()
    {
        _movementEnabled = true;
    }
}
