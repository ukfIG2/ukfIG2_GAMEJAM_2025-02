using UnityEngine;

public class Paper : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //find script GameManager in object with tag GameManager
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _gameManager.AddPaperBallToList(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("TrashCanTarget"))
        {
            _gameManager.ThrowPapersToTrashCan();
            _gameManager.RemoveFromList(gameObject);
            Destroy(gameObject);
        }
    }
}
