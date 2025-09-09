using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private int attackDamage = 10;     // Sát thương gây ra (chỉnh được trong Inspector)
    public Vector2 knockback = Vector2.zero; // Lực hất lùi khi tấn công

    private void Awake()
    {
        // Hiện tại chưa cần xử lý gì khi khởi tạo
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem object va chạm có component Damageable không
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null) // Nếu có thể nhận damage
        {
            // Lấy hướng scale X của chính object này
            float parentScaleX = transform.localScale.x;

            // Nếu có parent thì lấy hướng scale X từ parent (dùng cho nhân vật có child attack collider)
            if (transform.parent != null)
            {
                parentScaleX = transform.parent.localScale.x;
            }

            // Tính toán hướng knockback:
            // Nếu scaleX > 0 → hướng phải, dùng knockback gốc
            // Nếu scaleX < 0 → hướng trái, đảo chiều knockback.X
            Vector2 deliveredKnockback = parentScaleX > 0 ?
                                         knockback :
                                         new Vector2(-knockback.x, knockback.y);

            // Gọi hàm Hit() trên target, truyền vào sát thương và knockback
            bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);

            if (gotHit) // Nếu đối thủ thực sự nhận damage
            {
                Debug.Log(collision.name + " hit for " + attackDamage);
                // In ra console tên object và lượng damage nhận
            }
        }
    }
}