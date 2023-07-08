using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    const float _limitValue = 1.25f;

    private void Update()
    {
        if (Input.GetMouseButton(0))
	    {
            MoveHorizontal();
        }
    }
    
    private void MoveHorizontal()
    {
        float halfOfScreen = Screen.width / 2;
        float xPos = (Input.mousePosition.x - halfOfScreen) / halfOfScreen;
        float finalXPos = Mathf.Clamp(xPos, -_limitValue, _limitValue);

        _playerTransform.localPosition = new Vector3(finalXPos, _playerTransform.localPosition.y, _playerTransform.localPosition.z);
    }
}
