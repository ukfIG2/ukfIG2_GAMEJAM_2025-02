using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private bool _firstTriggerIsEnabled;
    [SerializeField] private bool _secondTriggerIsEnabled;
    [SerializeField] private bool _thirdTriggerIsEnabled;

    public bool FirstTriggerMessageIsShown;
    public bool SecondTriggerMessageIsShown;
    public bool ThirdTriggerMessageIsShown;

    [SerializeField] private Vector3 _FirstStagePlayerPosition; // Assign this in Inspector
    [SerializeField] private Vector3 _FirstStagePlayerRotation; // Assign this in Inspector
    private void Awake()
    {
        _firstTriggerIsEnabled = true;
        _secondTriggerIsEnabled = true;
        _thirdTriggerIsEnabled = true;
    }

    private void Update()
    {
        if (_firstTriggerIsEnabled && FirstTriggerMessageIsShown)
        {
            if (Input.GetKeyDown(KeyCode.E)) // If player presses "E"
            {
                StartCoroutine(StartFirstMiniGame());
                _firstTriggerIsEnabled = false;
                Debug.Log("FirstLevel trigered");
            }
        }
    }

    private IEnumerator StartFirstMiniGame()
    {
        Debug.Log("FirstLevel triggered minigame");

        _firstTriggerIsEnabled = false;
        // Disable player movement
        _player.GetComponent<MainPlayer>().DisableMovement();

        // Move player to first stage position
        _player.transform.position = _FirstStagePlayerPosition;
        _player.transform.eulerAngles = _FirstStagePlayerRotation;

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Smoothly rotate player 180° on the Y-axis
        Quaternion startRotation = _player.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 180f, 0f);
        float duration = 1f; // Duration of the rotation in seconds
        float elapsed = 0f;

        while (elapsed < duration)
        {
            _player.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is set
        _player.transform.rotation = endRotation;
    }

    
}
