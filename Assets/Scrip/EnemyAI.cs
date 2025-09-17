using System.Collections;
using UnityEngine;

public enum TypeEnemy
{
    Quaidoi,   // quái bay
    Qualua,    // quái lửa - dưới đất
    Slim,      // quái slime - dưới đất
}

public class EnemyAI : MonoBehaviour
{
    [Header("Điểm tuần tra")]
    public Transform pointA;
    public Transform pointB;

    [Header("Loại quái")]
    public TypeEnemy typeEnemy;

    [Header("Chỉ số tốc độ")]
    public float speed = 2f;
    public float chaseSpeed = 4f;
    public float stopDistance = 0.5f;

    private Transform player;
    private Animator animator;
    private Rigidbody2D rb;

    private bool movingToB = true;
    private bool isChasing = false;
    private bool isAttacking = false; // chống spam tấn công

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy Player!");
        }
    }

    void Update()
    {
        if (player == null || isAttacking) return;

        bool playerInZone = IsPlayerBetweenAandB();

        switch (typeEnemy)
        {
            case TypeEnemy.Quaidoi: // quái bay
                if (playerInZone)
                    FlyChasePlayer();
                else
                    FlyPatrol();
                break;

            case TypeEnemy.Qualua: // quái dưới đất
                isChasing = playerInZone;
                if (isChasing) GroundChasePlayer();
                else GroundPatrol(speed);
                break;

            case TypeEnemy.Slim: // slime chậm hơn
                isChasing = playerInZone;
                if (isChasing) GroundChasePlayer();
                else GroundPatrol(speed * 0.5f);
                break;
        }
    }

    /// <summary>
    /// Tự di chuyển giữa a và B  , focus player trục X và tấn công khi gần
    /// </summary>

    // ========== QUÁI DƯỚI ĐẤT ==========
    void GroundPatrol(float moveSpeed)
    {
        if (pointA == null || pointB == null) return;

        Vector2 target = new Vector2((movingToB ? pointB : pointA).position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        Flip(target.x);

        if (Vector2.Distance(transform.position, target) < 0.1f)
            movingToB = !movingToB;
    }

    void GroundChasePlayer()
    {
        Vector2 targetPos = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, chaseSpeed * Time.deltaTime);
        Flip(player.position.x);

        if (Vector2.Distance(transform.position, player.position) < stopDistance)
            StartCoroutine(AttackRoutine());
    }


    /// <summary>
    /// đi chuyển giữa A và B , bay theo 2D và tấn công khi
    /// </summary>
    // ========== QUÁI BAY ==========
    void FlyPatrol()
    {
        if (pointA == null || pointB == null) return;

        Transform target = movingToB ? pointB : pointA;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        Flip(target.position.x);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
            movingToB = !movingToB;
    }

    void FlyChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        Flip(player.position.x);

        if (Vector2.Distance(transform.position, player.position) < stopDistance)
            StartCoroutine(AttackRoutine());
    }


    /// <summary>
    /// attack player ( tính năng chung của 2 loại quái ) tấn công xong lùi lại 1 đoạn rồi mới tấn công tiếp
    /// </summary>

    // ========== CHUNG ==========
    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        // Animation tấn công
        animator.SetTrigger("Damage");
        Debug.Log($"{typeEnemy} tấn công Player!");

        yield return new WaitForSeconds(0.2f); // delay đánh trúng (nếu có hitbox thì để ở đây)

        // Lùi lại sau khi tấn công
        float retreatTime = 0.4f;
        float retreatDistance = 2f;

        Vector2 retreatDirection;

        if (typeEnemy == TypeEnemy.Quaidoi)
        {
            // Quái bay lùi theo 2D
            retreatDirection = (transform.position - player.position).normalized;
        }
        else
        {
            // Quái đất chỉ lùi theo trục X
            float dirX = Mathf.Sign(transform.position.x - player.position.x); // +1 hoặc -1
            retreatDirection = new Vector2(dirX, 0);
        }

        Vector2 retreatTarget = (Vector2)transform.position + retreatDirection * retreatDistance;

        float elapsedTime = 0;
        while (elapsedTime < retreatTime)
        {
            transform.position = Vector2.MoveTowards(transform.position, retreatTarget, (retreatDistance / retreatTime) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Chờ 0.5s rồi mới tấn công tiếp
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    bool IsPlayerBetweenAandB()
    {
        if (pointA == null || pointB == null) return false;

        float minX = Mathf.Min(pointA.position.x, pointB.position.x);
        float maxX = Mathf.Max(pointA.position.x, pointB.position.x);

        return player.position.x > minX && player.position.x < maxX;
    }

    void Flip(float targetX)
    {
        if ((targetX < transform.position.x && transform.localScale.x < 0) ||
            (targetX > transform.position.x && transform.localScale.x > 0))
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }
}
