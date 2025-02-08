using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]    private GameObject[] walls;
    [SerializeField]    private GameObject[] windows;
    
    //Start before creating the game
    void Awake()
    {
        //activate mesh renderer of walls
        foreach (var wall in walls)
        {
            wall.GetComponent<MeshRenderer>().enabled = true;
        }
        foreach (var window in windows)
        {
            window.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
