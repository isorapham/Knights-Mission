using UnityEngine;

[RequireComponent(typeof(BossController2))]
public class BossChaseController : MonoBehaviour
{
    [Header("Chase Settings")]
    public float chaseSpeed = 4f;       // tốc độ dí theo player
    public float attackRange = 1.5f;    // khoảng cách để ra đòn
    public float chaseRange = 6f;       // khoảng cách Boss bắt đầu chase

    private BossController2 boss;
    private Rigidbody2D rb;
    private Transform target;

    private void Awake()
    {
        boss = GetComponent<BossController2>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Boss chỉ chase khi có target
        if (boss.attackZone != null && boss.attackZone.detectedColliders.Count > 0)
        {
            target = boss.attackZone.detectedColliders[0].transform;
        }
        else
        {
            target = null;
        }

        if (target != null)
        {
            float distance = Vector2.Distance(transform.position, target.position);
            float dir = Mathf.Sign(target.position.x - transform.position.x);

            // quay mặt theo hướng player
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * dir, transform.localScale.y, transform.localScale.z);

            if (distance < chaseRange && distance > attackRange)
            {
                // chạy theo player
                rb.linearVelocity = new Vector2(chaseSpeed * dir, rb.linearVelocity.y);
            }
            else if (distance <= attackRange)
            {
                // trong tầm đánh
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

                // gọi RollAttack từ BossController2 nếu hết cooldown
                boss.RollAttack();
            }
        }
    }
}