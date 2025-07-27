using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 25f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isGrounded;
    public int count = 0;

    public Joystick joystick; // Thêm Joystick

    public Button jumpButton;
    public Button attackButton;
    public Button shieldButton;
    public Button buffButton; // Nút cho buff (hồi máu)
    public Kiem swordScript;
    public Khien shieldScript;

    public TextMeshProUGUI jumpCooldownText;
    public TextMeshProUGUI attackCooldownText;
    public TextMeshProUGUI shieldCooldownText;
    public TextMeshProUGUI buffCooldownText;

    public Image jumpCooldownPanel;
    public Image attackCooldownPanel;
    public Image shieldCooldownPanel;
    public Image buffCooldownPanel;

    public float jumpCooldown = 1f;
    public float attackCooldown = 1f;
    public float shieldCooldown = 2f;
    public float buffCooldown = 3f;

    private bool canJump = true;
    private bool canAttack = true;
    private bool canUseShield = true;
    private bool canUseBuff = true;

    private float jumpTimer = 0f;
    private float attackTimer = 0f;
    private float shieldTimer = 0f;
    private float buffTimer = 0f;

    public static Player Instance;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject); // Giữ Player giữa các scene
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void EnableShieldColliderProxy()
    {
        shieldScript.EnableShieldCollider(); // Gọi từ Animator
    }

    public void EnableSwordColliderProxy()
    {
        swordScript.EnableSwordCollider(); // Gọi từ Animator
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Gán sự kiện cho button
        if (jumpButton != null) jumpButton.onClick.AddListener(Jump);
        if (attackButton != null) attackButton.onClick.AddListener(Attack);
        if (shieldButton != null) shieldButton.onClick.AddListener(Shield);
        if (buffButton != null) buffButton.onClick.AddListener(TriggerBuffAnimation);

        SetCooldownUIActive(false);
    }

    void SetCooldownUIActive(bool active)
    {
        if (jumpCooldownText != null) jumpCooldownText.gameObject.SetActive(active);
        if (attackCooldownText != null) attackCooldownText.gameObject.SetActive(active);
        if (shieldCooldownText != null) shieldCooldownText.gameObject.SetActive(active);
        if (buffCooldownText != null) buffCooldownText.gameObject.SetActive(active);

        if (jumpCooldownPanel != null) jumpCooldownPanel.gameObject.SetActive(active);
        if (attackCooldownPanel != null) attackCooldownPanel.gameObject.SetActive(active);
        if (shieldCooldownPanel != null) shieldCooldownPanel.gameObject.SetActive(active);
        if (buffCooldownPanel != null) buffCooldownPanel.gameObject.SetActive(active);
    }

    void Update()
    {
        if (rb.bodyType != RigidbodyType2D.Dynamic) return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        float moveX = joystick.GetInput().x;
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        animator.SetBool("walk", moveX != 0);

        if (moveX > 0 && !isFacingRight)
            Flip();
        else if (moveX < 0 && isFacingRight)
            Flip();

        UpdateCooldownUI();
    }

    public void Jump()
    {
        if (!canJump) return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        animator.SetBool("Jump", true);
        Invoke(nameof(StopJump), 0.5f);

        canJump = false;
        jumpTimer = jumpCooldown;
        jumpCooldownPanel.fillAmount = 1f;
        jumpCooldownText.gameObject.SetActive(true);
        jumpCooldownPanel.gameObject.SetActive(true);
        jumpButton.interactable = false;
    }

    void StopJump()
    {
        animator.SetBool("Jump", false);
    }

    public void Attack()
    {
        if (!canAttack) return;

        animator.SetBool("guomattack", true);
        Invoke(nameof(StopAttack), 0.5f);

        canAttack = false;
        attackTimer = attackCooldown;
        attackCooldownPanel.fillAmount = 1f;
        attackCooldownText.gameObject.SetActive(true);
        attackCooldownPanel.gameObject.SetActive(true);
        attackButton.interactable = false;
    }

    void StopAttack()
    {
        animator.SetBool("guomattack", false);
    }

    public void Shield()
    {
        if (!canUseShield) return;

        animator.SetBool("khien", true);
        Invoke(nameof(StopShield), 1f);

        canUseShield = false;
        shieldTimer = shieldCooldown;
        shieldCooldownPanel.fillAmount = 1f;
        shieldCooldownText.gameObject.SetActive(true);
        shieldCooldownPanel.gameObject.SetActive(true);
        shieldButton.interactable = false;
    }

    void StopShield()
    {
        animator.SetBool("khien", false);
    }

    public void TriggerBuffAnimation()
    {
        if (!canUseBuff) return;

        animator.SetBool("buff", true);
        Invoke(nameof(StopBuffAnimation), 1f);

        canUseBuff = false;
        buffTimer = buffCooldown;
        buffCooldownPanel.fillAmount = 1f;
        buffCooldownText.gameObject.SetActive(true);
        buffCooldownPanel.gameObject.SetActive(true);
        buffButton.interactable = false;
    }

    void StopBuffAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("buff", false);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void UpdateCooldownUI()
    {
        // JUMP
        if (!canJump)
        {
            jumpTimer -= Time.deltaTime;
            jumpTimer = Mathf.Max(jumpTimer, 0f);
            jumpCooldownText.text = jumpTimer.ToString("F1");
            jumpCooldownPanel.fillAmount = jumpTimer / jumpCooldown;

            if (jumpTimer <= 0f)
            {
                canJump = true;
                jumpButton.interactable = true;
                jumpCooldownText.text = "";
                jumpCooldownText.gameObject.SetActive(false);
                jumpCooldownPanel.gameObject.SetActive(false);
            }
        }

        // ATTACK
        if (!canAttack)
        {
            attackTimer -= Time.deltaTime;
            attackTimer = Mathf.Max(attackTimer, 0f);
            attackCooldownText.text = attackTimer.ToString("F1");
            attackCooldownPanel.fillAmount = attackTimer / attackCooldown;

            if (attackTimer <= 0f)
            {
                canAttack = true;
                attackButton.interactable = true;
                attackCooldownText.text = "";
                attackCooldownText.gameObject.SetActive(false);
                attackCooldownPanel.gameObject.SetActive(false);
            }
        }

        // SHIELD
        if (!canUseShield)
        {
            shieldTimer -= Time.deltaTime;
            shieldTimer = Mathf.Max(shieldTimer, 0f);
            shieldCooldownText.text = shieldTimer.ToString("F1");
            shieldCooldownPanel.fillAmount = shieldTimer / shieldCooldown;

            if (shieldTimer <= 0f)
            {
                canUseShield = true;
                shieldButton.interactable = true;
                shieldCooldownText.text = "";
                shieldCooldownText.gameObject.SetActive(false);
                shieldCooldownPanel.gameObject.SetActive(false);
            }
        }

        // BUFF
        if (!canUseBuff)
        {
            buffTimer -= Time.deltaTime;
            buffTimer = Mathf.Max(buffTimer, 0f);
            buffCooldownText.text = buffTimer.ToString("F1");
            buffCooldownPanel.fillAmount = buffTimer / buffCooldown;

            if (buffTimer <= 0f)
            {
                canUseBuff = true;
                buffButton.interactable = true;
                buffCooldownText.text = "";
                buffCooldownText.gameObject.SetActive(false);
                buffCooldownPanel.gameObject.SetActive(false);
            }
        }
    }
}