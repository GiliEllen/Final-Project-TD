using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectDatabaseSO database;

    private int selectedObjectIndex = -1;

    [SerializeField]
    private GameObject gridVisualization;

    private void Start()
    {
        StopPlacement();
        inputManager.OnLongTouchStart += StartPlacementMode;
        inputManager.OnDrag += UpdatePlacementIndicators;
        inputManager.OnDrop += PlaceStructure;
        inputManager.OnCancelPlacement += StopPlacement;
    }

    private void StartPlacementMode()
    {
        StartPlacement(0);
        if (selectedObjectIndex >= 0)
        {
            gridVisualization.SetActive(true);
            cellIndicator.SetActive(true);
        }
    }

    public void StartPlacement(int ID)
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
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
    }

    private void UpdatePlacementIndicators()
    {
        if (selectedObjectIndex < 0) return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }

    private void PlaceStructure()
    {
        if (selectedObjectIndex < 0) return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition);

        StopPlacement();
    }
}
