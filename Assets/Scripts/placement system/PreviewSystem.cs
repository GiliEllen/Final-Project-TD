using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField]
    private float previewYOffset = 0.06f;
    [SerializeField]
    private GameObject cellIndicator;
    private GameObject previewObject;
    [SerializeField]
    private Material previewMaterialPrefab;
    private Material previewMaterialInstance;

    private void Start() {
        previewMaterialInstance = new Material(previewMaterialPrefab);
        cellIndicator.SetActive(false);
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size) {
        previewObject = Instantiate(prefab);
        PreparePreview(previewObject);
        PrepareCursor(size);
        cellIndicator.SetActive(true);
    }

    private void PreparePreview(GameObject previewObject) {

        ProcessObject(previewObject);

    // Recursively process all children
    foreach (Transform child in previewObject.transform) {
        ProcessObject(child.gameObject);
    }
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++) 
            {
                materials[i] = previewMaterialInstance;
            }

            renderer.materials = materials;
        }
    }

    private void ProcessObject(GameObject obj) {
    // Disable Rigidbody if it exists
    Rigidbody rb = obj.GetComponent<Rigidbody>();
    if (rb != null) {
        rb.isKinematic = true; // Disable physics interactions
        rb.detectCollisions = false; // Prevent collision detection
        rb.useGravity = false; // Disable gravity
    }

    // Disable all Colliders
    Collider[] colliders = obj.GetComponents<Collider>();
    foreach (Collider collider in colliders) {
        collider.enabled = false;
    }
    }
    private void PrepareCursor(Vector2Int size) {
        if (size.x > 0 && size.y > 0) {
            cellIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
            cellIndicator.GetComponentInChildren<Renderer>().material.mainTextureScale = size;
        }
    }

    public void StopShowingPreview() {
        cellIndicator.SetActive(false);
        Destroy(previewObject);
    }

    public void UpdatePosition(Vector3 position) 
    {
        MovePreview(position);
        MoveCursor(position);
    }

    private void MovePreview(Vector3 position) 
    {
        previewObject.transform.position = new Vector3(position.x, position.y+ previewYOffset, position.z);
    }
    private void MoveCursor(Vector3 position) 
    {
        cellIndicator.transform.position = position;
    }
}
