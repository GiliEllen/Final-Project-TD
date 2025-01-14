using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Toy
{
    hp = 5;
    isMovable = true;
    isMoving = false;
    canBulletBounce = true;
    shotDegree = 0;
    shotAmount = 0;
    manaCost = 3;
    gridWidth = 2;
    gridHeight = 2;

    // TODO: finish
    public void StartMove() {
        //TODO: calculate trajectory
        isMoving = true;
        MoveBall();
    }

    public void MoveBall() {

    }
}
