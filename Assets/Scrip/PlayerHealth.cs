using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false; // Kiểm tra nhân vật đã chết chưa

    public Slider healthBar;
    private Animator animator;
    private Player player;
    public GameObject panelgame;
    public Image huyhieulose;
    public Image yeusao;
    public Image trungbinhsao;
    public Image totsao;
    public Image huyhieuyeu;
    public Image huyhieutrungbinh;
    public Image huyhieutot;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();

        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        if (maxHealth <= 0)
        {
            ScoreManager.Instance.ResetScore();
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return; // Nếu đã chết thì không nhận sát thương

        Debug.Log("Va chạm với: " + collision.gameObject.name);

        //if (collision.gameObject.CompareTag("Bullet2"))
        //{
        //    StartCoroutine(Hit());
        //    TakeDamage(2);
        //    Destroy(collision.gameObject);
        //}
        //else if (collision.gameObject.CompareTag("Bullet3"))
        //{
        //    StartCoroutine(Hit());
        //    TakeDamage(20);
        //    Destroy(collision.gameObject);
        //}
        if (collision.gameObject.CompareTag("Dinh"))
        {
            StartCoroutine(Hit());
            TakeDamage(100);
        }
    }

    IEnumerator Hit()
    {
        if (isDead) yield break; // Nếu đã chết thì không chạy animation bị đánh

        animator.SetBool("Hit", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Hit", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return; // Nếu đã chết thì không nhận sát thương

        //if (other.gameObject.CompareTag("Bullet4"))
        //{
        //    StartCoroutine(Hit());
        //    TakeDamage(50);
        //}
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Hit());
            TakeDamage(2);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Nếu đã chết thì không trừ máu

        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();

        }
    }

    void Die()
    {
        if (isDead) return; // Nếu đã chết rồi thì không gọi lại

        isDead = true; // Đánh dấu nhân vật đã chết
        Debug.Log("Người chơi đã chết!");

        // Dừng mọi hoạt động của nhân vật
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false; // Vô hiệu hóa va chạm
        // Chạy animation chết
        animator.SetBool("Die", true); // Giữ nhân vật ở animation chết
        StartCoroutine(ResetGame());



    }
        
    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }
    public void IncreaseHealth(int amount)
    {
        if (currentHealth >= maxHealth)
        {
            Debug.Log("Máu đã đầy, không thể hồi thêm!");
            return;
        }

        int oldHealth = currentHealth;
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        int actualHeal = currentHealth - oldHealth;

        Debug.Log("Hồi máu: " + actualHeal + ", Máu hiện tại: " + currentHealth);
        UpdateHealthUI();

        // Gọi Player kích hoạt animation buff
        if (player != null)
        {
            player.TriggerBuffAnimation();
        }
    }
    public bool IsHealthFull()
    {
        return currentHealth >= maxHealth;
    }


    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(2f);
        panelgame.SetActive(true);
       StartCoroutine(lanluothiensao());

    }
    IEnumerator lanluothiensao()
    {
        yield return new WaitForSeconds(0.5f);
        if (ScoreManager.Instance.currentScore >= 1000)
        {
            yield return new WaitForSeconds(0.5f);
            huyhieutot.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            yeusao.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            trungbinhsao.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            totsao.gameObject.SetActive(true);
        }
        else if (ScoreManager.Instance.currentScore >= 600)
        {
            yield return new WaitForSeconds(0.5f);
            huyhieutrungbinh.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            yeusao.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            trungbinhsao.gameObject.SetActive(true);
        }
        else if (ScoreManager.Instance.currentScore >= 300)
        {
            yield return new WaitForSeconds(0.5f);
            huyhieuyeu.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            yeusao.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);

        }
        else if (ScoreManager.Instance.currentScore <= 300)
        {
            yield return new WaitForSeconds(0.5f);
            huyhieulose.gameObject.SetActive(true);

        }

    }

}   