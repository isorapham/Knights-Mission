using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Damageable))]
public class BossController : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float chaseRange = 8f;
    public float attackRange = 3f;

    private Rigidbody2D rb;
    private Animator animator;
    private Damageable damageable;

    private bool isAttacking = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        if (!damageable.IsAlive) return;
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (!isAttacking)
        {
            if (distance <= attackRange)
            {
                ChooseAttack();
            }
            else if (distance <= chaseRange)
            {
                ChasePlayer();
            }
            else
            {
                Idle();
            }
        }

        // Xoay mặt về phía player
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void Idle()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetBool(AnimationStrings.isMoving, false);
    }

    void ChasePlayer()
    {
        animator.SetBool(AnimationStrings.isMoving, true);
        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(dir.x * moveSpeed, rb.linearVelocity.y);
    }

    void ChooseAttack()
    {
        // Phase dựa trên máu
        int phase = damageable.Health <= damageable.MaxHealth / 2 ? 2 : 1;
        animator.SetInteger("phase", phase);

        int attackType;
        if (phase == 1)
            attackType = Random.Range(1, 3); // Spike hoặc Roll
        else
            attackType = Random.Range(1, 4); // Spike, Roll, thêm Roar

        animator.SetInteger("attackType", attackType);
        animator.SetTrigger(AnimationStrings.attackTrigger);

        rb.linearVelocity = Vector2.zero;
        isAttacking = true;
    }

    // Gọi từ Animation Event khi attack kết thúc
    public void EndAttack()
    {
        isAttacking = false;
        animator.SetInteger("attackType", 0);
    }
}
