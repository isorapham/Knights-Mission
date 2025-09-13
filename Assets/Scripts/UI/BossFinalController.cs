using UnityEngine;

public class BossFinalController : MonoBehaviour
{
    public Damageable damageable;             // kéo Damageable của Boss
    public MainMenuController mainMenu;       // kéo Canvas (có MainMenuController)

    private void Update()
    {
        if (damageable != null && !damageable.IsAlive)
        {
            if (mainMenu != null)
            {
                mainMenu.ShowWinPanel(); // Hiện WinPanel
            }
            enabled = false; // tránh gọi liên tục
        }
    }
}
