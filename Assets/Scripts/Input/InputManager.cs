using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    public GameInputSystem input;
    public CharacterActions characterController;

    public static event Action OnJumpPressed, OnDashPressed, OnSlidePressed, OnSlideReleased;

    private void Awake()
    {
        Debug.Log("Awake!");
        input = new GameInputSystem();
        input.Gameplay.Jump.performed += _ => Jump_performed();
        input.Gameplay.Dash.performed += _ => Dash_performed();
        input.Gameplay.Slide.performed += _ => Slide_performed();
        input.Gameplay.Slide.canceled += _ => OnSlideReleased?.Invoke();
#if UNITY_EDITOR
        input.Gameplay.Restart.performed += _ => Restart_performed();
#endif
    }

    private void Slide_performed()
    {
        Debug.Log("Sliding");
        OnSlidePressed?.Invoke();
    }

    private void Restart_performed()
    {
        Debug.Log("Restarting");
        GameManager.Instance.RestartGame();
    }

    private void Jump_performed()
    {
        Debug.Log("Jumping");
        //characterController.Jump();
        OnJumpPressed?.Invoke();
    }

    private void Dash_performed()
    {
        Debug.Log("Dashing");
        //characterController.Dash();
        OnDashPressed?.Invoke();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

}
