using UnityEngine;

public class BillboardCanvasController : MonoBehaviour
{
    private Camera _targetCamera;

    private void Start()
    {
        if (_targetCamera == null)
        {
            _targetCamera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = _targetCamera.transform.position;
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;
        if (direction.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(-direction);
        }
    }
}
