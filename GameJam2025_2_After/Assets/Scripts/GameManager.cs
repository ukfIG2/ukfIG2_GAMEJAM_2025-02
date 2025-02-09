using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    /// Basic settings
    private Boolean _somethingIsMissing;
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private GameObject[] _walls;
    [SerializeField] private GameObject[] _windows;
    [SerializeField] private Door _mainDoor;
    /// Basic settings

    /// 001
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _001_IntroSound;
    /// 001

    /// 002
    [SerializeField] private GameObject _firstMiniGamePlayerPosition;
    [SerializeField] private GameObject _trashCan;
    [SerializeField] private GameObject _player;
    private Boolean _isPlayingFirstMiniGame;
    [SerializeField] private List<GameObject> _dynamicGameObjects = new List<GameObject>();
    private const int _papersTothrowToTrashcan = 5;
    [SerializeField] private int _papersInTrash;
    [SerializeField] private GameObject _FirstMiniGameObject;
    [SerializeField] private Boolean _sunAquired;
    [SerializeField] private GameObject _firstMiniGameTrash;
    /// 002   

    /// 003
    [SerializeField] private GameObject _blanket;
    [SerializeField] private GameObject _secondMiniGamePlayerPosition;
    private bool _isPlayingSecondMiniGame = false;
    private int _gameObjectsToDestroy = 11;
    [SerializeField] private int _gameObjectsDestroyed;
    [SerializeField] private GameObject _secondMiniGameObject;
    private Boolean _keyAquired;
    [SerializeField]    private GameObject _toDeletaAfterCompletion;
    
    /// 003

    void Awake()
    {
        /// Basic settings
        foreach (var wall in _walls) { wall.GetComponent<MeshRenderer>().enabled = true; }
        foreach (var window in _windows) { window.GetComponent<MeshRenderer>().enabled = true; }
        /// Basic settings

        /// 001
        _audioSource = GetComponent<AudioSource>();
        /// 001 

        /// Checking if everything is set
        if (_canvasManager == null) { Debug.LogWarning("_canvasManager empty, FIX NOW!!!"); _somethingIsMissing = true; }
        if (_walls == null) { Debug.LogWarning("_walls empty, FIX NOW!!!"); _somethingIsMissing = true; }
        if (_windows == null) { Debug.LogWarning("_windows empty, FIX NOW!!!"); _somethingIsMissing = true; }
        if (_mainDoor == null) { Debug.LogWarning("_mainDoor empty, FIX NOW!!!"); _somethingIsMissing = true; }
        if (_audioSource == null) { Debug.LogWarning("_audioSource empty, FIX NOW!!!"); _somethingIsMissing = true; }
        if (_001_IntroSound == null) { Debug.LogWarning("_001_IntroSound empty, FIX NOW!!!"); _somethingIsMissing = true; }
        if (_trashCan == null) { Debug.LogWarning("_trashCan empty, FIX NOW!!!"); _somethingIsMissing = true; }
        if (_player == null) { Debug.LogWarning("_player empty, FIX NOW"); _somethingIsMissing = true; }
        if (_FirstMiniGameObject == null) { Debug.LogWarning("_firstMiniGameObject empty, FIX NOW"); _somethingIsMissing = true; }
        if (_firstMiniGameTrash == null) { Debug.LogWarning("_firstMiniGameTrash empty, FIX NOW"); _somethingIsMissing = true; }
        if (_blanket == null) { Debug.LogWarning("_blanket empty, FIX NOW"); _somethingIsMissing = true; }
        if (_secondMiniGamePlayerPosition == null) { Debug.LogWarning("_secondMiniGamePlayerPosition empty, FIX NOW"); _somethingIsMissing = true; }
        if (_secondMiniGameObject == null) { Debug.LogWarning("_secondMiniGameObject empty, FIX NOW"); _somethingIsMissing = true; }
        if (_somethingIsMissing) { Application.Quit(); }
        /// Checking if everything is set 

        /// 002
        _isPlayingFirstMiniGame = false;
        _sunAquired = false;
        _FirstMiniGameObject.SetActive(false);
        _firstMiniGameTrash.SetActive(true);
        _blanket.SetActive(true);

        _gameObjectsDestroyed = 0;
        _secondMiniGameObject.SetActive(false);
        _keyAquired = false;
    }

    void Start()
    {
        StartCoroutine(_001_FirstMessageShown());
    }

    void Update()
    {
        if (_canvasManager.Is_1_TriggerBeingShown() && Input.GetKeyDown(KeyCode.E))
        {
            _002_PlayFirstMinigame();
        }

        if (_isPlayingFirstMiniGame)
        {
            if (_papersInTrash >= _papersTothrowToTrashcan)
            {
                Debug.Log("First Mission finished");
                DestroyPaperBalls();
                Destroy(_firstMiniGameTrash);
                _isPlayingFirstMiniGame = false;
                _player.transform.rotation = Quaternion.Euler(0, 0, 0);
                _player.gameObject.SendMessage("ToggleMode");
                _FirstMiniGameObject.SetActive(true);
                _canvasManager.EnableEventTrigger2();
            }
        }

        if (_canvasManager.Is_2_TriggerBeingShown() && Input.GetKeyDown(KeyCode.E))
        {
            _003_PlaySecondMinigame();
        }

        if (_isPlayingSecondMiniGame)
        {
            CheckForObjectDestruction();
            if (_gameObjectsDestroyed >= _gameObjectsToDestroy)
            {
            Debug.LogWarning("everithing destroyed");
            _isPlayingSecondMiniGame = false;
            //disable boxcollider of  gameobjects with tag 02_MiniGameGameObjectsToBeDisabled
                foreach (var gameObj in GameObject.FindGameObjectsWithTag("02_MiniGameGameObjectsToBeDisabled"))
                {
                    gameObj.GetComponent<Collider>().enabled = true;
                }
                _secondMiniGameObject.SetActive(true);
                Destroy(_toDeletaAfterCompletion);
            }
        }
    }

    /// 001
    private IEnumerator _001_FirstMessageShown()
    {
        Debug.Log("First Message Shown");
        yield return new WaitForSeconds(5f);
        _mainDoor.OpenDoor();
        yield return new WaitForSeconds(5f);
        _canvasManager.ShowIntroText();
        _audioSource.PlayOneShot(_001_IntroSound);
        yield return new WaitForSeconds(6f);
        _mainDoor.CloseDoor();
        _canvasManager.HideIntroText();
        _canvasManager.EnableEventTrigger1();
    }

    /// 002
    private void _002_PlayFirstMinigame()
    {
        _isPlayingFirstMiniGame = true;
        Debug.Log("First Minigame Started");
        _canvasManager.DisableEventTrigger1();
        _canvasManager.Hide_002_1_EventTriggerMessage();

        _player.transform.position = _firstMiniGamePlayerPosition.transform.position;
        Vector3 targetPosition = _trashCan.transform.position;
        _player.transform.LookAt(targetPosition);
        _player.gameObject.SendMessage("ToggleMode");
    }

    public void AddPaperBallToList(GameObject paper)
    {
        _dynamicGameObjects.Add(paper);
    }

    public void DestroyPaperBalls()
    {
        foreach (var paperBall in _dynamicGameObjects)
        {
            Destroy(paperBall);
        }
        _dynamicGameObjects.Clear();
    }

    public void ThrowPapersToTrashCan()
    {
        _papersInTrash += 1;
    }

    public void RemoveFromList(GameObject paper)
    {
        _dynamicGameObjects.Remove(paper);
    }

    public void AquireSun()
    {
        _sunAquired = true;
        Debug.Log("Sun Acquired");
    }

    public void SunStolen()
    {
        _sunAquired = false;
        Debug.LogWarning("Sun Stolen");
    }

    /// 003
    private void _003_PlaySecondMinigame()
    {
        Debug.Log("Second Minigame Started");
        _isPlayingSecondMiniGame = true;
        _canvasManager.DisableEventTrigger2();
        _canvasManager.EnableEventTrigger3();
        _canvasManager.Hide_002_2_EventTriggerMessage();
        _blanket.SetActive(false);

        //_player.gameObject.SendMessage("ToggleMode");
        _player.gameObject.SendMessage("UnlockCursor");
        _player.gameObject.SendMessage("DisableRotation");

        _player.transform.rotation = Quaternion.Euler(0, 172.2f, 0);
        _player.transform.position = _secondMiniGamePlayerPosition.transform.position;
        //disable boxcollider of  gameobjects with tag 02_MiniGameGameObjectsToBeDisabled
        foreach (var gameObj in GameObject.FindGameObjectsWithTag("02_MiniGameGameObjectsToBeDisabled"))
        {
            gameObj.GetComponent<Collider>().enabled = false;
        }
    }

private void CheckForObjectDestruction()
{
    if (Input.GetMouseButtonDown(0)) // Left mouse button click
    {
        Debug.Log("Mouse Click Detected!");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f); // Draw ray in scene view

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Collide))
        {
            Debug.Log("Raycast Hit: " + hit.collider.gameObject.name);

            if (hit.collider.CompareTag("02_MiniGameObjectsToBeDestroyed"))
            {
                Debug.Log("Destroying: " + hit.collider.gameObject.name);
                Destroy(hit.collider.gameObject);
                _gameObjectsDestroyed += 1;
            }
            else
            {
                Debug.Log("Hit object does NOT have the correct tag: " + hit.collider.gameObject.tag);
            }
        }
        else
        {
            Debug.Log("Raycast did NOT hit anything!");
        }
    }
}

    public void KeyAquired()
    {
        _keyAquired = true;
        Debug.Log("Key Acquired");
    }
    public void KeyUsed()
    {
        _keyAquired = false;
        Debug.LogWarning("Key used");
    }

}
