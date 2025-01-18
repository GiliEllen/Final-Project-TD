using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyWall : MonoBehaviour
{
    public float nightmareCount = 0;
    public Baby baby;

    private void Start()
    {
        InvokeRepeating("UpdateScareLevel", 0f, 1f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Nightmare enemy = collision.gameObject.GetComponent<Nightmare>();
        if (enemy != null && enemy.touchedWall == false)
        {
            AdjustNightMareCount(1);
            enemy.touchedWall = true;
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
        baby.AdjustScare(nightmareCount * 3);
    }
}
