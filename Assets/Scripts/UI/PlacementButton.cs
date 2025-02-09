using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlacementButton : MonoBehaviour
{
    public float coolDown; 
    private float coolDownTimer; 
    private bool isOnCooldown; 
    public TMP_Text CoolDownText;
    private Button button; 

    private void Start()
    {
        button = GetComponent<Button>();
        InitActiveStatusListeners();


        if (button == null)
        {
            Debug.LogError("No Button component found on this GameObject.");
        }
    }

    private void InitActiveStatusListeners()
    {
        NewLevelManager levelManager = GetComponentInParent<NewLevelManager>();
        levelManager.LevelCompleted += DisableButton;
        levelManager.LevelLost += DisableButton;
        levelManager.LevelStarted += EnableButton;
    }

    private void Update()
    {
        if (isOnCooldown)
        {
            coolDownTimer -= Time.deltaTime;
            CoolDownText.text = Mathf.CeilToInt(coolDownTimer).ToString();

            if (coolDownTimer <= 0f)
            { 
                CoolDownText.text = "";
                EnableButton();
            }
        }
    }

    public void StartCooldown()
    {
        if (button != null && !isOnCooldown)
        {
            DisableButton();
            coolDownTimer = coolDown;
            isOnCooldown = true;
        }
    }

    private void DisableButton()
    {
        if (button != null)
        {
            button.interactable = false;

            EventTrigger trigger = GetComponent<EventTrigger>();
            if (trigger != null)
            {
                trigger.enabled = false;
            }
        }
    }

    private void EnableButton()
    {
        if (button != null)
        {
            button.interactable = true;

            EventTrigger trigger = GetComponent<EventTrigger>();
            if (trigger != null)
            {
                trigger.enabled = true;
            }
        }
        CoolDownText.text = "";
        isOnCooldown = false;
    }
}
