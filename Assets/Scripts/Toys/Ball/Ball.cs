using UnityEngine;

public class Ball : Toy
{
    public float speed = 5f;  
    private Rigidbody rb;

    public Ball()
    {
        hp = 5;
        isMovable = true;
        isMoving = false;
        canBulletBounce = true;
        shotDegree = 0;
        shotAmount = 0;
        manaCost = 3;
        gridWidth = 2;
        gridHeight = 2;
    }

    void Start() {
        StartMove();
    }

    public void StartMove()
    {
        isMoving = true;
        MoveBall();
    }

    public void MoveBall()
    {
        rb = GetComponent<Rigidbody>();

        Vector3 trajectory = BallTrajectory.CalculateTrajectory(GameObject.Find("center"), transform);
        rb.velocity = trajectory * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        // Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        // if (enemy != null)
        {
            // enemy.TakeDamage(damageAmount);

            // TakeDamage(damageAmount);
        }
    }
}
