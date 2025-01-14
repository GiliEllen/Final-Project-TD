using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Toy
{
    // Constructor (if used without Unity MonoBehaviour lifecycle)
    public Ball()
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
    }

    // // Use Unity's Start method for initialization
    // void Start()
    // {
    //     hp = 5;
    //     isMovable = true;
    //     isMoving = false;
    //     canBulletBounce = true;
    //     shotDegree = 0;
    //     shotAmount = 0;
    //     manaCost = 3;
    //     gridWidth = 2;
    //     gridHeight = 2;
    // }

    public void StartMove()
    {
        // TODO: Calculate trajectory
        isMoving = true;
        MoveBall();
    }

    public void MoveBall()
    {
        // TODO: Implement ball movement logic
    }
}
