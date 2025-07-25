using System.Collections;
using UnityEngine;

public class BossEnd : MonoBehaviour
{
    [Header("References")]
    public Transform pointA, pointB, player;
    public GameObject healthBarUI;
    public Transform[] teleportPoints;

    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float stopDistance = 1.5f;

    [Header("Attack Settings")]
    public float attackCooldown = 1f;
    public float idleBeforeAttackTime = 0.5f;
    public float retreatDistance = 3f;
    public float retreatTime = 0.3f;
    public float actionInterval = 0.5f;

    private Animator animator;
    private Rigidbody2D rb;
    private BossHealth bossHealth;

    private bool isAttacking = false;
    private bool isCollidingWithPlayer = false;
    private bool movingToB = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bossHealth = GetComponent<BossHealth>();

        if (healthBarUI != null)
            healthBarUI.SetActive(false);

        if (player == null)
        {
            var foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer) player = foundPlayer.transform;
            else Debug.LogError("Player not found!");
        }
    }

    void Update()
    {
        if (isAttacking || bossHealth.currentHealth <= 0) return;

        if (IsPlayerBetweenAandB())
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= stopDistance && !isAttacking)
                StartCoroutine(AttackLoop());
            else
                ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        Transform target = movingToB ? pointB : pointA;
        transform.position = Vector2.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);
        Flip(target.position.x);
        animator.SetBool("Running", true);
        animator.SetBool("Attack", false);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            movingToB = !movingToB;
            animator.SetBool("Running", false);
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        Flip(player.position.x);
        animator.SetBool("Running", true);
        animator.SetBool("Attack", false);
    }

    IEnumerator AttackLoop()
    {
        isAttacking = true;
        animator.SetBool("Running", false);

        while (isCollidingWithPlayer && bossHealth.currentHealth > 0)
        {
            yield return StartCoroutine(PerformRandomAttack());
            yield return new WaitForSeconds(actionInterval);
        }

        animator.SetBool("Attack", false);
        isAttacking = false;
    }

    IEnumerator PerformRandomAttack()
    {
        int pattern = Random.Range(0, 3);
        switch (pattern)
        {
            case 0: yield return MeleeAttack(); break;
            case 1: yield return IdleStare(); break;
            case 2: yield return TeleportAway(); break;
        }
    }

    IEnumerator MeleeAttack()
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(attackCooldown);
        yield return RetreatAfterAttack();
    }

    IEnumerator IdleStare()
    {
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(idleBeforeAttackTime);
    }

    IEnumerator TeleportAway()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Jump", true);
        JumpToRandomPoint();
        yield return new WaitForSeconds(2f);

        if (bossHealth.currentHealth > bossHealth.maxHealth * 0.3f)
        {
            TeleportNearPlayer();
            yield return new WaitForSeconds(0.2f);
            animator.SetBool("Jump", false);
            animator.SetBool("Attack", true);
            yield return new WaitForSeconds(attackCooldown);
            yield return RetreatAfterAttack();

            if (Random.value < 0.5f)
            {
                JumpToRandomPoint();
                yield return new WaitForSeconds(0.3f);
            }
        }
        else
        {
            animator.SetBool("Jump", false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void TeleportNearPlayer()
    {
        Vector2 offset = (player.position.x < transform.position.x) ? new Vector2(1.5f, 0) : new Vector2(-1.5f, 0);
        transform.position = (Vector2)player.position + offset;
        Flip(player.position.x);
    }

    void JumpToRandomPoint()
    {
        if (teleportPoints.Length == 0) return;
        int rand = Random.Range(0, teleportPoints.Length);
        transform.position = teleportPoints[rand].position;
    }

    IEnumerator RetreatAfterAttack()
    {
        Vector2 dir = (transform.position - player.position).normalized;
        Vector2 target = (Vector2)transform.position + dir * retreatDistance;

        float t = 0;
        while (t < retreatTime)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, (retreatDistance / retreatTime) * Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);
    }

    bool IsPlayerBetweenAandB()
    {
        float minX = Mathf.Min(pointA.position.x, pointB.position.x);
        float maxX = Mathf.Max(pointA.position.x, pointB.position.x);
        return player.position.x > minX && player.position.x < maxX;
    }

    void Flip(float targetX)
    {
        if ((targetX < transform.position.x && transform.localScale.x > 0) ||
            (targetX > transform.position.x && transform.localScale.x < 0))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isCollidingWithPlayer = true;
            if (healthBarUI != null)
                healthBarUI.SetActive(true);

            if (!isAttacking)
                StartCoroutine(AttackLoop());
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isCollidingWithPlayer = false;
            if (healthBarUI != null)
                healthBarUI.SetActive(false);
        }
    }

    public void FinishMeleeAttacking()
    {
        isAttacking = false;
        animator.SetBool("Attack", false);
    }
}
