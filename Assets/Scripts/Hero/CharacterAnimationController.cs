using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private CharacterActions controller;
    private Animator animator;
    private static readonly int JUMP_TRIGGER = Animator.StringToHash("jumpTrigger");
    private static readonly int JUMP_FALL_TRIGGER = Animator.StringToHash("jumpFallTrigger");
    private static readonly int LANDING_TRIGGER = Animator.StringToHash("landingTrigger");
    private static readonly int DASH_TRIGGER = Animator.StringToHash("dashTrigger");
    private static readonly int DIE_TRIGGER = Animator.StringToHash("dieTrigger");
    private static readonly int MOVEMENT_SPEED_ABS = Animator.StringToHash("movementSpeedAbs");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        CharacterActions.OnJumpEvent += () => animator.SetTrigger(JUMP_TRIGGER);
        CharacterActions.OnJumpFallEvent += () => animator.SetTrigger(JUMP_FALL_TRIGGER);
        CharacterActions.OnLandEvent += () => animator.SetTrigger(LANDING_TRIGGER);
        CharacterActions.OnDashEvent += () => animator.SetTrigger(DASH_TRIGGER);
        CharacterActions.OnDeathEvent += () => animator.SetTrigger(DIE_TRIGGER);
    }

    public void ToggleSword()
    {
        controller.ToggleSword();
    }

    public void GameOver()
    {
        animator.enabled = false;
    }

}
