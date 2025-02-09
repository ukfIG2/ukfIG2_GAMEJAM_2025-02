using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;


public class SecondLevelFinishedScene : MonoBehaviour
{
    [SerializeField]    private TextMeshProUGUI _secondMiniText;
    [SerializeField]    private GameManager _gameManager;
    [SerializeField] private Sprite _bearIdle;

    [SerializeField] private Sprite _bearWave1;
    [SerializeField] private Sprite _bearWave2;
    
    [SerializeField] private GameObject _bear;
    [SerializeField] private GameObject _sun;
    [SerializeField] private GameObject _key;
    [SerializeField] private GameObject _house;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _playerDefaultPosition;
    

    private void Awake()
    {

        _sun.gameObject.SetActive(false);
        _secondMiniText.gameObject.SetActive(true);
        _bear.gameObject.SetActive(true);
    }

    private void Start()
    {
        
        // Start the sequence
        StartCoroutine(LevelEndSequence());
    }

    private IEnumerator LevelEndSequence()
    {
        
        // Step 2: Move Bear to z = -3.1 smoothly
        yield return StartCoroutine(MoveToPosition(_bear, 0.44f, 2f));

        yield return new WaitForSeconds(2f);

        // Step 3: Change Bear sprite to Bear2
        _secondMiniText.text = "That you asshole from saving us from your trash. ";

        yield return new WaitForSeconds(5f);

        // Step 4: Change Bear sprite to Bear5
        _secondMiniText.text = "As token of our gratitude we will take your SUN !!!";

        yield return new WaitForSeconds(2f);

        // Step 5: Activate Sun
        _sun.SetActive(true);
        _key.SetActive(true);
        _gameManager.SunStolen();
        Debug.Log("Step 5: Activated Sun");

        yield return new WaitForSeconds(3f);
        _key.SetActive(false);
        _gameManager.KeyAquired();

        for(int i = 0; i <=20; i++)
        {
            yield return new WaitForSeconds(0.5f);
            if(i%2==0){ChangeSprite(_bear, _bearWave1);}
            else {ChangeSprite(_bear, _bearWave2);}
        }

        //Move player to default position

       // _player.gameObject.SendMessage("ToggleMode");
        _player.gameObject.SendMessage("LockCursor");
        _player.gameObject.SendMessage("EnableRotation");
        _player.transform.position = _playerDefaultPosition.position;
        _player.transform.rotation = _playerDefaultPosition.rotation;
        Destroy(gameObject);


    }

    private void ChangeSprite(GameObject obj, Sprite newSprite)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = newSprite;
        }
        else
        {
            Debug.LogError("No SpriteRenderer found on " + obj.name);
        }
    }

    private IEnumerator MoveToPosition(GameObject obj, float targetZ, float duration)
        {
            float elapsedTime = 0f;
            Vector3 startPosition = obj.transform.localPosition;
            Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y, targetZ);

            while (elapsedTime < duration)
            {
                obj.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure final position is set
            obj.transform.localPosition = targetPosition;
        }
}
