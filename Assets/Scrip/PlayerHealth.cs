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
    public AudioClip deathClip;
    public AudioClip sao;
    private AudioSource audioSource;      // Component phát âm thanh

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        audioSource = GetComponent<AudioSource>();

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
        if (isDead) return;

        isDead = true;

        Debug.Log("Người chơi đã chết!");

        // Dừng mọi hoạt động vật lý
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;

        // Phát âm thanh chết (dùng PlayOneShot để không cần gán clip trước)
        if (audioSource != null && deathClip != null)
        {
            audioSource.PlayOneShot(deathClip);
        }

        // Animation chết
        animator.SetBool("Die", true);

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
            audioSource.PlayOneShot(sao);
            yield return new WaitForSeconds(0.5f);
            yeusao.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);

            yield return new WaitForSeconds(0.5f);
            trungbinhsao.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);

            yield return new WaitForSeconds(0.5f);
            totsao.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);

        }
        else if (ScoreManager.Instance.currentScore >= 600)
        {
            yield return new WaitForSeconds(0.5f);
            huyhieutrungbinh.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);

            yield return new WaitForSeconds(0.5f);
            yeusao.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);

            yield return new WaitForSeconds(0.5f);
            trungbinhsao.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);

        }
        else if (ScoreManager.Instance.currentScore >= 300)
        {
            yield return new WaitForSeconds(0.5f);
            audioSource.PlayOneShot(sao);

            huyhieuyeu.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            yeusao.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);

            yield return new WaitForSeconds(0.5f);

        }
        else if (ScoreManager.Instance.currentScore <= 300)
        {
            yield return new WaitForSeconds(0.5f);
            huyhieulose.gameObject.SetActive(true);
            audioSource.PlayOneShot(sao);


        }

    }

}   