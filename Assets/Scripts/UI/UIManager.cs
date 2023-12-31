using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UIScreenElement> uiScreens;
    public UIScreen CurrentScreen
    {
        get => _currentScreen;
        set
        {
            foreach (var screen in uiScreens)
            {
                screen.screenObject.SetActive(screen.screenType==value);
            }

            _currentScreen = value;
        }
    }
    private UIScreen _currentScreen;

    private void Awake()
    {
        CurrentScreen = UIScreen.MAIN_MENU;
        CharacterActions.OnDeathEvent += () =>
        {
            Debug.Log("Player died, switching to game over screen");
            CurrentScreen = UIScreen.GAME_OVER;
        };
    }
}

[Serializable]
public class UIScreenElement
{
    public UIScreen screenType;
    public GameObject screenObject;
}

public enum UIScreen
{
    [InspectorName("Main Menu")]        MAIN_MENU, 
    [InspectorName("In-Game Menu")]     IN_GAME,
    [InspectorName("Game Over Screen")]     GAME_OVER
}

