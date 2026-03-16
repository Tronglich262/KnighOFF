using UnityEngine;
using System.Collections;

public class LightningSpawner : MonoBehaviour
{
    public GameObject lightningPrefab;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 2f;
    public float lightningLifetime = 0.5f;

    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    void Start()
    {
        StartCoroutine(SpawnLightning());
    }

    IEnumerator SpawnLightning()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            // Chọn vị trí random trong vùng spawn
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPosition = new Vector2(randomX, randomY);

            // Tạo sét tại vị trí ngẫu nhiên
            GameObject lightning = Instantiate(lightningPrefab, spawnPosition, Quaternion.identity);

            // Xóa sét sau một thời gian ngắn
            Destroy(lightning, lightningLifetime);
        }
    }
}