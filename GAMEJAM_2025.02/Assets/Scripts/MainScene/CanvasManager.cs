using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CanvasManager : MonoBehaviour
{

    [SerializeField] private GameManager _gameManager;

    //This is waht appears in the begining
    [SerializeField] private TextMeshProUGUI _firstMessage;

    [SerializeField] private TextMeshProUGUI _firstTriggerMessage;  
    [SerializeField] private TextMeshProUGUI _percentage;
    /*[SerializeField] private TextMeshProUGUI _secondTriggerMessage;  
    [SerializeField] private TextMeshProUGUI _thirdTriggerMessage;  */
       
    [SerializeField]    private TextMeshProUGUI _FirstMiniGameFinishedMessage;


    private void Awake()
    {
        // Initial states
        _firstMessage.gameObject.SetActive(true);
        
        _firstTriggerMessage.gameObject.SetActive(false);
        _FirstMiniGameFinishedMessage.gameObject.SetActive(false);
        /*_secondTriggerMessage.gameObject.SetActive(false);
        _thirdTriggerMessage.gameObject.SetActive(false);*/
    }

    private void Start()
    {
        StartCoroutine(HideFirstMessageAfterDelay(10f)); // Hide first message after 10 seconds
    }

    private IEnumerator HideFirstMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _firstMessage.gameObject.SetActive(false);
    }

    // Show/hide first trigger message
    public void ShowFirstTriggerMessage() => _firstTriggerMessage.gameObject.SetActive(true);
    public void HideFirstTriggerMessage() => _firstTriggerMessage.gameObject.SetActive(false);

    // Show/hide second trigger message
  /*  public void ShowSecondTriggerMessage() => _secondTriggerMessage.gameObject.SetActive(true);
    public void HideSecondTriggerMessage() => _secondTriggerMessage.gameObject.SetActive(false);

    // Show/hide third trigger message
    public void ShowThirdTriggerMessage() => _thirdTriggerMessage.gameObject.SetActive(true);
    public void HideThirdTriggerMessage() => _thirdTriggerMessage.gameObject.SetActive(false);*/

    public void ChangePercentageText(float percentage)
    {
        _percentage.text = $"Power: {percentage}%";
    }

    public void ShowFirstMiniGameFinishedMessage()
    {
        _FirstMiniGameFinishedMessage.gameObject.SetActive(true);
    }

    public void HideFirstMiniGameFinishedMessage()
    {
        _FirstMiniGameFinishedMessage.gameObject.SetActive(false);
    }

    public void SetFirstMiniGameFinishedMessage(String message)
    {
        _FirstMiniGameFinishedMessage.text = message;
    }
}
