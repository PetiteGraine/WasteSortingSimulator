using UnityEngine;

public class BillboardCanvas : MonoBehaviour
{
    public Camera targetCamera;

    private void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = targetCamera.transform.position;
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0; // Ignore vertical difference
        if (direction.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(-direction);
        }
    }
}
