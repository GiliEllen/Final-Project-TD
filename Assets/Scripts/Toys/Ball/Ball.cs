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
        timeActive = 5;
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
        Nightmare enemy = collision.gameObject.GetComponent<Nightmare>();
        if (enemy != null)
        {
            enemy.TakeDamage(1);

            TakeDamage(hp);
        } 
    }

    private void Update() {
        elapsedTime += Time.deltaTime;
        ActivatedTimeIsUp();
    }
}
