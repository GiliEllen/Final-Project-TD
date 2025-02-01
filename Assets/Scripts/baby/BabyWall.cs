using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyWall : MonoBehaviour
{
    public float nightmareCount = 0;
    public Baby baby;

    private void Start()
    {
        Nightmare.NightmareDestroyed += OnNightmareDestoryed;
        InvokeRepeating("UpdateScareLevel", 0f, 1f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Nightmare enemy = collision.gameObject.GetComponent<Nightmare>();
        if (enemy != null && enemy.touchedWall == false)
        {
            AdjustNightMareCount(1);
            baby.AdjustScare(enemy.scareLevelReachWall);
            enemy.touchedWall = true;
            enemy.isMoving = false;
        } 
    }

    public void AdjustNightMareCount(float amount) {
        nightmareCount += amount;
    }

    public void UpdateScareLevel() {
        if (nightmareCount <= 0) {
            nightmareCount = 0;
            return;
        }
        baby.AdjustScare(nightmareCount * 1);
    }

    private void OnNightmareDestoryed(float scareLevelToDecrease)
    {
        AdjustNightMareCount(-1);
        UpdateScareLevel(scareLevelToDecrease);
    }

    public void UpdateScareLevel(float amount) {
        baby.AdjustScare(amount);
    }
}
