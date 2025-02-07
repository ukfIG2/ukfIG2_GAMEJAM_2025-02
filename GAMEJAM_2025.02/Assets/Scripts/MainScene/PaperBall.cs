using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBall : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        //find script GameManager in object with tag GameManager
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("TargetToHit"))
        {
            _gameManager.BallHit();
            Destroy(gameObject);
        }
    }
}
