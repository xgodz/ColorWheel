using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public GameObject InfoPanel,ExitPanel;

   
    void Start()
    {
        InfoPanel.SetActive(false);
        ExitPanel.SetActive(false);
    }

    public void OpenInfo()
    {
        InfoPanel.SetActive(true);
    }

    public void CloseInfo()
    {
        InfoPanel.SetActive(false);
    }

    public void QuitGame()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            ExitPanel.SetActive(true);
         //   Application.Quit();
        }
    }
}
