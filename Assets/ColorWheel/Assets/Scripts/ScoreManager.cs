using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public TextMeshProUGUI tempLevelText, nextLevelText;

    private Image levelBarImage;

    [HideInInspector]
    public int score = 0;

    void Start()
    {
        levelBarImage = GameObject.FindGameObjectWithTag("LevelBar").GetComponent<Image>();      
    }

    public void IncrementScore(int tempCount, int counter)
    {
        levelBarImage.fillAmount = 1 - (float)counter / (float)tempCount;           
        FindObjectOfType<AudioManager>().ScoreSound();       
    }

    public void ChangeLevelTexts()
    {
        score++;         
        tempLevelText.text = score.ToString();
        nextLevelText.text = (score + 1).ToString();
    }
}
