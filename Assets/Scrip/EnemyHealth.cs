using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Sát thương tấn công player")]
    public int damageToPlayer = 5;
    public int DameVongQuay = 1;
    public int dameVongquaybatu = 0;
    public int currendame;

    [Header("Thanh Máu")]
    public float health = 20f;
    public float currentHealth;
    public Slider healthBar;

    private Animator animator;
    private Enemy enemy;

    private void Start()
    {
        currentHealth = health;
        healthBar.maxValue = health;
        healthBar.value = currentHealth;
        currendame = damageToPlayer;
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        
    }

    private void Update()
    {
        healthBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;

        StartCoroutine(HitEffect());

        if (currentHealth <= 0)
        {
            if (enemy != null)
            {
                enemy.StartCoroutine(enemy.DieSequence()); 
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator HitEffect()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(currendame);
                Debug.Log("Enemy tấn công Player! HP");
            }
        }
    }
    public void GiapVongQuay()
    {
        StartCoroutine(Giap());
    }
    public void battu1()
    {
        StartCoroutine(battu());
    }
    IEnumerator Giap()
    {
        currendame = DameVongQuay;
        yield return new WaitForSeconds(5f);
        currendame = damageToPlayer;
    }
    IEnumerator battu()
    {
        currendame = dameVongquaybatu;
        yield return new WaitForSeconds(5f);
        currendame = damageToPlayer;
    }
    private void OnEnable()
    {
        PhanThuong.OnThuongTiaset1 += GiapVongQuay;
        PhanThuong.OnThuongTiaset2 += battu1;
    }
    private void OnDisable()
    {
        PhanThuong.OnThuongTiaset1 -= GiapVongQuay;
        PhanThuong.OnThuongTiaset2 -= battu1;
    }
}
