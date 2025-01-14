using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToys : MonoBehaviour
{
    // player toys list, contains their level and info
    // general list for toys, NOT active toys
    //[ 
    //  {
    //      toyName: 1
    //      toyType: toy type
    //      toyLevel: 1
    //      toyHpAdded: 1
    //      toyShotAmountAdded: 1
    //      isActiveForPlayer: true,
    //      toyPosition: vector 3 ? any other positioning options
    //  }
    //]

    public List<PlayerToy> Toys { get; private set; }

    public PlayerToys()
    {
        Toys = new List<PlayerToy>();
    }

    void Awake() {
        // PlayerToys();
        // CreateNewPlayerToyList();
    }

    public void AddToy(PlayerToy toy)
    {
        Toys.Add(toy);
    }

    public void RemoveToy(PlayerToy toy)
    {
        Toys.Remove(toy);
    }

    // public void CreateNewPlayerToyList() {
        //TODO: go through all types on initial load
        //laser - TODO: change type
        // PlayerToy newLaser = PlayerToy.CreateNewPlayerToy(Laser);
        // AddToy(newLaser);
    // }

}