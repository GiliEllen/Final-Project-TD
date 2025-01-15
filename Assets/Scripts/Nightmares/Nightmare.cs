using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightmare : MonoBehaviour
{
    public float hp;
    public bool isMoving;
    public float gridWidth;
    public float gridHeight;
    public bool isAlive = true;
    public GameObject plane;

    void Start() {
        PositionNightmare();
    }

    
    public void TakeDamage(float howMuch) {
        hp -= howMuch;
        if (hp >= 0 ) {
            DestroyNightmare();
        }
    }

    public void DestroyNightmare() {
        isAlive = false;
        //TODO: add logic - remove from active playerToys
        // this.gameObject.SetActive(false);
    }

    public void Move() {

    }

    public void PositionNightmare() {
        float length = PlaneSizeExtractor.getXSize(plane);
        if (length > 0) {
            float randomValue = Random.Range(0f, length);

            if (randomValue > 0) {
                this.transform.position = new Vector3(randomValue * 1f, 0.6f, 3.4f);
            } 
        } else {
            Debug.Log("no plane provided");
        }
    }

}