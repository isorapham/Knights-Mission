using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UIHealthBar healthBar;
    public UIHealthBarBoss bossHealthBar;

    // Sự kiện khi bị trúng đòn, truyền vào lượng damage (int) và lực đẩy knockback (Vector2)
    public UnityEvent<int, Vector2> damageableHit;

    Animator animator; // Tham chiếu tới Animator để điều khiển animation

    [SerializeField]
    private int _maxHealth = 100; // Máu tối đa (có thể chỉnh trong Inspector)

    // Thuộc tính MaxHealth (public) để đọc/ghi _maxHealth
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField]
    private int _health = 100; // Máu hiện tại

    // Thuộc tính Health để đọc/ghi _health
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;           // Gán giá trị mới
            if (_health <= 0)          // Nếu máu ≤ 0 thì nhân vật chết
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true; // Trạng thái còn sống hay không
    [SerializeField]
    private bool isInvincible = false; // Trạng thái bất tử tạm thời sau khi dính đòn
    [SerializeField]
    private float timeSinceHit = 0; // Thời gian đã trôi qua từ lúc bị đánh
    [SerializeField]
    private float invincibilityTimer = 0.25f; // Thời gian bất tử (0.25 giây)

    // Thuộc tính IsAlive để đọc/ghi _isAlive
    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value; // Cập nhật trạng thái sống
            animator.SetBool(AnimationStrings.isAlive, value); // Gửi giá trị tới Animator
            Debug.Log("IsAlive set " + value); // In ra console để debug
        }
    }

    // Thuộc tính LockVelocity để điều khiển nhân vật có bị khóa di chuyển không
    public bool LockVelocity
    {
        get { return animator.GetBool(AnimationStrings.lockVelocity); }
        set { animator.SetBool(AnimationStrings.lockVelocity, value); }
    }

    private void Awake()
    {
        // Lấy Animator gắn trên object để điều khiển animation
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(MaxHealth);
        }
        if (bossHealthBar != null)
        {
            bossHealthBar.Initialize(MaxHealth);
        }
    }
    private void Update()
    {
        // Nếu đang bất tử sau khi bị đánh
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTimer)
            {
                // Hết thời gian bất tử → cho phép bị đánh lại
                isInvincible = false;
                timeSinceHit = 0;
            }
            // Tăng bộ đếm thời gian
            timeSinceHit += Time.deltaTime;
        }
    }

    // Hàm xử lý khi bị đánh
    public bool Hit(int damage, Vector2 knockback)
    {
        // Nếu còn sống và không trong trạng thái bất tử
        if (IsAlive && !isInvincible)
        {
            Health -= damage;              // Trừ máu
            isInvincible = true;           // Kích hoạt bất tử tạm thời

            animator.SetTrigger(AnimationStrings.hitTrigger); // Gửi trigger "hit" cho Animator
            LockVelocity = true;           // Khóa chuyển động khi bị đánh
            damageableHit?.Invoke(damage, knockback); // Gọi event (nếu có hàm đăng ký)

            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            // → Bắn sự kiện toàn cục characterDamaged 
            // → Truyền vào: gameObject (nhân vật bị đánh) và damage (lượng sát thương)
            // → Giúp các script khác (ví dụ UI máu, popup sát thương, hệ thống log...) có thể lắng nghe
            if (healthBar != null)
            {
                healthBar.SetHealth(Health);
            }

            return true;                   // Trả về true = đã nhận damage
        }
        return false; // Trả về false nếu không nhận damage (đang bất tử hoặc đã chết)
    }

    public bool Heal(int healthRestores)
    {
        if (IsAlive&& Health<MaxHealth) // Chỉ hồi máu nếu nhân vật còn sống
        {
            // maxHeal = lượng máu có thể hồi tối đa (không vượt quá MaxHealth)
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);

            // actualHeal = lượng máu thực tế được hồi (giới hạn bởi healthRestores và maxHeal)
            int actualHeal = Mathf.Min(maxHeal, healthRestores);

            // Tăng máu hiện tại thêm actualHeal
            Health += actualHeal;

            // 🔥 Bắn sự kiện toàn cục characterHealed
            // → Truyền vào: gameObject (nhân vật được hồi máu) và actualHeal (lượng máu hồi)
            CharacterEvents.characterHealed(gameObject, actualHeal);

            if (healthBar != null)
            {
                healthBar.SetHealth(Health);
            }
            return true;
        }
        return false ;
    }
    public void Respawn()
    {
        // Đặt lại trạng thái sống
        IsAlive = true;

        // Mở khoá di chuyển
        LockVelocity = false;

        // Reset máu đầy
        Health = MaxHealth;  // setter sẽ tự cập nhật UI HealthBar

        // Reset animation nếu cần
        if (animator != null)
        {
            animator.Play("Idle"); // hoặc state Idle/Respawn tuỳ animator của bạn
        }

        Debug.Log($"{gameObject.name} đã respawn, máu đầy {Health}/{MaxHealth}");
    }
}