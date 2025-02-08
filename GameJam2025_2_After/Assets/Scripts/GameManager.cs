using UnityEngine;
using System;
using System.Collections;


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
    [SerializeField] private GameObject _TrashCan;
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
        if(_somethingIsMissing) {Application.Quit();}
        ///Checking if i have everithing set 



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
        Debug.Log("First Minigame Shown");
        _canvasManager.DisableEventTrigger1(); _canvasManager.Hide_002_1_EventTriggerMessage();
    }   
     /// 002
}
