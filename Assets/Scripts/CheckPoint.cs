using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{

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

    }
}