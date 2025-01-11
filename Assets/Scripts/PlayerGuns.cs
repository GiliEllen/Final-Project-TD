using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuns : MonoBehaviour
{
    // player guns list, contains their level and info
    // general list for guns, NOT active guns
    //[ 
    //  {
    //      gunName: 1
    //      gunType: gun type
    //      gunLevel: 1
    //      gunHpAdded: 1
    //      gunShotAmountAdded: 1
    //      isActiveForPlayer: true,
    //      gunPosition: vector 3 ? any other positioning options
    //  }
    //]

    public List<PlayerGun> Guns { get; private set; }

    public PlayerGuns()
    {
        Guns = new List<PlayerGun>();
    }

    void Awake() {
        // PlayerGuns();
        // CreateNewPlayerGunList();
    }

    public void AddGun(PlayerGun gun)
    {
        Guns.Add(gun);
    }

    public void RemoveGun(PlayerGun gun)
    {
        Guns.Remove(gun);
    }

    // public void CreateNewPlayerGunList() {
        //TODO: go through all types on initial load
        //laser - TODO: change type
        // PlayerGun newLaser = PlayerGun.CreateNewPlayerGun(Laser);
        // AddGun(newLaser);
    // }

}