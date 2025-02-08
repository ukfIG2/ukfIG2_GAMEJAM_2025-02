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
        
        ///Checking id i have everithing set in SerializedFields
        if(_canvasManager == null) {Debug.LogWarning("_canvasManafer empty, FIX NOW!!!"); _somethingIsMissing = true;}
        if(_walls == null) {Debug.LogWarning("_walls empty, FIX NOW!!!"); _somethingIsMissing = true;}
        if(_windows == null) {Debug.LogWarning("_windows empty, FIX NOW!!!"); _somethingIsMissing = true;}
        if(_mainDoor == null) {Debug.LogWarning("_mainDoor empty, FIX NOW!!!"); _somethingIsMissing = true;}
        if(_somethingIsMissing) {Application.Quit();}
        ///Checking id i have everithing set in SerializedFields


    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //At the start after 5 second _canvasManager.ShowIntroText(), door.openDoor(), after another 10 second door.closeDoor()
        StartCoroutine(_001_FirstMessageShown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator _001_FirstMessageShown()
    {
        Debug.Log("First Message Shown");
        yield return new WaitForSeconds(5f); // Wait 5 seconds before starting
        _mainDoor.OpenDoor();
        yield return new WaitForSeconds(5f); // Wait 5 seconds before starting
        _canvasManager.ShowIntroText();

    }

    
}
