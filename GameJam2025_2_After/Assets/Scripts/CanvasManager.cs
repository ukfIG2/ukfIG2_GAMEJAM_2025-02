using System;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    /// Basic settings
    private Boolean _somethingIsMissing;
    [SerializeField] private GameManager _gameManager;
    /// Basic settings

    // 001
    [SerializeField] private TextMeshProUGUI _001_IntroText;
    /// 001

    ///Event triggers
    [SerializeField] private Boolean _level_1_TriggerEventEnabled;
    [SerializeField] private Boolean _level_2_TriggerEventEnabled;
    [SerializeField] private Boolean _level_3_TriggerEventEnabled;
    [SerializeField] private Boolean _level_1_TriggerBeingShown;
    [SerializeField] private Boolean _level_2_TriggerBeingShown;
    [SerializeField] private Boolean _level_3_TriggerBeingShown;
    [SerializeField] private TextMeshProUGUI _002_1_EventTriggerMessage;
    [SerializeField] private TextMeshProUGUI _002_2_EventTriggerMessage;
    [SerializeField] private TextMeshProUGUI _002_3_EventTriggerMessage;

    
    ///Event triggers

    private void Awake()
    {
        _somethingIsMissing = false;
        ///Checking if i have everithing set 
        if(_gameManager == null) {Debug.LogWarning("GameManager empty, FIX NOW!!!"); _somethingIsMissing = true;}
        if(_001_IntroText == null) {Debug.LogWarning("_001_IntroText empty, FIX NOW!!!"); _somethingIsMissing = true;}
        if(_002_1_EventTriggerMessage == null) {Debug.LogWarning("_002_EventTriggerMessage empty !!!"); _somethingIsMissing = true;}
        if(_002_2_EventTriggerMessage == null) {Debug.LogWarning("_002_EventTriggerMessage empty !!!"); _somethingIsMissing = true;}
        if(_002_3_EventTriggerMessage == null) {Debug.LogWarning("_002_EventTriggerMessage empty !!!"); _somethingIsMissing = true;}

        if(_somethingIsMissing) {Application.Quit();}
        ///Checking if i have everithing set 
        
        /// Basic settings
        _001_IntroText.gameObject.SetActive(false);
        /// Basic settings
        
        ///Event triggers
        _level_1_TriggerBeingShown = false;
        _level_2_TriggerBeingShown = false;
        _level_3_TriggerBeingShown = false;

        _level_1_TriggerEventEnabled = false;
        _level_2_TriggerEventEnabled = false;
        _level_3_TriggerEventEnabled = false;

        ///Event triggers
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }



    /// 001
    public void ShowIntroText(){_001_IntroText.gameObject.SetActive(true);}
    public void HideIntroText(){_001_IntroText.gameObject.SetActive(false);}
    /// 001
    
    ///Event triggers
    public void EnableEventTrigger1()  {_level_1_TriggerEventEnabled = true; }
    public void DisableEventTrigger1() { _level_1_TriggerEventEnabled = false; }
    public void EnableEventTrigger2() {_level_2_TriggerEventEnabled = true; }
    public void DisableEventTrigger2() { _level_2_TriggerEventEnabled = false; }
    public void EnableEventTrigger3() {_level_3_TriggerEventEnabled = true; }
    public void DisableEventTrigger3() { _level_3_TriggerEventEnabled = false; }


    public void Show_002_1_EventTriggerMessage(){_002_1_EventTriggerMessage.gameObject.SetActive(true); _level_1_TriggerBeingShown = true;}
    public void Hide_002_1_EventTriggerMessage(){_002_1_EventTriggerMessage.gameObject.SetActive(false); _level_1_TriggerBeingShown = false;}
    public void Show_002_2_EventTriggerMessage(){_002_2_EventTriggerMessage.gameObject.SetActive(true); _level_2_TriggerBeingShown = true;}
    public void Hide_002_2_EventTriggerMessage(){_002_2_EventTriggerMessage.gameObject.SetActive(false); _level_2_TriggerBeingShown = false;}
    public void Show_002_3_EventTriggerMessage(){_002_3_EventTriggerMessage.gameObject.SetActive(true); _level_3_TriggerBeingShown = true;}
    public void Hide_002_3_EventTriggerMessage(){_002_3_EventTriggerMessage.gameObject.SetActive(false); _level_3_TriggerBeingShown = false;}

    public Boolean Is_1_TriggerBeingShown() {return _level_1_TriggerBeingShown;}
    public Boolean Is_2_TriggerBeingShown() {return _level_2_TriggerBeingShown;}
    public Boolean Is_3_TriggerBeingShown() {return _level_3_TriggerBeingShown;}

    public Boolean Is_1_TriggerEnabled() {return _level_1_TriggerEventEnabled;}
    public Boolean Is_2_TriggerEnabled() {return _level_2_TriggerEventEnabled;}
    public Boolean Is_3_TriggerEnabled() {return _level_3_TriggerEventEnabled;}

    ///Event triggers
}
