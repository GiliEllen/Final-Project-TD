using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Nightmare
{
    public Zombie()
    {
        hp = 1;
        isMoving = false;
        gridWidth = 2;
        gridHeight = 2;
    }
}