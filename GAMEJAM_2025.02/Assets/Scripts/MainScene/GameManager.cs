using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]    private CanvasManager _canvasManager;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _paperBulletPrefab;
    [SerializeField] private Transform _shootPoint; // Position to spawn bullets
    [SerializeField] private float _minShootPower = 20f;
    [SerializeField] private float _maxShootPower = 100f;
    [SerializeField] private float _powerChargeSpeed = 10f;

    private const int TotalPaperBallsToDestroy = 5;
    [SerializeField]    private int _PaperBallsDestroyed;
    private bool _isChargingPower;
    [SerializeField]    private float _currentShootPower;

    [SerializeField] private bool _firstTriggerIsEnabled;
    [SerializeField] private bool _secondTriggerIsEnabled;
    [SerializeField] private bool _thirdTriggerIsEnabled;
    [SerializeField] private bool _isPlayingFirstMiniGame;

    public bool FirstTriggerMessageIsShown;
    public bool SecondTriggerMessageIsShown;
    public bool ThirdTriggerMessageIsShown;
    public bool SunAquired;

    [SerializeField] private Vector3 _FirstStagePlayerPosition;
    [SerializeField] private Vector3 _FirstStagePlayerRotation;

    private void Awake()
    {
        _firstTriggerIsEnabled = true;
        _secondTriggerIsEnabled = true;
        _thirdTriggerIsEnabled = true;
        _isPlayingFirstMiniGame = false;
        _PaperBallsDestroyed = 0;
        _isChargingPower = false;
        SunAquired = false;
    }

    private void Update()
    {
        if (_firstTriggerIsEnabled && FirstTriggerMessageIsShown)
        {
            if (Input.GetKeyDown(KeyCode.E)) // If player presses "E"
            {
                StartCoroutine(StartFirstMiniGame());
                _firstTriggerIsEnabled = false;
                Debug.Log("FirstLevel triggered");
            }
        }

        if (_isPlayingFirstMiniGame)
        {
            HandleShooting();
            if(_PaperBallsDestroyed >= TotalPaperBallsToDestroy)
            {
                _isPlayingFirstMiniGame = false;
                _player.GetComponent<MainPlayer>().EnableMovement();

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
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            _player.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _player.transform.rotation = endRotation;

        // Enable mini-game mechanics
        _isPlayingFirstMiniGame = true;
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse click starts charging
        {
            _isChargingPower = true;
            _currentShootPower = _minShootPower;
        }

        if (Input.GetMouseButton(0) && _isChargingPower) // Holding mouse increases power
        {
            _currentShootPower += _powerChargeSpeed * Time.deltaTime;
            _canvasManager.ChangePercentageText(_currentShootPower);


            // Make sure power cycles back to min after reaching max
            if (_currentShootPower > _maxShootPower)
            {
                _currentShootPower = _minShootPower;
            }
        }

        if (Input.GetMouseButtonUp(0) && _isChargingPower) // Release to shoot
        {
            ShootPaperBullet();
            _isChargingPower = false;
        }
    }

    private void ShootPaperBullet()
    {
        if (_shootPoint == null)
        {
            Debug.LogError("Shoot point not assigned in the Inspector!");
            return;
        }

        GameObject paperBullet = Instantiate(_paperBulletPrefab, _shootPoint.position, Quaternion.identity);
        Rigidbody rb = paperBullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = _player.transform.forward * _currentShootPower;
        }

        Debug.Log("Shot fired with power: " + _currentShootPower);
    }

    public void BallHit(){
        _PaperBallsDestroyed++;
        Debug.Log("Ball hit");
    }
}
