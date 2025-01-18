using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Nightmare
{
    public Skeleton()
    {
        hp = 10;
        isMoving = false;
        gridWidth = 1;
        gridHeight = 1;
        speed = 1;
    }

    public override void Move() 
    {
        return;
    }

}