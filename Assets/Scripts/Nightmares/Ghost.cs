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
    }
    public void GhostReappear() {
        isInvisible = false;
        ghostRenderer.material = materialVisible;
        ghostRigidbody.isKinematic = false;
        Vector3 currentPosition = transform.position;
        float randomX = Random.Range(-4, 7);  
        transform.position = new Vector3(Mathf.Round(randomX), currentPosition.y, currentPosition.z);
    }

      private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2f)
        {
            if (isInvisible)
            {
                GhostReappear();
            }
            else
            {
                GhostDisappear();
            }

            timer = 0f;
        }
          Move();
    }
}