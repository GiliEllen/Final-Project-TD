using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlacementButton : MonoBehaviour
{
    public float coolDown; 
    private float coolDownTimer; 
    private bool isOnCooldown; 

    private Button button; 

    private void Start()
    {
        button = GetComponent<Button>();

        if (button == null)
        {
            Debug.LogError("No Button component found on this GameObject.");
        }
    }

    private void Update()
    {
        if (isOnCooldown)
        {
            coolDownTimer -= Time.deltaTime;

            if (coolDownTimer <= 0f)
            {
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

        isOnCooldown = false;
    }
}
