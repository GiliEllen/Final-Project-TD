using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class smokeEffect : MonoBehaviour
{
        private float elapsedTime = 0f; 
    void Start()
    {
        
    }

        private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 5) {
            DestroySmoke();
        }
    }

    public void DestroySmoke() {
        Destroy(gameObject);
    }
}
