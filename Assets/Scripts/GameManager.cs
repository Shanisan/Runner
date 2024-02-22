using System;
using UnityEngine;

[DefaultExecutionOrder(-5)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    /*
    public AudioManager AudioManager { get; private set; }
    public UIManager UIManager { get; private set; }
    */
    public UIManager UIManager;
    public bool isGameStarted = false;

    public GameObject Player { get; private set; }
    
    public event Action RestartGameEvent;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        /*
         AudioManager = GetComponentInChildren<AudioManager>();
        UIManager = GetComponentInChildren<UIManager>();
        */
        Player = GameObject.FindWithTag("Player");
    }


    public void RestartGame()
    {
        RestartGameEvent?.Invoke();
    }
}