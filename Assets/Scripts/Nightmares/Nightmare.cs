using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Nightmare : MonoBehaviour
{
    public float hp;
    public bool isMoving = true;
    public float gridWidth;
    public float gridHeight;
    public bool isAlive = true;
    public float speed = 1;
    public float timeToInitialize;
    public bool touchedWall = false;
    public float scareLevelAppear;
    public float scareLevelPassive;
    public float scareLevelReachWall;
    public float scareLevelDisappear;
    public bool isInvisible;
    //public static event Action NightmareCreated = delegate { };
    public static event Action<float> NightmareDestroyed = delegate { };

    protected async virtual void Awake()
    {
        await DelayActivation();
        //NightmareCreated();
    }

    private async Task DelayActivation()
    {
        gameObject.SetActive(false);
        await Task.Delay(TimeSpan.FromSeconds(timeToInitialize));
        gameObject.SetActive(true);
    }

    private void Update()
    {
        Move();
    }

    
    public void TakeDamage(float howMuch) {
        hp -= howMuch;
        if (hp <= 0 ) {
            DestroyNightmare();
        }
    }

    public void DestroyNightmare() {
        isAlive = false;
        //TODO: add logic - remove from active playerToys
        gameObject.SetActive(false);
        NightmareDestroyed(scareLevelDisappear);
    }

    public virtual void Move() 
    {
        if (isMoving)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }

}