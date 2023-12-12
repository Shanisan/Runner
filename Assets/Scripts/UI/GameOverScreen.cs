using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button restartButton, mainMenuButton, exitButton;
    [SerializeField] private TextMeshProUGUI gameOverText;
    private void OnEnable()
    {
        restartButton.onClick.AddListener(OnRestartClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuClicked);
        exitButton.onClick.AddListener(OnExitClicked);
        gameOverText.text = "Game Over";
        GetComponent<Animator>().enabled = true;
    }
    private void OnDisable()
    {
        restartButton.onClick.RemoveListener(OnRestartClicked);
        mainMenuButton.onClick.RemoveListener(OnMainMenuClicked);
        exitButton.onClick.RemoveListener(OnExitClicked);
    }

    private void OnRestartClicked()
    {
        GameManager.Instance.RestartGame();
        GameManager.Instance.UIManager.CurrentScreen = UIScreen.IN_GAME;
    }

    private void OnMainMenuClicked()
    {
        GameManager.Instance.RestartGame();
        GameManager.Instance.isGameStarted = false;
        GameManager.Instance.UIManager.CurrentScreen = UIScreen.MAIN_MENU;
    }

    private void OnExitClicked()
    {
        Application.Quit();
    }

    public void DisableAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }
}
