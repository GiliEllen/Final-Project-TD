using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

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
    private bool activatePortal = true;
    //public static event Action NightmareCreated = delegate { };
    public static event Action<float> NightmareDestroyed = delegate { };

    private bool isMovementDelayed = false;

    protected async virtual void Awake()
    {
        await DelayActivation();
    }

    private async Task DelayActivation()
    {
        gameObject.SetActive(false); 
        await Task.Delay(TimeSpan.FromSeconds(timeToInitialize));
        if (activatePortal) {
            GameObject portal = Instantiate(Resources.Load("EnemyPortal"), transform.position, Quaternion.identity) as GameObject;
        }

        gameObject.SetActive(true); 
        
        await StartMovementDelay();
    }

    private async Task StartMovementDelay()
    {
        await Task.Delay(1000); 
        isMovementDelayed = true;
    }

    private void Update()
    {
        if (isMovementDelayed)
        {
            Move();
        }
    }

    public void TakeDamage(float howMuch) {
        hp -= howMuch;

        transform.DOShakePosition(0.3f, strength: 0.2f, vibrato: 10, randomness: 90);

        if (hp <= 0 ) {
            DestroyNightmare();
        }
    }

    public void DestroyNightmare() {
        isAlive = false;
        //TODO: add logic - remove from active playerToys
        gameObject.SetActive(false);
        GameObject smoke = Instantiate(Resources.Load("DarkSmoke"), transform.position, Quaternion.identity) as GameObject;

        NightmareDestroyed(scareLevelDisappear);
        Destroy(gameObject);
    }

    public virtual void Move() 
    {
        if (isMoving)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }
}
