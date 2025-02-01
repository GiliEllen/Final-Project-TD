using UnityEngine;

public class Pyramid : Toy
{
    public GameObject hoopPrefab; 

    private float shootTimer;
    private float activeTimer;
    private bool isShooting; 

    public Pyramid()
    {
        hp = 5;
        isMovable = false;
        isMoving = false;
        canBulletBounce = false;
        shotDegree = 0;
        shotAmount = 1;
        manaCost = 5;
        gridWidth = 2;
        gridHeight = 2;
        timeActive = 10;
        type = "Pyramid";
    }

    private void Start() {
        StartShotting();
    }

    private void Update()
    {
        if (isShooting)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= 1f / shotAmount) 
            {
                shootTimer = 0f;
                FireHoop();
            }

            activeTimer += Time.deltaTime;
            if (activeTimer >= timeActive)
            {
                Deactivate();
            }
        }
    }

    public void StartShotting()
    {
        isShooting = true;
        shootTimer = 0f;
        activeTimer = 0f;
    }

    private void FireHoop()
    {
        Vector3 firePoint = transform.position + new Vector3(0, 0, 2);

        if (hoopPrefab != null)
        {
            Instantiate(hoopPrefab, firePoint, Quaternion.identity, transform);
        }
        else
        {
            Debug.LogWarning("Hoop prefab is not assigned!");
        }
    }

    private void Deactivate()
    {
        isShooting = false;
        gameObject.SetActive(false);
        DestroyToy();
    }
}
