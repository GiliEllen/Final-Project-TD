using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Gun
{
    void Awake() {
        hp = 3;
        isMovable = false;
        isMoving = false;
        canBulletBounce = false;
        shotDegree = 0;
        shotAmount = 20;
        manaCost = 2;
        gridWidth = 2;
        gridHeight = 2;
    }
}