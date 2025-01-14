using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerCubes;
    public float totalCubes;
    public float playerMana = 0;
    public PlayerToys playerToys;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddMana(int addedMana) {
        playerMana += addedMana;
    }

    //TODO: add Toy class
    public void PlaceToy(string toy, Vector3 position) {
        //TODO: place toy logic
        Debug.Log(position);
        Debug.Log(toy);
    }

    public void SetPlayerCubes()
    {
        if (totalCubes < 0) {
            Debug.LogError("Player - SetPlayerCubes(): Total cubes has not been set");
            return;
        }
        playerCubes = totalCubes / 2;
        Debug.Log($"Total Cubes: {totalCubes}, Player Cubes: {playerCubes}");
    }

    // public bool IsGameOver()
    // {
    //     if (IsGameLost() || IsGameWon())
    //     {
    //         return true; 
    //     }
    //     return false; 
    // }

    // public bool IsGameWon()
    // {
    //     if (playerCubes >= totalCubes * 0.95f) 
    //     {
    //         return true; 
    //     }
    //     return false; 
    // }

    // public bool IsGameLost()
    // {
    //     if (playerCubes <= totalCubes * 0.05f)
    //     {
    //         return true; 
    //     }
    //     return false; 
    // }

}
