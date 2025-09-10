using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public Damageable damageable;     // Kéo thả Damageable của Player vào
    public Transform respawnPoint;    // Điểm respawn hiện hành
    public int maxRespawns = 3;

    private int respawnCount = 0;

    void Awake()
    {
        if (damageable == null) damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        // Khi chết → respawn nếu còn lượt, hết lượt → game over
        if (!damageable.IsAlive)
        {
            if (respawnCount < maxRespawns)
            {
                Respawn();
            }
            else
            {
                Debug.Log("GAME OVER");
                enabled = false;
            }
        }
    }

    public void SetRespawnPoint(Transform newPoint)
    {
        respawnPoint = newPoint;
        Debug.Log($"Đã cập nhật Checkpoint: {newPoint.name}");
    }

    private void Respawn()
    {
        respawnCount++;

        if (respawnPoint != null)
        {
            damageable.transform.position = respawnPoint.position;
        }
        else
        {
            Debug.LogWarning("Chưa có respawnPoint! Hãy đặt một checkpoint mặc định trong Scene.");
        }

        damageable.Health = damageable.MaxHealth; // Hồi đầy máu
        damageable.IsAlive = true;                // Sống lại

        Debug.Log($"Respawn lần {respawnCount}/{maxRespawns}");
    }
}