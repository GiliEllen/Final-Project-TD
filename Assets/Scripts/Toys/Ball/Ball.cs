using UnityEngine;

public class Ball : Toy
{
    public float speed = 5f;  
    private Rigidbody rb;
    [SerializeField] private float knockBackForce;

    public Ball()
    {
        hp = 2;
        isMovable = true;
        isMoving = false;
        canBulletBounce = true;
        shotDegree = 0;
        shotAmount = 0;
        manaCost = 3;
        gridWidth = 1;
        gridHeight = 1;
        timeActive = 5;
        type = "ball";
    }

    void Start() {
        StartMove();
    }

    public void StartMove()
    {
        isMoving = true;
        MoveBall();
    }

    private void MoveBall()
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
            if (!enemy.isInvisible) {    
                enemy.TakeDamage(3);
                TakeDamage(1);
            }
        } 
    }

    private void Update() {
        elapsedTime += Time.deltaTime;
        ActivatedTimeIsUp();
    }

    public float GetKnockBackForce()
    {
        return knockBackForce;
    }
}
