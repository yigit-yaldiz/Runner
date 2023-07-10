using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    [SerializeField] [Range(8, 999)] private int _targetFPS;

    private void Update()
    {
        Application.targetFrameRate = _targetFPS;
    }
}
