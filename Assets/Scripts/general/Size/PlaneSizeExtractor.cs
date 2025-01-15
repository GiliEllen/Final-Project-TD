using UnityEngine;

public static class PlaneSizeExtractor
{
    public static float getXSize(GameObject plane)
    {
        if (plane != null && plane.TryGetComponent(out MeshFilter meshFilter))
        {
            Vector3 meshSize = meshFilter.sharedMesh.bounds.size;
            Vector3 worldScale = plane.transform.lossyScale;
            return meshSize.x * worldScale.x; // Return the X length in world space
        }

        Debug.LogWarning("Plane is null or has no MeshFilter.");
        return 0f;
    }

    public static float getZSize(GameObject plane)
    {
        if (plane != null && plane.TryGetComponent(out MeshFilter meshFilter))
        {
            Vector3 meshSize = meshFilter.sharedMesh.bounds.size;
            Vector3 worldScale = plane.transform.lossyScale;
            return meshSize.z * worldScale.z; // Return the Z length in world space
        }

        Debug.LogWarning("Plane is null or has no MeshFilter.");
        return 0f;
    }
}
