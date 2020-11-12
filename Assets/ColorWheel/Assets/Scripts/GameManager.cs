using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject startPanel, endPanel, pausedPanel, pauseButton, muteImage, reviveButton, pointer, clickButton, levelBar;
    public TextMeshProUGUI highScoreText, endScoreText, endHighScoreText;

    
    public bool gameIsOver = false;

	void Start () {
        CallAdmobAds();       

        StartPanelActivation();
        HighScoreCheck();
        AudioCheck();
	}

    public void CallAdmobAds()
    {
        FindObjectOfType<AdManager>().ShowAdmobBanner();             
        if (Time.time != Time.timeSinceLevelLoad)
            FindObjectOfType<AdManager>().ShowAdmobInterstitial();                
    }

    public void Initialize()
    {
        pauseButton.SetActive(false);
        pointer.SetActive(false);
        clickButton.SetActive(false);
        levelBar.SetActive(false);
    }

    public void StartPanelActivation()
    {
        Initialize();
        startPanel.SetActive(true);
        endPanel.SetActive(false);
        pausedPanel.SetActive(false);
    }

    public void EndPanelActivation()
    {
        gameIsOver = true;
        FindObjectOfType<AudioManager>().DeathSound();
        startPanel.SetActive(false);
        endPanel.SetActive(true);
        pausedPanel.SetActive(false);
        endScoreText.text = FindObjectOfType<ScoreManager>().score.ToString();
        pauseButton.SetActive(false);
        HighScoreCheck();
        clickButton.SetActive(true);
        FindObjectOfType<PointerRotation>().canRotate = false;
        levelBar.SetActive(false);
    }

    public void PausedPanelActivation()
    {
        startPanel.SetActive(false);
        endPanel.SetActive(false);
        pausedPanel.SetActive(true);
    }

    public void HighScoreCheck()
    {
        if (FindObjectOfType<ScoreManager>().score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", FindObjectOfType<ScoreManager>().score);
        }
        highScoreText.text = "BEST " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        endHighScoreText.text = "BEST " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
      
    public void AudioCheck() 
    {
        if (PlayerPrefs.GetInt("Audio", 0) == 0)
        {
            muteImage.SetActive(false);
            FindObjectOfType<AudioManager>().soundIsOn = true;
            FindObjectOfType<AudioManager>().PlayBackgroundMusic();
        }
        else
        {
            muteImage.SetActive(true);
            FindObjectOfType<AudioManager>().soundIsOn = false;
            FindObjectOfType<AudioManager>().StopBackgroundMusic();
        }
    }

    public void StartButton()
    {
        pauseButton.SetActive(true);
        startPanel.SetActive(false);
        FindObjectOfType<AudioManager>().ButtonClickSound();
        pointer.SetActive(true);
        clickButton.SetActive(true);
        levelBar.SetActive(true);
    }

    public void RestartButton()
    {
        FindObjectOfType<AudioManager>().ButtonClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AudioButton()
    {
        FindObjectOfType<AudioManager>().ButtonClickSound();
        if (PlayerPrefs.GetInt("Audio", 0) == 0)
            PlayerPrefs.SetInt("Audio", 1);
        else
            PlayerPrefs.SetInt("Audio", 0);
        AudioCheck();
    }

    public void PauseButton()
    {
        pauseButton.SetActive(false);
        PausedPanelActivation();
        FindObjectOfType<AudioManager>().StopBackgroundMusic();
        Time.timeScale = 0f;
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().PlayBackgroundMusic();
        pauseButton.SetActive(true);
        pausedPanel.SetActive(false);
    }

    public void HomeButton()
    {
        ResumeButton();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Revive()
    {
        FindObjectOfType<AdManager>().ShowAdmobRewardVideo();           

        endPanel.SetActive(false);
        reviveButton.SetActive(false);
        pauseButton.SetActive(true);
        clickButton.SetActive(true);
        FindObjectOfType<PointerRotation>().canRotate = true;
        levelBar.SetActive(true);

        gameIsOver = false;
    }
}
