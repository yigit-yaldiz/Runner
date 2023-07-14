using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] GameObject _cellIndicator;
    [SerializeField] InputManager _inputManager;
    [SerializeField] Grid _grid;

    [SerializeField] ObjectsDatabaseSO _database;

    [SerializeField] LayerMask _vehicleLayerMask;

    public Transform CellIndicatorTransform => _cellIndicator.transform;
    public ObjectsDatabaseSO Database => _database;

    int _selectedObjectIndex = -1;
    Vector3 _offset = new Vector3(0, 0, -1);

    private void OnEnable()
    {
        _inputManager.OnClicked += PlaceStructure;
        _inputManager.OnExit += StopPlacement;
    }

    private void OnDisable()
    {
        _inputManager.OnClicked -= PlaceStructure;
        _inputManager.OnExit -= StopPlacement;
    }

    private void Start()
    {
        StopPlacement();
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        _selectedObjectIndex = _database.objectsDatas.FindIndex(data => data.ID == ID);
        Vector2Int objectSize = _database.objectsDatas[_selectedObjectIndex].Size;

        if (_selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        //_cellIndicator.SetActive(true);
        _cellIndicator.transform.localScale = new Vector3(objectSize.x + 0.1f, _cellIndicator.transform.localScale.y, objectSize.y + 0.1f);
    }

    private void PlaceStructure()
    {
        Vector3 mousePosition = Input.mousePosition;
        //mousePos.z = _camera.nearClipPlane;
        Ray ray = _inputManager.Camera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, _vehicleLayerMask))
        {
            if (hit.collider.CompareTag("Vehicle"))
            {
                Debug.LogWarning("There is a car!");
                return; 
            }
        }

        if (_inputManager.IsPointerOverUI())
        {
            return;
        }

        Vector3 mousePos = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = _grid.WorldToCell(mousePos);
        GameObject newObject = Instantiate(_database.objectsDatas[_selectedObjectIndex].Prefab);
        newObject.transform.position = _grid.CellToWorld(gridPos) + _offset;

        newObject.GetComponentInChildren<DragDrop>().SetVehicleIndex(_selectedObjectIndex);
    }

    private void StopPlacement()
    {
        _selectedObjectIndex = -1;
        _cellIndicator.SetActive(false);
    }

    private void Update()
    {
        if (_selectedObjectIndex < 0)
        {
            return;
        }

        Vector3 mousePos = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = _grid.WorldToCell(mousePos);
        _cellIndicator.transform.position = _grid.CellToWorld(gridPos) + _offset;
    }
}
