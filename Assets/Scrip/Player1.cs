using UnityEngine;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Lấy input trong Update
        moveX = Input.GetAxisRaw("Horizontal");

        // Animation di chuyển
        if (animator != null)
            animator.SetBool("walk", moveX != 0);

        // Đổi hướng
        if (moveX > 0 && !isFacingRight) Flip();
        else if (moveX < 0 && isFacingRight) Flip();

        // Nhảy
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Tấn công
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }

        // Khiên
        if (Input.GetKeyDown(KeyCode.W))
        {
            Shield();
        }

        // Buff
        if (Input.GetKeyDown(KeyCode.E))
        {
            TriggerBuffAnimation();
        }

        // Mở túi đồ
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void FixedUpdate()
    {
        // Di chuyển vật lý mượt hơn
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false;
        if (animator != null)
            animator.SetBool("Jump", true);
        Invoke(nameof(StopJump), 0.5f);
    }

    void StopJump()
    {
        if (animator != null)
            animator.SetBool("Jump", false);
    }

    void Attack()
    {
        if (animator != null)
        {
            animator.SetBool("guomattack", true);
            Invoke(nameof(StopAttack), 0.5f);
        }
    }

    void StopAttack()
    {
        if (animator != null)
            animator.SetBool("guomattack", false);
    }

    void Shield()
    {
        if (animator != null)
        {
            animator.SetBool("khien", true);
            Invoke(nameof(StopShield), 1f);
        }
    }

    void StopShield()
    {
        if (animator != null)
            animator.SetBool("khien", false);
    }

    void TriggerBuffAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("buff", true);
            Invoke(nameof(StopBuffAnimation), 1f);
        }
    }

    void StopBuffAnimation()
    {
        if (animator != null)
            animator.SetBool("buff", false);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void ToggleInventory()
    {
        Debug.Log("Mở túi đồ");
        // Thêm code bật/tắt UI túi đồ ở đây
    }
}
