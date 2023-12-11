using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayButtons : MonoBehaviour
{
    [SerializeField] private Animator dashButtonAnim;
    [SerializeField] private Image dashButton;

    private float previousDashButtonState = 1;
    
    private void OnEnable()
    {
        CharacterActions.DashRecoveryUpdate += UpdateDashButtonProgress;
    }
    private void OnDisable()
    {
        CharacterActions.DashRecoveryUpdate -= UpdateDashButtonProgress;
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
