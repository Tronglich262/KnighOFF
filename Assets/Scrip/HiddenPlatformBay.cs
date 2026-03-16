using UnityEngine;

public class HiddenPlatformBay : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer; 

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player chạm vào, ẩn nền!");
            boxCollider.enabled = false;

            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
        }
    }
}