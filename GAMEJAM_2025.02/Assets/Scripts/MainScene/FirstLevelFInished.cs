using System.Collections;
using UnityEngine;

public class FirstLevelFinished : MonoBehaviour
{
    [SerializeField]    private CanvasManager _canvasManager;
    [SerializeField]    private GameManager _gameManager;
    [SerializeField]    private MainPlayer _mainPlayer;
    [SerializeField] private Sprite Bear2;
    [SerializeField] private Sprite Bear5;
    [SerializeField] private Sprite Church2;
    

    private GameObject _church;
    private GameObject _bear;
    [SerializeField]    private GameObject _sun;

    private void Start()
    {
        // Find objects by tag
        _church = GameObject.FindGameObjectWithTag("Churge");
        _bear = GameObject.FindGameObjectWithTag("Bear");
        //_sun = GameObject.FindGameObjectWithTag("Sun");

        if (_church == null || _bear == null || _sun == null)
        {
            Debug.LogError("Missing required GameObjects in the scene!");
            return;
        }

        // Start the sequence
        StartCoroutine(LevelEndSequence());
    }

    private IEnumerator LevelEndSequence()
    {
        // Step 1: Change Church sprite
        ChangeSprite(_church, Church2);
        Debug.Log("Step 1: Changed Church sprite");
        yield return new WaitForSeconds(2f);

        // Step 2: Move Bear to z = -3.1 smoothly
        yield return StartCoroutine(MoveToPosition(_bear, -3.1f, 2f));
        Debug.Log("Step 2: Moved Bear");

        yield return new WaitForSeconds(5f);

        // Step 3: Change Bear sprite to Bear2
        ChangeSprite(_bear, Bear2);
        _canvasManager.ShowFirstMiniGameFinishedMessage();
        _canvasManager.SetFirstMiniGameFinishedMessage("Thank you adventurer for saving us from your trash!");
        Debug.Log("Step 3: Changed Bear sprite to Bear2");

        yield return new WaitForSeconds(5f);

        // Step 4: Change Bear sprite to Bear5
        ChangeSprite(_bear, Bear5);
        _canvasManager.SetFirstMiniGameFinishedMessage("As token of gratitude please take our sun");
        Debug.Log("Step 4: Changed Bear sprite to Bear5");

        yield return new WaitForSeconds(5f);

        // Step 5: Activate Sun
        _canvasManager.HideFirstMiniGameFinishedMessage();
        _sun.SetActive(true);
        Debug.Log("Step 5: Activated Sun");

        yield return new WaitForSeconds(5f);
        _sun.SetActive(false);
        _gameManager.SunAquired = true;



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
