using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject itemPrefab;  
    public float spawnInterval = 0.3f; 
    public float minX = -8f, maxX = 8f; 
    public float spawnHeight = 5f; 

    void Start()
    {
        InvokeRepeating("SpawnItem", 0f, spawnInterval);
    }

    void SpawnItem()
    {
        float randomX = Random.Range(minX, maxX); 
        float spawnY = transform.position.y + spawnHeight; 
        Vector2 spawnPosition = new Vector2(randomX, spawnY);
        
        // Tạo vật phẩm tại vị trí ngẫu nhiên và cao hơn
        Instantiate(itemPrefab, spawnPosition, Quaternion.Euler(0f, 0f, -90f));
    }
}