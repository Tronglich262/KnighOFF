using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public int coinValue = 5; // Giá trị của coin
    public AudioSource coinAudioPrefab; // Prefab chứa AudioSource có sẵn clip

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Cộng vàng
            ScoreManager.Instance.AddScore(coinValue);

            // Hiện chữ bay
            FloatingTextSpawner.Instance.SpawnText($"+{coinValue} vàng", transform.position);

            // Phát âm thanh bằng prefab tách riêng
            if (coinAudioPrefab != null)
            {
                AudioSource audioInstance = Instantiate(coinAudioPrefab, transform.position, Quaternion.identity);
                audioInstance.Play();
                Destroy(audioInstance.gameObject, audioInstance.clip.length); // Xoá sau khi phát xong
            }

            // Xoá coin ngay (âm thanh vẫn phát riêng)
            Destroy(gameObject);
        }
    }
}
