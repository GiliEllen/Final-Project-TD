using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float hp;
    public bool isMovable;
    public bool isMoving;
    public bool canBulletBounce;
    public float shotDegree;
    public float shotAmount;
    public float manaCost;
    public float gridWidth;
    public float gridHeight;
    public bool isAlive = true;


    public void Shoot() {
        //TODO: create shooting logic per gun logic
    }

    public void TakeDamage(float howMuch) {
        hp -= howMuch;
        if (hp >= 0 ) {
            DestroyGun();
        }
    }

    public void DestroyGun() {
        isAlive = false;
        //TODO: add logic - remove from active playerGuns
        this.gameObject.SetActive(false);
    }

    public levelUp(float addedHp = 0, float addedShotAmount = 0) {
          //TODO: add logic - change this parameters in PlayerGuns
        if (addedHp > 0) {
            hp += addedHp;
        }

        if (addedShotAmount > 0) {
            shotAmount += addedShotAmount;
        }
    }
}
