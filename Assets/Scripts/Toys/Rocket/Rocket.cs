using UnityEngine;

public class Rocket : Toy
{
    public float speed = 5f;  
    private Rigidbody rb;

    public Rocket()
    {
        hp = 5;
        isMovable = true;
        isMoving = true;
        canBulletBounce = false;
        shotDegree = 0;
        shotAmount = 0;
        manaCost = 5;
        gridWidth = 1;
        gridHeight = 1;
        timeActive = 5;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Nightmare enemy = collision.gameObject.GetComponent<Nightmare>();
        if (enemy != null)
        {
            enemy.TakeDamage(20);
            TakeDamage(hp);
        } 
    }

}
