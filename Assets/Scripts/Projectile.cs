using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockback = new Vector2(0, 0);
    public int damage = 10;
    public float lifeTime = 5f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
        Destroy(gameObject,lifeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            // Tính toán hướng knockback:      
            Vector2 deliveredKnockback = transform.localScale.x > 0 ?
                                         knockback :
                                         new Vector2(-knockback.x, knockback.y);

            // Gọi hàm Hit() trên target, truyền vào sát thương và knockback
            bool gotHit = damageable.Hit(damage, deliveredKnockback);

            if (gotHit) // Nếu đối thủ thực sự nhận damage
            {
                
                Debug.Log(collision.name + " hit for " + damage);
                // In ra console tên object và lượng damage nhận

                Destroy(gameObject);
            }
        }
    }
}
