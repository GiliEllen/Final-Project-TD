using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiEffect : MonoBehaviour
{
    private float elapsedTime = 0f; 
    void Start()
    {
        
    }

        private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 6) {
            DestroyEffect();
        }
    }

    public void DestroyEffect() {
        Destroy(gameObject);
    }
}
