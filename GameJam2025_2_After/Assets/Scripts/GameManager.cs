using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{

    /// Basic settings
    private Boolean _somethingIsMissing;
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField]    private GameObject[] _walls;
    [SerializeField]    private GameObject[] _windows;
    [SerializeField] private Door _mainDoor;
    /// Basic settings

    /// 001
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _001_IntroSound;
    ///001
    
    ///002
    [SerializeField] private GameObject _firstMiniGamePlayerPosition;
    [SerializeField] private GameObject _trashCan;
    [SerializeField] private GameObject _player;
    private Boolean _isPlayingFirstMiniGame;
    //Dynamic list of gameObjects
    [SerializeField] private List<GameObject> _dynamicGameObjects = new List<GameObject>();
    private const int _papersTothrowToTrashcan = 5;
    [SerializeField] private int _papersInTrash;
    [SerializeField] private GameObject _FirstMiniGameObject;
    [SerializeField] private Boolean _sunAquired;
    [SerializeField] private GameObject _firstMiniGameTrash;

    ///002    
    
    
    //Start before creating the game
    void Awake()
    {
        /// Basic settings
        //activate mesh renderer of walls
        foreach (var wall in _walls)
        {
            wall.GetComponent<MeshRenderer>().enabled = true;
        }
        foreach (var window in _windows)
        {
            window.GetComponent<MeshRenderer>().enabled = true;
        }
        /// Basic settings
        
        /// 001
        _audioSource = GetComponent<AudioSource>();
        /// 001 
        
        
        ///Checking if i have everithing set
        if(_canvasManager == null) {Debug.LogWarning("_canvasManafer empty, FIX NOW!!!"); _somethingIsMissing = true;}
        if(_walls == null) {Debug.LogWarning("_walls empty, FIX NOW!!!"); _somethingIsMissing = true;}
        if(_windows == null) {Debug.LogWarning("_windows empty, FIX NOW!!!"); _somethingIsMissing = true;}
        if(_mainDoor == null) {Debug.LogWarning("_mainDoor empty, FIX NOW!!!"); _somethingIsMissing = true;}
        if(_audioSource == null) {Debug.LogWarning("_audioSource empty, FIX NOW!!!"); _somethingIsMissing = true;}
        if(_001_IntroSound == null) {Debug.LogWarning("_001_IntroSound empty, FIX NOW!!!"); _somethingIsMissing = true;}
        if(_trashCan == null) {Debug.LogWarning("_trashCan empty, FIX NOW!!!"); _somethingIsMissing = true;}
        if(_player == null) {Debug.LogWarning("_player empty, FIX NOW"); _somethingIsMissing = true;}
        if(_FirstMiniGameObject == null) {Debug.LogWarning("_firstMiniGameObject empty, FIX NOW"); _somethingIsMissing = true;}
        if(_firstMiniGameTrash == null) {Debug.LogWarning("_firstMiniGameTrash empty, FIX NOW"); _somethingIsMissing = true;}
        if(_somethingIsMissing) {Application.Quit();}
        ///Checking if i have everithing set 

        /// 002
        _isPlayingFirstMiniGame = false;
        _sunAquired = false; ///
        _FirstMiniGameObject.SetActive(false);
        _firstMiniGameTrash.SetActive(true);


    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /// 001
        StartCoroutine(_001_FirstMessageShown());
        /// 001
    }

    // Update is called once per frame
    void Update()
    {
        //if _canvasManager._level_1_TriggerBeingShown && press E playFirstMinigame()
        if (_canvasManager.Is_1_TriggerBeingShown() && Input.GetKeyDown(KeyCode.E))
        {
            /// 002
            _002_PlayFirstMinigame();
        }

        if(_isPlayingFirstMiniGame)
        {
            if (_papersInTrash >= _papersTothrowToTrashcan)
            {
                Debug.Log("FirstMision finished"); 
                DestroyPaperBalls();
                Destroy(_firstMiniGameTrash);
                _isPlayingFirstMiniGame = false;
                //change rotation to 0 0 0
                _player.transform.rotation = Quaternion.Euler(0, 0, 0);
                _player.gameObject.SendMessage("ToggleMode");
                _FirstMiniGameObject.SetActive(true);
                _canvasManager.EnableEventTrigger2();
            }
        }
    }

    /// 001
    private IEnumerator _001_FirstMessageShown()
    {
        Debug.Log("First Message Shown");
        yield return new WaitForSeconds(5f); // Wait 5 seconds before starting
        _mainDoor.OpenDoor();
        yield return new WaitForSeconds(5f); // Wait 5 seconds before starting
        _canvasManager.ShowIntroText();
        _audioSource.PlayOneShot(_001_IntroSound);
        yield return new WaitForSeconds(6f); // Wait 5 seconds before starting
        _mainDoor.CloseDoor();
        _canvasManager.HideIntroText();
        _canvasManager.EnableEventTrigger1();
    }
    /// 001
    
    /// 002
    private void _002_PlayFirstMinigame()
    {
        _isPlayingFirstMiniGame = true;
        Debug.Log("First Minigame Shown");
        _canvasManager.DisableEventTrigger1(); 
        _canvasManager.Hide_002_1_EventTriggerMessage();

        // Move the player to the designated minigame start position
        _player.transform.position = _firstMiniGamePlayerPosition.transform.position;

        // Rotate player to face the trash can
        Vector3 targetPosition = _trashCan.transform.position;
        Vector3 direction = targetPosition - _player.transform.position;
        _player.transform.LookAt(targetPosition);

        // Ensure the player enters rotation mode (assuming ToggleMode switches between movement and rotation)
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
        Debug.Log("Sun Aquired");
    }

     /// 002
}
