using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject upgradePanel;

    private void Start()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        upgradePanel.SetActive(false);
    }

    // PausePanel
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MainMenu");
    }

    // Settings Panel
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    // Upgrades Panel
    public void UpgradeOpen()
    {
        upgradePanel.SetActive(true);
    }
    public void UpgradeClose()
    {
        upgradePanel.SetActive(false);
    }
}
