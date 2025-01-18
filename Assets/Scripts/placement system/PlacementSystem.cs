using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject cellIndicator;

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

        inputManager.OnDrag += UpdatePlacementIndicators;
        inputManager.OnDrop += PlaceStructure;
         Debug.Log("Subscribed to OnDrag and OnDrop events.");
    }

   public void StartPlacementFromButton(int ID)
{
    StartPlacement(ID);
    if (selectedObjectIndex >= 0)
    {
        Debug.Log($"Placement mode started for object ID: {ID}");
        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
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
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
    }

    private void UpdatePlacementIndicators()
    {
        if (selectedObjectIndex < 0) return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }

    private void PlaceStructure()
    {
        if (selectedObjectIndex < 0) return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        Debug.Log($"Placing object at Grid Position: {gridPosition}");

        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition);

        StopPlacement();
    }
}
