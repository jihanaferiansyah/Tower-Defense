using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPauseMenuController : MonoBehaviour
{
    private static UIPauseMenuController _instance = null;

    public static UIPauseMenuController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIPauseMenuController>();
            }

            return _instance;
        }
    }
    
    public GameObject pauseMenuPanel;
    public GameObject gameplayPanel;
    public Text statusInfo;
    public Button buttonResume;
    public Button buttonRestart;
    public Button buttonExit;

    private void Start()
    {
        buttonResume.onClick.AddListener(ResumeGame);
        buttonRestart.onClick.AddListener(RestartGame);
        buttonExit.onClick.AddListener(ExitToMenu);
        pauseMenuPanel.gameObject.SetActive(false);
    }

    public void EndGame(bool isWin)
    {
        buttonResume.gameObject.SetActive(false);
        statusInfo.text = isWin ? "YOU WIN!" : "YOU LOSE!";
        pauseMenuPanel.gameObject.SetActive(true);
        gameplayPanel.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuPanel.gameObject.SetActive(true);
        statusInfo.text = "GAME PAUSED";
        gameplayPanel.SetActive(false);
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuPanel.gameObject.SetActive(false);
        gameplayPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ExitToMenu()
    {
        Application.Quit();
    }
}
