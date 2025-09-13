using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    [SerializeField]
    private Slider playerHealthSlider;
    [SerializeField]
    private Slider bossHealthSlider;
    [SerializeField]
    private GameObject bossHealthGo;

    private int playerMaxHealth;

    public void SetMaxHealth(int maxHealthValue)
    {
        playerMaxHealth = maxHealthValue;
        playerHealthSlider.maxValue = maxHealthValue;
    }

    public void UpdateHealth(int currentHealthValue)
    {
        playerHealthSlider.value = currentHealthValue;
    }

    public void ResetHealth()
    {
        UpdateHealth(playerMaxHealth);
    }

    public void ActiveBossHealth(bool status)
    {
        bossHealthGo.SetActive(status);
    }

    public void SetBossMaxHealth(int maxHealthValue)
    {
        bossHealthSlider.maxValue = maxHealthValue;
    }

    public void UpdateBossHealth(int currentHealthValue)
    {
        bossHealthSlider.value = currentHealthValue;
    }
}
