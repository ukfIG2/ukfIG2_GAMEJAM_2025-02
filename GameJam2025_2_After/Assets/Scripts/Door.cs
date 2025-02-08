using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum Side { Left, Right }

    [SerializeField] private Side _sideToOpen;
    [SerializeField] private float _openAngle = 90f;  // Angle to open the door
    [SerializeField] private float _openSpeed = 2f;   // Speed of opening/closing
    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private bool _isOpening = false;
    private bool _isClosing = false;

    private void Start()
    {
        _closedRotation = transform.rotation;

        // Calculate the target rotation based on the side
        if (_sideToOpen == Side.Left)
        {
            _openRotation = Quaternion.Euler(0, _openAngle, 0) * _closedRotation;
        }
        else
        {
            _openRotation = Quaternion.Euler(0, -_openAngle, 0) * _closedRotation;
        }
    }

    private void Update()
    {
        if (_isOpening)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _openRotation, Time.deltaTime * _openSpeed);
            // Stop opening once it's near the target
            if (Quaternion.Angle(transform.rotation, _openRotation) < 0.1f)
            {
                _isOpening = false;
            }
        }

        if (_isClosing)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _closedRotation, Time.deltaTime * _openSpeed);
            // Stop closing once it's near the target
            if (Quaternion.Angle(transform.rotation, _closedRotation) < 0.1f)
            {
                _isClosing = false;
            }
        }
    }

    // Open the door smoothly
    public void OpenDoor()
    {
        _isOpening = true;
        _isClosing = false; // Ensure closing is stopped when opening
    }

    // Close the door smoothly
    public void CloseDoor()
    {
        _isClosing = true;
        _isOpening = false; // Ensure opening is stopped when closing
    }
}
