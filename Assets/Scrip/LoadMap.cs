using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadMap : MonoBehaviour
{
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

