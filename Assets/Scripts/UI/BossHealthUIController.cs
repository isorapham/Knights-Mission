using UnityEngine;

public class BossHealthUIController : MonoBehaviour
{
    public Damageable bossDamageable;    // Damageable của Boss
    public UIHealthBarBoss bossHealthBar; // Thanh máu UI

    private void Start()
    {
        if (bossHealthBar != null && bossDamageable != null)
        {
            bossHealthBar.Initialize(bossDamageable.MaxHealth);
        }
    }

    private void Update()
    {
        if (bossHealthBar != null && bossDamageable != null)
        {
            bossHealthBar.SetHealth(bossDamageable.Health);

            if (!bossDamageable.IsAlive)
            {
                bossHealthBar.Hide();
            }
        }
    }
}
