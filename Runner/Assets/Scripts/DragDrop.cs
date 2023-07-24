using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public int VehicleIndex => _vehicleIndex;
    public bool IsThatMerged;

    Vector3 _mousePosition;
    Vector3 _parentPosition;
    bool _isInAir;
    int _vehicleIndex;

    void Awake()
    {
        _parentPosition = transform.parent.position;
    }
    private void OnEnable()
    {
        CarSelecting.CarSelected += DisableTheComponent;
    }

    private void OnDisable()
    {
        CarSelecting.CarSelected -= DisableTheComponent;
    }

    #region Drag & Drop
    Vector3 GetObjectPosition()
    {    
        return Camera.main.WorldToScreenPoint(transform.parent.position);
    }

    private void OnMouseDown()
    {
        if (StateMachine.Instance.GameState != GameStates.Merge)
        {
            return;
        }

        _mousePosition = Input.mousePosition - GetObjectPosition();
    }

    private void OnMouseDrag()
    {
        if (StateMachine.Instance.GameState != GameStates.Merge)
        {
            return;
        }

        Vector3 objectPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
        objectPos.y = 0;
        transform.parent.position = RoundThePoint(objectPos, 1f);
        _isInAir = true;
    }

    private void OnMouseUp()
    {
        _isInAir = false;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (StateMachine.Instance.GameState != GameStates.Merge)
        {
            return;
        }

        if (other.CompareTag("Vehicle") && !_isInAir && _vehicleIndex == other.GetComponent<DragDrop>().VehicleIndex)
        {
            if (_vehicleIndex != other.GetComponent<DragDrop>().VehicleIndex)
            {
                Debug.LogWarning("Vehicle indexs are not equal !");
            }

            UpgradeTier(other);
        }
    }

    public void SetVehicleIndex(int index)
    {
        _vehicleIndex = index;
    }

    void UpgradeTier(Collider other)
    {
        int nextTier = _vehicleIndex + 1;

        GameObject nextTierObject = Instantiate(PlacementSystem.Instance.Database.objectsDatas[nextTier].Prefab, transform.parent.parent);
        nextTierObject.GetComponentInChildren<DragDrop>().IsThatMerged = true;
        nextTierObject.GetComponentInChildren<DragDrop>().SetVehicleIndex(nextTier);
        nextTierObject.transform.position = _parentPosition;

        Destroy(other.transform.parent.gameObject);
        Destroy(transform.parent.gameObject);
    }

    void DisableTheComponent(Collider other)
    {
        enabled = false;
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
