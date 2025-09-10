using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{
    [Header("Hiển thị/Feedback khi được kích hoạt")]
    public SpriteRenderer flagSprite; // (tuỳ chọn) cờ/biểu tượng checkpoint để đổi màu
    public Color activeColor = Color.green;
    public Color inactiveColor = Color.white;

    private static Checkpoint currentActive; // checkpoint đang active (để tắt cái cũ)

    void Reset()
    {
        // Đảm bảo collider là trigger
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Tìm Damageable trên Player (để chắc chắn đúng Player)
        var damageable = other.GetComponent<Damageable>();
        if (damageable == null) return;

        // Lấy RespawnSystem trên cùng GameObject của Player
        var respawnSystem = damageable.GetComponent<RespawnSystem>();
        if (respawnSystem == null) return;

        // Cập nhật respawn point
        respawnSystem.SetRespawnPoint(transform);

        // Cập nhật feedback hiển thị
        ActivateVisuals();
    }

    private void ActivateVisuals()
    {
        // Tắt checkpoint cũ (nếu có)
        if (currentActive != null && currentActive != this)
            currentActive.SetColor(inactiveColor);

        currentActive = this;
        SetColor(activeColor);
    }

    private void SetColor(Color c)
    {
        if (flagSprite != null) flagSprite.color = c;
    }

    // Vẽ bán kính/gizmo nhỏ để dễ thấy trên Scene (tuỳ chọn)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
}