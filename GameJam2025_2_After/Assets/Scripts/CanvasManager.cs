using System;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private Boolean _somethingIsMissing;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _001_IntroText;
    
    private void Awake()
    {
        if(_gameManager == null) {Debug.LogWarning("GameManager empty, FIX NOW!!!"); _somethingIsMissing = true;}
        if(_001_IntroText == null) {Debug.LogWarning("_001_IntroText empty, FIX NOW!!!"); _somethingIsMissing = true;}

        if(_somethingIsMissing) {Application.Quit();}
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _001_IntroText.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowIntroText()
    {
        _001_IntroText.gameObject.SetActive(true);
    }
    public void HideIntroText()
    {
        _001_IntroText.gameObject.SetActive(false);
    }
}
