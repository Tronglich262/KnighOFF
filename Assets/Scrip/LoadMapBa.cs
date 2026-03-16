using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadMapBa: MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ScoreManager.Instance.currentScore >= 250)
        {
            
            SceneManager.LoadScene("End");
        }
    }
}