using UnityEngine;

public class BackHitBox : MonoBehaviour
{
    private BossController2 boss;

    private void Awake()
    {
        boss = GetComponentInParent<BossController2>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (boss == null) return;

        if (collision.CompareTag("Player") || collision.CompareTag("Projectile"))
        {
            boss.FlipDirection();
            Debug.Log("Boss bị tấn công từ phía sau → quay lại!");
        }
    }
}