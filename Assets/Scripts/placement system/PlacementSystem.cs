using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.UI.Image;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectDatabaseSO database;

    private int selectedObjectIndex = -1;

    [SerializeField]
    private GameObject gridVisualization;
    [SerializeField]
    private PreviewSystem preview;
    public List<PlacementButton> placementButtons;

    private MeshCollider gridMeshCollider;

    [SerializeField] private LayerMask placementObstructionsLayerMask;

    private void Start()
    {
        // Find the MeshCollider attached to the grid visualization object
        gridMeshCollider = gridVisualization.GetComponent<MeshCollider>();
        
        StopPlacement();
        inputManager.OnDrag += UpdatePlacementIndicators;
        inputManager.OnDrop += PlaceStructure;
    }

    public void StartPlacementFromButton(int ID)
    {
        StartPlacement(ID);
        if (selectedObjectIndex >= 0)
        {
            // gridVisualization.SetActive(true);
            preview.StartShowingPlacementPreview(database.objectsData[selectedObjectIndex].Prefab, database.objectsData[selectedObjectIndex].Size);
        }
        else
        {
            Debug.LogWarning($"No valid object found for ID: {ID}");
        }
    }

    private void StartPlacement(int ID)
    {
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found for {ID}");
            return;
        }
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        // gridVisualization.SetActive(false);
        preview.StopShowingPreview();
    }

    private void UpdatePlacementIndicators()
    {
        if (selectedObjectIndex < 0) return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        // Ensure the preview position stays inside the grid bounds
        Vector3 validPosition = GetValidPositionInsideGrid(gridPosition);
        preview.UpdatePosition(validPosition);
    }

    private Vector3 GetValidPositionInsideGrid(Vector3Int gridPosition)
    {
        Vector3 worldPosition = grid.CellToWorld(gridPosition);

        // Check if the position is inside the grid's mesh bounds
        Vector3 localPosition = gridVisualization.transform.InverseTransformPoint(worldPosition);
        Vector3 meshMin = gridMeshCollider.bounds.min;
        Vector3 meshMax = gridMeshCollider.bounds.max;

        // Clamp the position to stay within the grid's bounds
        localPosition.x = Mathf.Clamp(localPosition.x, meshMin.x, meshMax.x);
        localPosition.z = Mathf.Clamp(localPosition.z, meshMin.z, meshMax.z);

        // Convert back to world position
        return gridVisualization.transform.TransformPoint(localPosition);
    }

    private void PlaceStructure()
    {
        if (selectedObjectIndex < 0) return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        // Get a valid position for placing the object inside the grid bounds
        Vector3 validPosition = GetValidPositionInsideGrid(gridPosition);

        Vector2 size = database.objectsData[selectedObjectIndex].Size;
        if (database.objectsData[selectedObjectIndex].Name != "Rocket" && !IsPositionAvailable(validPosition, size))
        {
            StopPlacement();
            return;
        }

        // Instantiate and place the object
        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = new(validPosition.x, 2, validPosition.z);

        PlacementButton buttonAtIndex = placementButtons[selectedObjectIndex];
        buttonAtIndex.StartCooldown();

        StopPlacement();
    }

    private bool IsPositionAvailable(Vector3 pos, Vector2 size)
    {
        pos = new(pos.x + size.x / 2, pos.y, pos.z + size.y / 2f);
        bool res = Physics.CheckBox(pos, new(size.x / 2, 15f, size.y / 2), 
            Quaternion.identity, placementObstructionsLayerMask);
                return !res;
    }

}
