using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Nightmare
{
    private float timer = 0f;

    [SerializeField]
    private Material materialVisible;  
    [SerializeField]
    private Material materialInvisible;
    private Renderer ghostRenderer;
     private Rigidbody ghostRigidbody;

     public float timeUntilDisappear = 3f;
     public float timeUntilReappear = 2f;
    public Ghost()
    {
        hp = 15;
        isMoving = true;
        gridWidth = 1;
        gridHeight = 1;
        speed = 0.8f;
        scareLevelAppear = 10;
        scareLevelPassive = 0;
        scareLevelReachWall = 60;
        scareLevelDisappear = -15;
        isInvisible = false;
    }

    private void Start()
    {
        ghostRenderer = GetComponent<Renderer>();
        ghostRigidbody = GetComponent<Rigidbody>();
    }

    public void GhostDisappear() {
        isInvisible = true;
        ghostRenderer.material = materialInvisible; 
        ghostRigidbody.isKinematic = true;
        isMoving = false;
    }
    public void GhostReappear() {
        isInvisible = false;
        ghostRenderer.material = materialVisible;
        ghostRigidbody.isKinematic = false;
        Vector3 currentPosition = transform.position;
        float randomX = Random.Range(-4, 6);  
        transform.position = new Vector3(Mathf.Round(randomX) +0.5f, currentPosition.y, currentPosition.z);
        if (!touchedWall) {
            isMoving = true;
        }
    }

      private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeUntilReappear)
        {
            if (isInvisible)
            {
                GhostReappear();
                timer = 0f;
            }
        }

        if (timer >= timeUntilDisappear) {
             if (!isInvisible)
            {
                GhostDisappear();
                timer = 0f;
            }
        }
          Move();
    }
}