using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour
{
    public AudioClip clickSound;   // Kéo file âm thanh vào Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Tìm AudioSource chung trong scene (VD: gắn vào Canvas)
        audioSource = FindObjectOfType<AudioSource>();

        // Gắn sự kiện click cho button
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
