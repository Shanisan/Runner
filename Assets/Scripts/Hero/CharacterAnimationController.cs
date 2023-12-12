using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : Resettable
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
        CharacterActions.MovementEvent += (x) => animator.SetFloat(MOVEMENT_SPEED_ABS, Math.Abs(x));
    }

    public void ToggleSword()
    {
        controller.ToggleSword();
    }

    public void GameOver()
    {
        animator.enabled = false;
    }

    protected override void Reset()
    {
        animator.enabled = true;
        animator.Play("Run");
        StartCoroutine(ResetAnimationTriggers());
    }

    private IEnumerator ResetAnimationTriggers()
    {
        yield return null;
        animator.ResetTrigger(JUMP_TRIGGER);
        animator.ResetTrigger(JUMP_FALL_TRIGGER);
        animator.ResetTrigger(LANDING_TRIGGER);
        animator.ResetTrigger(DASH_TRIGGER);
        animator.ResetTrigger(DIE_TRIGGER);
        animator.SetFloat(MOVEMENT_SPEED_ABS, 0);
    }
}
