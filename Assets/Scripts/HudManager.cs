using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HudManager : MonoBehaviour
{

    public Text scoreText;
    public GameObject GameOVer,GameEnd;
    public Image[] lifeImages;
    public Sprite lifeDark;
    public Sprite lifeLight;
    public Image[] keysImage;
    public Sprite activeKey;
    public Image environmentImage;
    public Sprite lightImage;
    public Sprite darkImage;

    private int life = 5;

    void OnEnable()
    {
        ActionManager.OnPlayerHit += OnBulletHitPlayer;
        ActionManager.OnEnvironmentChange += OnEnvironmentChange;
        ActionManager.OnGameRestart += OnGameRestart;
        ActionManager.OnGamePaused += OnGamePaused;
        ActionManager.OnGetKey += OnGetKey;
        ActionManager.OnScoreUpdated += OnScoreUpdated;
        ActionManager.OnPlayerDie += OnPlayerDie;
        ActionManager.OnEndGame += OnEndGame;
    }

    void OnDisable()
    {
        ActionManager.OnPlayerHit -= OnBulletHitPlayer;
        ActionManager.OnEnvironmentChange -= OnEnvironmentChange;
        ActionManager.OnGameRestart -= OnGameRestart;
        ActionManager.OnGamePaused -= OnGamePaused;
        ActionManager.OnGetKey -= OnGetKey;
        ActionManager.OnScoreUpdated -= OnScoreUpdated;
        ActionManager.OnPlayerDie -= OnPlayerDie;
        ActionManager.OnEndGame -= OnEndGame;
    }
    int keyindex = -1;
    void OnGetKey()
    {
        keyindex++;
        keysImage[keyindex].sprite = activeKey;
    }
    void OnPlayerDie()
    {
        Invoke("DisplayGameOVer",1);
    }
    void DisplayGameOVer()
    {
        GameOVer.SetActive(true);
    }
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            UiAudioManager.GetInstance().PlayClickSound();


            if (Time.timeScale == 1)
                ActionManager.OnGamePaused();
            else
                ActionManager.OnGameResumed();
        }

    }

    private void OnBulletHitPlayer()
    {
        life--;
        if (life <= 0)
        {
            SetLife(0);
        }
        else
        {
            SetLife(life);
        }
    }

    private void SetLife(int life)
    {
        switch (life)
        {
            case 5:
                lifeImages[0].gameObject.SetActive(true);
                lifeImages[1].gameObject.SetActive(true);
                lifeImages[2].gameObject.SetActive(true);
                lifeImages[3].gameObject.SetActive(true);
                lifeImages[4].gameObject.SetActive(true);
                break;
            case 4:
                lifeImages[0].gameObject.SetActive(true);
                lifeImages[1].gameObject.SetActive(true);
                lifeImages[2].gameObject.SetActive(true);
                lifeImages[3].gameObject.SetActive(true);
                lifeImages[4].gameObject.SetActive(false);
                break;
            case 3:
                lifeImages[0].gameObject.SetActive(true);
                lifeImages[1].gameObject.SetActive(true);
                lifeImages[2].gameObject.SetActive(true);
                lifeImages[3].gameObject.SetActive(false);
                lifeImages[4].gameObject.SetActive(false);
                break;
            case 2:
                lifeImages[0].gameObject.SetActive(true);
                lifeImages[1].gameObject.SetActive(true);
                lifeImages[2].gameObject.SetActive(false);
                lifeImages[3].gameObject.SetActive(false);
                lifeImages[4].gameObject.SetActive(false);
                break;
            case 1:
                lifeImages[0].gameObject.SetActive(true);
                lifeImages[1].gameObject.SetActive(false);
                lifeImages[2].gameObject.SetActive(false);
                lifeImages[3].gameObject.SetActive(false);
                lifeImages[4].gameObject.SetActive(false);
                break;
            case 0:
                lifeImages[0].gameObject.SetActive(false);
                lifeImages[1].gameObject.SetActive(false);
                lifeImages[2].gameObject.SetActive(false);
                lifeImages[3].gameObject.SetActive(false);
                lifeImages[4].gameObject.SetActive(false);
                break;
        }
    }

    private void OnEnvironmentChange()
    {
        if (Statics.EnvironmentNumber == 0)
        {
            environmentImage.sprite = lightImage;

            foreach (Image img in lifeImages)
            {
                img.sprite = lifeLight;
            }
        }
        else if (Statics.EnvironmentNumber == 1)
        {
            environmentImage.sprite = darkImage;

            foreach (Image img in lifeImages)
            {
                img.sprite = lifeDark;
            }
        }
    }

    private void OnGamePaused()
    {

    }

    private void OnGameRestart()
    {
        Statics.OnGameRestart();
        SceneManager.LoadScene("GamePlayPrototype");
    }

    public void Restart()
    {
       // UiAudioManager.GetInstance().PlayClickSound();
        ActionManager.OnGameRestart();
    }
    public void ExitToMainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void OnScoreUpdated()
    {
        scoreText.text = Statics.Score.ToString();
    }
    void OnEndGame()
    {
        GameEnd.SetActive(true);
    }
    public GameObject HintPage;
    public void HideHint()
    {
        HintPage.SetActive(false);
    }
}
