using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] GameObject _cellIndicator;
    [SerializeField] InputManager _inputManager;
    [SerializeField] Grid _grid;

    private void Update()
    {
        Vector3 mousePos = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = _grid.WorldToCell(mousePos);
        _cellIndicator.transform.position = _grid.CellToWorld(gridPos);
    }
}
