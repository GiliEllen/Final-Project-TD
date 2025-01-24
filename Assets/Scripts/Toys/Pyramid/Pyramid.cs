using UnityEngine;

public class Pyramid : Toy
{
    public GameObject hoopPrefab; // Prefab for the hoop

    private float shootTimer; // Timer to handle the interval between shots
    private float activeTimer; // Timer to track how long the pyramid is active
    private bool isShooting; // Flag to indicate whether the pyramid is shooting

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
            // Handle shooting logic
            shootTimer += Time.deltaTime;
            if (shootTimer >= 1f / shotAmount) // Fire at the specified rate
            {
                shootTimer = 0f;
                FireHoop();
            }

            // Handle deactivation logic
            activeTimer += Time.deltaTime;
            if (activeTimer >= timeActive)
            {
                Deactivate();
            }
        }
    }

    public void StartShotting()
    {
        // Start the shooting process
        isShooting = true;
        shootTimer = 0f;
        activeTimer = 0f;
    }

    private void FireHoop()
    {
        // Calculate the fire point dynamically
        Vector3 firePoint = transform.position + new Vector3(0, 0, 2);

        // Instantiate a hoop at the fire point
        if (hoopPrefab != null)
        {
            Instantiate(hoopPrefab, firePoint, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Hoop prefab is not assigned!");
        }
    }

    private void Deactivate()
    {
        // Stop shooting and deactivate the pyramid
        isShooting = false;
        gameObject.SetActive(false);
    }
}
