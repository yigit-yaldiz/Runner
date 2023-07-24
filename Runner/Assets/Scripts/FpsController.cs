using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    [SerializeField] [Range(8, 999)] private int _targetFPS;

    bool _isTimeMultiplied;

    private void Update()
    {
        Application.targetFrameRate = _targetFPS;

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_isTimeMultiplied)
            {
                _isTimeMultiplied = false;
                Time.timeScale = 1;
            }
            else
            {
                _isTimeMultiplied = true;
                Time.timeScale = 4;
            }   
        }
    }
}
