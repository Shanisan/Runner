using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGameButton, exitButton;

    private void OnEnable()
    {
        startGameButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void StartGame()
    {
        GameManager.Instance.UIManager.CurrentScreen = UIScreen.IN_GAME;
        GameManager.Instance.isGameStarted = true;
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
