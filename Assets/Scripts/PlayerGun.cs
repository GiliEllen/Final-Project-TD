using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{

    public Gun GunTemplate { get; private set; }
    public float gunLevel;
    public float gunHpAdded;
    public float gunShotAmountAdded;
    public bool isActiveForPlayer;

    //public Vector3 gunPosition: vector 3 ? any other positioning options

    //public void CreateGun(float id, string gunType, float newLevel = 1, float newHpAdded = 0, float newShotAmountAdded = 0 )

    public void LevelUpGun(float newHpAdded = 0, float newShotAmountAdded = 0 ) {
        gunLevel += 1;
        gunHpAdded += newHpAdded;
        gunShotAmountAdded += newShotAmountAdded;
    }

    public void CreateNewPlayerGun(Gun gunTemplate)
    {
        GunTemplate = gunTemplate;
        Level = 1;
        gunHpAdded = 0;
        gunShotAmountAdded = 0;
        isActiveForPlayer = true;
    }

}