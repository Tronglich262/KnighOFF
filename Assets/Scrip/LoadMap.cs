using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadMap : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ScoreManager.Instance.currentScore >= 100)
        {
            
            SceneManager.LoadScene("MapHai");
        }
        if(collision.gameObject.CompareTag("Player") && ScoreManager.Instance.currentScore <= 100)
        {
            FloatingTextSpawner.Instance.SpawnText($"Yêu cầu số điểm phải hơn 100", transform.position);

        }
    }
    }

