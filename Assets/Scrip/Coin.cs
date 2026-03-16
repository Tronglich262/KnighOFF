using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public int coinValue = 5; 
    public AudioSource coinAudioPrefab; 



    /// <summary>
    /// Xử lý khi người chơi chạm vào coin
    /// </summary>
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(coinValue);
            FloatingTextSpawner.Instance.SpawnText($"+{coinValue} vàng", transform.position);
            if (coinAudioPrefab != null)
            {
                AudioSource audioInstance = Instantiate(coinAudioPrefab, transform.position, Quaternion.identity);
                audioInstance.Play();
                Destroy(audioInstance.gameObject, audioInstance.clip.length); 
            }
            Destroy(gameObject);
        }
    }
}
