using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Nightmare
{
    public Zombie()
    {
        hp = 10;
        isMoving = false;
        gridWidth = 1;
        gridHeight = 1;
        speed = 1f;
    }

}