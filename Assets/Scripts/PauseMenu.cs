using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pausePanel;

    void OnEnable()
    {
        ActionManager.OnGamePaused += OnGamePaused;
        ActionManager.OnGameResumed += OnGameResumed;
    }

    void OnDisable()
    {
        ActionManager.OnGamePaused -= OnGamePaused;
        ActionManager.OnGameResumed -= OnGameResumed;
    }

    private void OnGamePaused()
    {
        pausePanel.SetActive(true);

        Time.timeScale = 0;
    }

    public void Resume()
    {
        UiAudioManager.GetInstance().PlayClickSound();
        ActionManager.OnGameResumed();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        ActionManager.OnGameRestart();
    }

    public void Exit()
    {
        UiAudioManager.GetInstance().PlayClickSound();

        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    private void OnGameResumed()
    {
        Time.timeScale = 1;

        pausePanel.SetActive(false);
    }
}
