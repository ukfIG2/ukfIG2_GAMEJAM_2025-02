using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GameObject[] clothesPrefabs; // Array to store 4 different clothes prefabs
    [SerializeField] private int treeCount = 100;

    private Vector3 planeSize;

    void Start()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer == null)
        {
            Debug.LogError("Attach this script to a GameObject with a MeshRenderer!");
            return;
        }

        planeSize = renderer.bounds.size;
        GenerateForest();
        SpawnClothes();
    }

    void GenerateForest()
    {
        for (int i = 0; i < treeCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            Quaternion rotation = Quaternion.Euler(-90, Random.Range(0, 360), 0);
            Instantiate(treePrefab, randomPosition, rotation);
        }
    }

    void SpawnClothes()
{
    if (clothesPrefabs.Length != 4)
    {
        Debug.LogError("You must assign exactly 4 different clothes prefabs!");
        return;
    }

    for (int i = 0; i < clothesPrefabs.Length; i++)
    {
        for (int j = 0; j < 2; j++) // Each prefab spawns twice
        {
            Vector3 randomPosition = GetRandomPosition();
            randomPosition.y = -0.1f; // Set Y position to 0.1
            Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0); // Clothes should stay upright
            Instantiate(clothesPrefabs[i], randomPosition, rotation);
        }
    }
}


    Vector3 GetRandomPosition()
    {
        float halfX = planeSize.x / 2;
        float halfZ = planeSize.z / 2;
        float y = -0.5f; // Keep objects on the plane level

        float randomX = Random.Range(-halfX, halfX) + transform.position.x;
        float randomZ = Random.Range(-halfZ, halfZ) + transform.position.z;

        return new Vector3(randomX, y, randomZ);
    }
}
