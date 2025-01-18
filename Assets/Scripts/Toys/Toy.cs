using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
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
    public float timeActive;
    public float elapsedTime = 0f; 
    public string type;



    public void TakeDamage(float howMuch) {
        hp -= howMuch;
        if (hp >= 0 ) {
            DestroyToy();
        }
    }

    public void DestroyToy() {
        isAlive = false;
        //TODO: add logic - remove from active playerToys
        this.gameObject.SetActive(false);
    }

    public void levelUp(float addedHp = 0, float addedShotAmount = 0) {
          //TODO: add logic - change this parameters in PlayerToys
        if (addedHp > 0) {
            hp += addedHp;
        }

        if (addedShotAmount > 0) {
            shotAmount += addedShotAmount;
        }
    }

    public void ActivatedTimeIsUp() {
        if (elapsedTime >= timeActive) {
            DestroyToy();
        }
    }
}
