using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperZombie : Nightmare
{
    public SuperZombie()
    {
        hp = 20;
        isMoving = true;
        gridWidth = 1;
        gridHeight = 1;
        speed = 1.8f;
        scareLevelAppear = 10;
        scareLevelPassive = 10;
        scareLevelReachWall = 60;
        scareLevelDisappear = -15;
        isInvisible= false;
    }
}