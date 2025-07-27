using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player1 : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 25f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isGrounded;
    private float moveX;

    public Button jumpButton;
    public Button kiemButton;
    public Button khienButton;
    public Button hoimauButton;

    public TextMeshProUGUI kiemCooldownText;
    public TextMeshProUGUI khienCooldownText;
    public TextMeshProUGUI hoimauCooldownText;
    public TextMeshProUGUI jumpCooldownText;

    public Image kiemCooldownPanel;
    public Image khienCooldownPanel;
    public Image hoimauCooldownPanel;
    public Image jumpCooldownPanel;

    public static Player1 Instance;

    public float kiemCooldown = 1f;
    public float khienCooldown = 2f;
    public float hoimauCooldown = 3f;
    public float jumpCooldown = 1f;

    private bool canUseKiem = true;
    private bool canUseKhien = true;
    private bool canUseHoiMau = true;
    private bool canJump = true;

    private float kiemTimer = 0f;
    private float khienTimer = 0f;
    private float hoimauTimer = 0f;
    private float jumpTimer = 0f;

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (jumpButton != null) jumpButton.onClick.AddListener(Jump);
        if (kiemButton != null) kiemButton.onClick.AddListener(Attack);
        if (khienButton != null) khienButton.onClick.AddListener(Shield);
        if (hoimauButton != null) hoimauButton.onClick.AddListener(TriggerBuffAnimation);

        SetCooldownUIActive(false);
    }

    void SetCooldownUIActive(bool active)
    {
        kiemCooldownText.gameObject.SetActive(active);
        khienCooldownText.gameObject.SetActive(active);
        hoimauCooldownText.gameObject.SetActive(active);
        jumpCooldownText.gameObject.SetActive(active);

        kiemCooldownPanel.gameObject.SetActive(active);
        khienCooldownPanel.gameObject.SetActive(active);
        hoimauCooldownPanel.gameObject.SetActive(active);
        jumpCooldownPanel.gameObject.SetActive(active);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        moveX = Input.GetAxisRaw("Horizontal");
        animator?.SetBool("walk", moveX != 0);

        if (moveX > 0 && !isFacingRight) Flip();
        else if (moveX < 0 && isFacingRight) Flip();

        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetKeyDown(KeyCode.Q)) Attack();
        if (Input.GetKeyDown(KeyCode.W)) Shield();
        if (Input.GetKeyDown(KeyCode.E)) TriggerBuffAnimation();
        if (Input.GetKeyDown(KeyCode.I)) ToggleInventory();

        UpdateCooldownUI();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        // if (!canJump || !isGrounded) return;
        if (!canJump) return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false;
        animator?.SetBool("Jump", true);
        Invoke(nameof(StopJump), 0.5f);

        canJump = false;
        jumpTimer = jumpCooldown;
        jumpCooldownPanel.fillAmount = 1f;
        jumpCooldownText.gameObject.SetActive(true);
        jumpCooldownPanel.gameObject.SetActive(true);
        jumpButton.interactable = false;
        //Invoke(nameof(ResetJumpCooldown), jumpCooldown);
    }

    void ResetJumpCooldown()
    {
        canJump = true;
        jumpButton.interactable = true;
    }

    void StopJump() => animator?.SetBool("Jump", false);

    void Attack()
    {
        if (!canUseKiem) return;

        animator?.SetBool("guomattack", true);
        Invoke(nameof(StopAttack), 0.5f);

        canUseKiem = false;
        kiemTimer = kiemCooldown;
        kiemCooldownPanel.fillAmount = 1f;
        kiemCooldownText.gameObject.SetActive(true);
        kiemCooldownPanel.gameObject.SetActive(true);
        kiemButton.interactable = false;
        // Xóa Invoke(nameof(ResetKiemCooldown), kiemCooldown);
    }

    void ResetKiemCooldown()
    {
        canUseKiem = true;
        kiemButton.interactable = true;
    }

    void StopAttack() => animator?.SetBool("guomattack", false);

    void Shield()
    {
        if (!canUseKhien) return;

        animator?.SetBool("khien", true);
        Invoke(nameof(StopShield), 1f);

        canUseKhien = false;
        khienTimer = khienCooldown;
        khienCooldownPanel.fillAmount = 1f;
        khienCooldownText.gameObject.SetActive(true);
        khienCooldownPanel.gameObject.SetActive(true);
        khienButton.interactable = false;
       // Invoke(nameof(ResetKhienCooldown), khienCooldown);
    }

    void ResetKhienCooldown()
    {
        canUseKhien = true;
        khienButton.interactable = true;
    }

    void StopShield() => animator?.SetBool("khien", false);

    void TriggerBuffAnimation()
    {
        if (!canUseHoiMau) return;

        animator?.SetBool("buff", true);
        Invoke(nameof(StopBuffAnimation), 1f);

        canUseHoiMau = false;
        hoimauTimer = hoimauCooldown;
        hoimauCooldownPanel.fillAmount = 1f;
        hoimauCooldownText.gameObject.SetActive(true);
        hoimauCooldownPanel.gameObject.SetActive(true);
        hoimauButton.interactable = false;
       // Invoke(nameof(ResetHoiMauCooldown), hoimauCooldown);
    }

    void ResetHoiMauCooldown()
    {
        canUseHoiMau = true;
        hoimauButton.interactable = true;
    }

    void StopBuffAnimation() => animator?.SetBool("buff", false);

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void ToggleInventory()
    {
        Debug.Log("Mở túi đồ");
    }

    void UpdateCooldownUI()
    {
        // KIẾM
        if (!canUseKiem)
        {
            kiemTimer -= Time.deltaTime;
            kiemTimer = Mathf.Max(kiemTimer, 0f); // Không để âm
            kiemCooldownText.text = kiemTimer.ToString("F1");
            kiemCooldownPanel.fillAmount = kiemTimer / kiemCooldown;

            if (kiemTimer <= 0f)
            {
                canUseKiem = true;
                kiemButton.interactable = true;
                kiemCooldownText.text = "";
                kiemCooldownText.gameObject.SetActive(false);
                kiemCooldownPanel.gameObject.SetActive(false);
            }
        }

        // KHIÊN
        if (!canUseKhien)
        {
            khienTimer -= Time.deltaTime;
            khienTimer = Mathf.Max(khienTimer, 0f);
            khienCooldownText.text = khienTimer.ToString("F1");
            khienCooldownPanel.fillAmount = khienTimer / khienCooldown;

            if (khienTimer <= 0f)
            {
                canUseKhien = true;
                khienButton.interactable = true;
                khienCooldownText.text = "";
                khienCooldownText.gameObject.SetActive(false);
                khienCooldownPanel.gameObject.SetActive(false);
            }
        }

        // BUFF
        if (!canUseHoiMau)
        {
            hoimauTimer -= Time.deltaTime;
            hoimauTimer = Mathf.Max(hoimauTimer, 0f);
            hoimauCooldownText.text = hoimauTimer.ToString("F1");
            hoimauCooldownPanel.fillAmount = hoimauTimer / hoimauCooldown;

            if (hoimauTimer <= 0f)
            {
                canUseHoiMau = true;
                hoimauButton.interactable = true;
                hoimauCooldownText.text = "";
                hoimauCooldownText.gameObject.SetActive(false);
                hoimauCooldownPanel.gameObject.SetActive(false);
            }
        }

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
    }
}
