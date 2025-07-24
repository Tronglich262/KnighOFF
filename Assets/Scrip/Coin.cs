using UnityEngine;
using TMPro; // Import để sử dụng TextMeshPro

public class Coin : MonoBehaviour
{
    public int coinValue = 5; // Giá trị của coin
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(coinValue);
            FloatingTextSpawner.Instance.SpawnText($"+{coinValue} vàng", transform.position);
            Destroy(gameObject);
        }
    }

}