using UnityEngine;
using UnityEngine.UI;

public class UIHealthBarBoss : MonoBehaviour
{
    public Slider slider;
    public GameObject healthBarObject; // chính là BossHealthBarUI

    public void Initialize(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        healthBarObject.SetActive(false); // mặc định ẩn
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void Show()
    {
        healthBarObject.SetActive(true);
    }

    public void Hide()
    {
        healthBarObject.SetActive(false);
    }
}
