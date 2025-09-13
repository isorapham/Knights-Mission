using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject menuPanel;   // Panel Menu
    public GameObject gamePanel;   // Panel HUD trong game
    public GameObject winPanel;    // Panel Win Game

    private void Start()
    {
        // Khi mở game: bật menu, tắt HUD & Win
        if (menuPanel != null) menuPanel.SetActive(true);
        if (gamePanel != null) gamePanel.SetActive(false);
        if (winPanel != null) winPanel.SetActive(false);

        Time.timeScale = 0f; // Dừng game khi menu bật
    }

    // Gọi khi bấm nút Start
    public void StartGame(string sceneName = "")
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName); // Load sang scene khác
        }
        else
        {
            if (menuPanel != null) menuPanel.SetActive(false);
            if (gamePanel != null) gamePanel.SetActive(true);
            if (winPanel != null) winPanel.SetActive(false);

            Time.timeScale = 1f; // Chạy game
        }
    }

    // Hiện WinPanel (gọi khi Boss chết)
    public void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            if (menuPanel != null) menuPanel.SetActive(false);
            if (gamePanel != null) gamePanel.SetActive(false);

            Time.timeScale = 0f; // Dừng game khi thắng
        }
    }

    // Restart game
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Quit game
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
