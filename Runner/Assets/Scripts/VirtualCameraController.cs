using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    [SerializeField] List<GameObject> _virtualCameras;
    private void OnEnable()
    {
        Finish.StateFinished += ChangeTheCamera;
    }

    private void OnDisable()
    {
        Finish.StateFinished -= ChangeTheCamera;
    }

    void ChangeTheCamera(GameStates state)
    {
        foreach (GameObject camera in _virtualCameras)
        {
            camera.SetActive(false);
        }

        int nextCameraIndex = (int)state + 1;

        if (nextCameraIndex >= _virtualCameras.Count)
        {
            return;
        }

        _virtualCameras[nextCameraIndex].SetActive(true);
    }
}
