using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightmare : MonoBehaviour
{
    public float hp;
    public bool isMoving = true;
    public float gridWidth;
    public float gridHeight;
    public bool isAlive = true;
    public float speed = 1;
    public GameObject plane;
    public float timeToInitialize;
    public BabyWall babyWall;
    public bool touchedWall = false;
    public float scareLevelAppear;
    public float scareLevelPassive;
    public float scareLevelReachWall;
    public float scareLevelDisappear;

    private void Update()
    {
        Move();
    }

    
    public void TakeDamage(float howMuch) {
        hp -= howMuch;
        if (hp <= 0 ) {
            DestroyNightmare();
        }
    }

    public void DestroyNightmare() {
        isAlive = false;
        //TODO: add logic - remove from active playerToys
        gameObject.SetActive(false);
        babyWall.AdjustNightMareCount(-1);
        babyWall.UpdateScareLevel(scareLevelDisappear);
    }

    public virtual void Move() 
    {
        if (isMoving)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }

}