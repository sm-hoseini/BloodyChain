using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadScreen : MonoBehaviour {

    public GameObject deadScreenPanel;

    public Text score;

    void OnEnable()
    {
        ActionManager.OnPlayerDie += OnPlayerDie;
    }

    void OnDisable()
    {
        ActionManager.OnPlayerDie -= OnPlayerDie;
    }

    private void OnPlayerDie()
    {
        StartCoroutine("SetDeathData");
    }

    IEnumerator SetDeathData()
    {
        yield return new WaitForSeconds(2);
        deadScreenPanel.SetActive(true);
        score.text = Statics.Score.ToString();
    }

    public void Restart()
    {
        UiAudioManager.GetInstance().PlayClickSound();
        ActionManager.OnGameRestart();
    }

    public void Exit()
    {
        UiAudioManager.GetInstance().PlayClickSound();
        SceneManager.LoadScene("MainMenu");
    }
}
