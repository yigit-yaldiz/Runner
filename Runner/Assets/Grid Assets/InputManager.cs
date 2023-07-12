using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Camera _camera;
    Vector3 _lastPosition;
    [SerializeField] LayerMask _placementLayerMask;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        //mousePos.z = _camera.nearClipPlane;
        Ray ray = _camera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, _placementLayerMask))
        {
            _lastPosition = hit.point;
        }

        return _lastPosition;
    }
}
