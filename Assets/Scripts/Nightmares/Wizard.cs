using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Nightmare
{
    private float timer = 0f;
    public float attackChargeTime = 8f;
    public WizardCharge wizardCharge;
    public float minDamage = 5f;
    public float maxDamage = 10f;


    public Wizard()
    {
        hp = 15;
        isMoving = true;
        gridWidth = 1;
        gridHeight = 1;
        speed = 0.8f;
        scareLevelAppear = 10;
        scareLevelPassive = 0;
        scareLevelReachWall = 60;
        scareLevelDisappear = -15;
        isInvisible = false;

    }

    public void StartCharging() {
        // ACTIVATE ART
        ActivateChargingArt();
    }

    public void ActivateChargingArt() {
        wizardCharge.gameObject.SetActive(true);
        wizardCharge.ToggleAnimation(true);
    }
    public void DeactivateChargingArt() {
        wizardCharge.ToggleAnimation(false);
        wizardCharge.gameObject.SetActive(false);
    }
    public void Attack()
    {
        Toy[] allToys = FindObjectsOfType<Toy>(); 

        foreach (Toy toy in allToys)
        {
            float distance = Vector3.Distance(transform.position, toy.transform.position);

            float damage = Mathf.Clamp(10f / distance, minDamage, maxDamage); 
            
            damage = Mathf.Round(damage);
           GameObject lightning = Instantiate(
                Resources.Load("Lightning"), 
                new Vector3(toy.transform.position.x, 6f, toy.transform.position.z), 
                Quaternion.Euler(0, 0, 90)
             ) as GameObject;

            // GameObject darkSmoke = Instantiate(Resources.Load("DarkSmoke"), transform.position, Quaternion.identity) as GameObject;
            toy.TakeDamage(damage);
        }

        DestroyNightmare();
    }


    private void Start()
    {
        // activatePortal = false;
        StartCharging();
    }

      private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= attackChargeTime) {
            Attack();
            timer = 0;
        }
    }
}