using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event System.Action<int> EnemyDied;

    [Header("Enemy Info")]
    public int enemyID;
    private Animator animator;
    private bool isDead = false;

    [Header("Item Drop")]
    [SerializeField] public GameObject itemDropPrefab;
    [Range(0f, 1f)] public float dropRate = 0.5f; // Tỷ lệ rơi item (50%)

    private ScoreManager scoreManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Kiem") || other.CompareTag("Khien"))
        {
            scoreManager.AddScore(Random.Range(3, 8)); // Cộng điểm
            StartCoroutine(DieSequence()); // Bắt đầu quá trình chết
        }
    }

    IEnumerator DieSequence()
    {
        isDead = true;

        if (animator != null)
        {
            animator.SetBool("die", true);
            yield return new WaitForSeconds(0.3f); // Đợi animation
        }

        Die();
    }

    public void Die()
    {
        // Tỉ lệ rơi vật phẩm
        if (itemDropPrefab != null && Random.value <= dropRate)
        {
            Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
        }

        EnemyDied?.Invoke(enemyID);
        Destroy(gameObject); // Xóa enemy
    }
}
