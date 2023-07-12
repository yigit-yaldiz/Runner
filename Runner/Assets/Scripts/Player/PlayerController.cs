using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveClamp;
    [SerializeField] private float _speed;
    [SerializeField] Transform _playerTransform;

    private Vector3 _targetPos;
    private bool _isDragging = false;

    private Vector3 _lastPosition;

    private void Update()
    {
        MouseInput();
        Movement();
    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0) && !Finish.IsGameFinished)
        {
            _isDragging = true;
            _lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }
    }

    private void Movement()
    {
        if (!_isDragging || Obstacle.IsItCrashed) return;
        
        float x = Mathf.Clamp(_playerTransform.localPosition.x + (MouseDelta().x * _speed / 1000), -_moveClamp, _moveClamp);
        _targetPos = new Vector3(x, _playerTransform.localPosition.y, _playerTransform.localPosition.z);
        _playerTransform.localPosition = _targetPos;

        float xPos = _playerTransform.localPosition.x;
        xPos = Mathf.Clamp(xPos, -_moveClamp, _moveClamp);
        _playerTransform.localPosition = new Vector3(xPos, _playerTransform.localPosition.y, _playerTransform.localPosition.z);
    }

    private Vector3 MouseDelta()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 deltaPosition = currentPosition - _lastPosition;
        _lastPosition = currentPosition;
        return deltaPosition;
    }
  
}
