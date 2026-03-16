using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour
{
    public AudioClip clickSound;  
    private AudioSource audioSource;

    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    public void PlaySound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
