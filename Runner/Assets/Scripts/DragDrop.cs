using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    [SerializeField] PlacementSystem _placementSystem;

    public int VehicleIndex => _vehicleIndex;

    Vector3 _mousePosition;
    Vector3 _parentPosition;
    bool _isInAir;
    int _vehicleIndex;

    void Awake()
    {
        _placementSystem = FindObjectOfType<PlacementSystem>();
        _parentPosition = transform.parent.position;
    }

    #region Drag & Drop
    Vector3 GetObjectPosition()
    {    
        return Camera.main.WorldToScreenPoint(transform.parent.position);
    }

    private void OnMouseDown()
    {
        if (StateMachine.Instance.GameState != StateMachine.GameStates.Merge)
        {
            return;
        }

        _mousePosition = Input.mousePosition - GetObjectPosition();
    }

    private void OnMouseDrag()
    {
        if (StateMachine.Instance.GameState != StateMachine.GameStates.Merge)
        {
            return;
        }

        Vector3 objectPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
        objectPos.y = 0;
        transform.parent.position = RoundThePoint(objectPos, 1f);
        _isInAir = true;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (StateMachine.Instance.GameState != StateMachine.GameStates.Merge)
        {
            return;
        }

        if (other.CompareTag("Vehicle") && !_isInAir && _vehicleIndex == other.GetComponent<DragDrop>().VehicleIndex)
        {
            UpgradeTier(other);
        }
        else
        {
            Debug.LogWarning("Vehicle indexs are not equal !");
        }
    }

    public void SetVehicleIndex(int index)
    {
        _vehicleIndex = index;
    }

    void UpgradeTier(Collider other)
    {
        int nextTier = _vehicleIndex + 1;
        Debug.Log(nextTier);

        GameObject nextTierObject = Instantiate(_placementSystem.Database.objectsDatas[nextTier].Prefab, transform.parent.parent);
        nextTierObject.GetComponentInChildren<DragDrop>().SetVehicleIndex(nextTier);
        nextTierObject.transform.position = _parentPosition;

        Destroy(other.transform.parent.gameObject);
        Destroy(transform.parent.gameObject);
    }

    private Vector3 RoundThePoint(Vector3 position, float digitAmount)
    {
        Vector3 pos;
        pos.x = (float) Mathf.Round(position.x * digitAmount) / digitAmount;
        pos.y = (float) Mathf.Round(position.y * digitAmount) / digitAmount;
        pos.z = (float) Mathf.Round(position.z * digitAmount) / digitAmount;

        pos.x = Mathf.Clamp(pos.x, -5, 3);
        pos.z = Mathf.Clamp(pos.z, 50, 57);
        
        position = pos;

        return position;
    }

}
