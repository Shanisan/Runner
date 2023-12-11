using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }
    /*
    public AudioManager AudioManager { get; private set; }
    public UIManager UIManager { get; private set; }
    */
    public UIManager UIManager;
    public bool isGameStarted = false;
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
    }
}