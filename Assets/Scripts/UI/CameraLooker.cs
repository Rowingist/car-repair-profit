using UnityEngine;

public class CameraLooker : MonoBehaviour
{
    private Transform _camera;
    private Transform _transform;

    private void Start()
    {
        _camera = Camera.main.transform;
        _transform = transform;
    }

    private void LateUpdate()
    {
        _transform.rotation = _camera.transform.rotation;
    }
}
