using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayButtons : Resettable
{
    [SerializeField] private Animator dashButtonAnim;
    [SerializeField] private Image dashButton;

    private float previousDashButtonState = 1;
    
    private void OnEnable()
    {
        dashButton.fillAmount = 1f;
        CharacterActions.DashRecoveryUpdate += UpdateDashButtonProgress;
    }
    private void OnDisable()
    {
        CharacterActions.DashRecoveryUpdate -= UpdateDashButtonProgress;
    }

    protected override void Reset()
    {
        dashButton.fillAmount = 1f;
    }

    private void UpdateDashButtonProgress(float currentFillAmount)
    {
        if (previousDashButtonState < 1 && currentFillAmount >= 1)
            dashButtonAnim.enabled = true;
        previousDashButtonState = dashButton.fillAmount;
        dashButton.fillAmount = currentFillAmount;
    }

    public void DisableAnimator()
    {
        dashButtonAnim.enabled = false;
    }
}
