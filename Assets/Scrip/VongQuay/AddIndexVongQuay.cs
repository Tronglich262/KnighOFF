using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource coinAudioPrefab; // Prefab chứa AudioSource có sẵn clip

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LuckySpin.instance.AddSpinCount(1);

            if (coinAudioPrefab != null)
            {
                AudioSource audioInstance = Instantiate(coinAudioPrefab, transform.position, Quaternion.identity);
                audioInstance.Play();
                Destroy(audioInstance.gameObject, audioInstance.clip.length);
            }

            Destroy(gameObject); // Hủy coin ngay sau khi nhặt
        }
    }

}
