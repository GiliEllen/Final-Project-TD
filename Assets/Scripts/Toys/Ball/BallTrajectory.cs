using UnityEngine;

public class BallTrajectory : MonoBehaviour
{
    public static Vector3 CalculateTrajectory(GameObject center, Transform ballTransform)
    {
        if (center == null)
        {
            Debug.LogError("Center object is not assigned!");
            return Vector3.zero;
        }

        float directionX = ballTransform.position.x < center.transform.position.x ? 1f : -1f;

        float directionZ = 1f;

        Vector3 trajectory = new Vector3(directionX, 0f, directionZ).normalized;

        return trajectory;
    }
}
