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

        Vector3 directionToCenter = center.transform.position - ballTransform.position;

        Vector3 trajectory = directionToCenter.normalized;

        return trajectory;
    }
}
