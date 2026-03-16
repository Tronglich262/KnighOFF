using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public AudioSource coinAudioPrefab; 
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

            Destroy(gameObject); 
        }
    }

}
