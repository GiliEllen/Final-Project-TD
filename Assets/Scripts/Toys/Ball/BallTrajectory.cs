using UnityEngine;

public class BallTrajectory : MonoBehaviour
{
    public GameObject center; 
    public float speed = 5f;  
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Vector3 trajectory = CalculateTrajectory();
        rb.velocity = trajectory * speed;
    }

    private Vector3 CalculateTrajectory()
    {
        if (center == null)
        {
            Debug.LogError("Center object is not assigned!");
            return Vector3.zero;
        }

        float directionX = transform.position.x < center.transform.position.x ? 1f : -1f;

        Vector3 trajectory = new Vector3(directionX, 0f, Random.Range(-1f, 1f)).normalized;
        return trajectory;
    }
}
