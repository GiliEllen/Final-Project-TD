using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToy : MonoBehaviour
{

    public Toy ToyTemplate { get; private set; }
    public float toyLevel;
    public float toyHpAdded;
    public float toyShotAmountAdded;
    public bool isActiveForPlayer;

    //public Vector3 toyPosition: vector 3 ? any other positioning options

    //public void CreateToy(float id, string toyType, float newLevel = 1, float newHpAdded = 0, float newShotAmountAdded = 0 )

    public void LevelUpToy(float newHpAdded = 0, float newShotAmountAdded = 0 ) {
        toyLevel += 1;
        toyHpAdded += newHpAdded;
        toyShotAmountAdded += newShotAmountAdded;
    }

    public void CreateNewPlayerToy(Toy toyTemplate)
    {
        ToyTemplate = toyTemplate;
        toyLevel = 1;
        toyHpAdded = 0;
        toyShotAmountAdded = 0;
        isActiveForPlayer = true;
    }

}