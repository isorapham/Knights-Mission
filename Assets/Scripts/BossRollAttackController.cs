using UnityEngine;

public class BossRollAttackController : MonoBehaviour
{
    public Collider2D rollAttackCollider; // collider gây damage khi Roll Attack
    public Rigidbody2D rb;
    public float rollImpulse = 10f; // lực đẩy khi lăn

    private void Awake()
    {
        if (rollAttackCollider != null)
            rollAttackCollider.enabled = false;

        rb = GetComponent<Rigidbody2D>();
    }

    // Gọi ở frame đầu animation RollAttack
    public void RollImpulse()
    {
        float direction = transform.localScale.x > 0 ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * rollImpulse, rb.linearVelocity.y);
    }

    // Bật collider (gọi bằng Animation Event ở frame bắt đầu gây damage)
    public void EnableRollAttack()
    {
        if (rollAttackCollider != null)
            rollAttackCollider.enabled = true;
    }

    // Tắt collider (gọi bằng Animation Event ở frame cuối)
    public void DisableRollAttack()
    {
        if (rollAttackCollider != null)
            rollAttackCollider.enabled = false;
    }
    private bool isRolling = false;

    public void StartRoll()
    {
        isRolling = true;
    }

    public void StopRoll()
    {
        isRolling = false;
        DisableRollAttack();
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // dừng
    }

    private void FixedUpdate()
    {
        if (isRolling)
        {
            float dir = transform.localScale.x > 0 ? 1f : -1f;
            rb.linearVelocity = new Vector2(dir * rollImpulse, rb.linearVelocity.y);
        }
    }
}