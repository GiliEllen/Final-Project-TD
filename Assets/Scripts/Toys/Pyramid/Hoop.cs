using UnityEngine;

public class Hoop : MonoBehaviour
{
    public float speed = 10f; 
    public int damage = 5; 

    private float initialY; 

    void Start()
    {
        initialY = transform.position.y;
    }

    void Update()
    {
        transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Nightmare enemy = collision.gameObject.GetComponent<Nightmare>();
        if (enemy != null)
        {
            enemy.TakeDamage(5);
            gameObject.SetActive(false);
        } 
    }
}
