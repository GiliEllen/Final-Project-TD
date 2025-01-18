using UnityEngine;

public class RandomUtils : MonoBehaviour
{
    // Method to return either -1 or 1 randomly
    public static int GetRandomSign()
    {
        return Random.Range(0, 2) == 0 ? -1 : 1;
    }
}
