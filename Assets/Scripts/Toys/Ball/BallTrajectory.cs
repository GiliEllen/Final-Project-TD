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

        float zDirection = center.transform.position.z - ballTransform.position.z;
        float normalizedZ = zDirection > 0 ? 1 : -1; 

        Vector3 trajectory = new Vector3(0, 0, normalizedZ);

        return trajectory;
    }
}
