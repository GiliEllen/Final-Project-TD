using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System.Threading;

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
    [SerializeField] private float scareLevelIncreaseFrequency;
    //public static event Action NightmareCreated = delegate { };
    public static event Action<float> NightmareDestroyed = delegate { };
    public static event Action<float> IncreaseScareLevel = delegate { };
    private bool isMovementDelayed = false;
    private CancellationTokenSource _cts;

    protected async virtual void Awake()
    {
        await DelayActivation();
        IncreaseScareLevel(scareLevelAppear);
        _cts = new CancellationTokenSource();
        ScareLevelUpdate();
    }

    protected virtual void OnDestroy()
    {
        if (_cts != null) {
            _cts.Dispose();
        }
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

    protected async void ScareLevelUpdate()
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(scareLevelIncreaseFrequency));
            if (_cts.IsCancellationRequested)
                return;
            IncreaseScareLevel(scareLevelPassive);
        }
    }

    public void TakeDamage(float howMuch) {
        hp -= howMuch;

        transform.DOShakePosition(0.3f, strength: 0.2f, vibrato: 10, randomness: 90);

        if (hp <= 0 ) {
            DestroyNightmare();
        }
    }

    public virtual void DestroyNightmare(bool wasDestroyedByPlayer = true) {
        isAlive = false;
        //TODO: add logic - remove from active playerToys
        GameObject smoke = Instantiate(Resources.Load("DarkSmoke"), transform.position, Quaternion.identity) as GameObject;

        float scareLevelAddition = wasDestroyedByPlayer ? scareLevelDisappear : 0;
        NightmareDestroyed(scareLevelAddition);
        _cts.Cancel();
        Destroy(gameObject);
    }

    protected virtual void Move() 
    {
        if (isMoving)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }
}
