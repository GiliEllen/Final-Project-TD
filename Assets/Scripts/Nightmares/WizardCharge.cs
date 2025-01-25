using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardCharge : MonoBehaviour 
{
    public Animator animator;

    void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleAnimation(bool status) {
        animator.SetBool("isCharging", status);
    }
    
} 
