using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject aboutPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            if (aboutPanel.activeInHierarchy)
            {
                aboutPanel.SetActive(false);
            }
        }
    }

    public void OnBtnStartGame()
    {
        UiAudioManager.GetInstance().PlayClickSound();
        SceneManager.LoadScene("Toturial");
    }

    public void OnBtnExit()
    {
        UiAudioManager.GetInstance().PlayClickSound();
        Application.Quit();
    }

    public void AboutShow()
    {
        UiAudioManager.GetInstance().PlayClickSound();
        aboutPanel.SetActive(true);
    }

    public void AboutOff()
    {
        UiAudioManager.GetInstance().PlayClickSound();
        aboutPanel.SetActive(false);
    }
}
