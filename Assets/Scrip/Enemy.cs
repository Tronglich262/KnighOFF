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


    [Header("Dame Vong QUay 20%")]
    public float takedame = 5f;
    public float damebuff = 20f;
    public float currenttakedame;

    private ScoreManager scoreManager;

    private void Start()
    {
        currenttakedame = takedame;
        animator = GetComponent<Animator>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (isDead) return;

        if (other.CompareTag("Kiem") || other.CompareTag("Khien"))
        {
            scoreManager.AddScore(Random.Range(3, 8));

            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(currenttakedame);
            }
        }

    }

    public IEnumerator DieSequence()
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
    private void OnEnable()
    {
        PhanThuong.sathuongbuff += DameBuff1;
    }
    private void OnDisable()
    {
        PhanThuong.sathuongbuff -= DameBuff1;
    }
    public void DameBuff1()
    {
        StartCoroutine(damebuffIE());
    }
    IEnumerator damebuffIE()
    {
        currenttakedame = takedame + damebuff;
        yield return new WaitForSeconds(10f);
        currenttakedame = takedame;
    }
}
