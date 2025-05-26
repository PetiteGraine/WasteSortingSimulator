using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _canvasToToggle;

    private XRGrabInteractable _grabInteractable;

    private void Awake()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();

        _grabInteractable.selectEntered.AddListener(OnGrab);
        _grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (_canvasToToggle != null)
            _canvasToToggle.SetActive(true);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (_canvasToToggle != null)
            _canvasToToggle.SetActive(false);
    }

    private void OnDestroy()
    {
        _grabInteractable.selectEntered.RemoveListener(OnGrab);
        _grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}
