using UnityEngine;

public class BossHealthTrigger : MonoBehaviour
{
    public BossHealthUIController bossUIController; // để bật thanh máu

    [Header("Music Settings")]
    public AudioSource musicSource;   // AudioSource đang phát nhạc nền
    public AudioClip bossMusic;       // Nhạc nền khi gặp boss
    [Range(0f, 1f)]
    public float bossMusicVolume = 0.3f; // Volume khi boss music phát

    private bool triggered = false;   // tránh lặp lại nhiều lần

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            // 🔴 Bật thanh máu boss
            if (bossUIController != null && bossUIController.bossHealthBar != null)
            {
                bossUIController.bossHealthBar.Show();
            }

            // 🔴 Đổi nhạc nền + chỉnh volume
            if (musicSource != null && bossMusic != null)
            {
                musicSource.clip = bossMusic;
                musicSource.volume = bossMusicVolume; // chỉnh volume
                musicSource.loop = true;
                musicSource.Play();
            }

            triggered = true;
        }
    }
}
