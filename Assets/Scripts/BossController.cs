using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate = 0.05f;
    public DetectionZone cliffDetectionZone;
    public DetectionZone attackZone;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;

    [SerializeField] private float rollSpeed = 12f;   // tốc độ lăn
    [SerializeField] private float rollCooldown = 5f; // thời gian hồi roll
    [SerializeField] private float anticipationTime = 0.5f; // thời gian chờ trước khi roll
    [SerializeField] private float recoilTime = 0.5f;       // thời gian chờ sau khi roll

    private bool isRolling = false;

    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                if (value == WalkableDirection.Right)
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                else
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {

                    walkDirectionVector = Vector2.left;

                }
                _walkDirection = value;
            }
        }
    }
    public bool _hasTarget = false;
    public bool HasTarget
    {
        get
        { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCoolDown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }


    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

        if (AttackCoolDown > 0)
        {
            AttackCoolDown -= Time.deltaTime;
        }

        // Nếu có target, cooldown xong và đang không roll → bắt đầu roll
        if (HasTarget && AttackCoolDown <= 0 && !isRolling && touchingDirections.IsGrounded)
        {
            StartCoroutine(RollAttackRoutine());
        }
    }

    private IEnumerator RollAttackRoutine()
    {
        isRolling = true;
        animator.SetTrigger(AnimationStrings.rollAttackTrigger);

        // Anticipation (đợi animation chuẩn bị xong)
        yield return new WaitForSeconds(anticipationTime);

        // RollAttack
        float direction = walkDirectionVector.x; // hướng hiện tại
        rb.linearVelocity = new Vector2(direction * rollSpeed, rb.linearVelocityY);

        // Bật hitbox qua Animation Event (EnableAttack) trong clip RollAttack
        // giữ vận tốc trong lúc roll
        yield return new WaitForSeconds(0.6f); // thời lượng clip RollAttack

        // RollAttackRecoil
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(recoilTime);

        // Reset
        AttackCoolDown = rollCooldown;
        isRolling = false;
    }

    void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if (!damageable.LockVelocity)
        {
            if (CanMove)
            {
                rb.linearVelocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.linearVelocityY);
            }
            else
            {
                rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, walkStopRate), rb.linearVelocityY);
            }
        }

    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Loi roi");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocityY + knockback.y);
    }

    public void OnCliffDectection()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}