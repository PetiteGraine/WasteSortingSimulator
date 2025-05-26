using UnityEngine;

public class RotateArrowTowardWaste : MonoBehaviour
{
    [SerializeField] private GameObject _waste;
    [SerializeField] private Color _packagingColor;
    [SerializeField] private Color _glassColor;
    [SerializeField] private Color _foodColor;

    private void LateUpdate()
    {
        if (_waste == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = _waste.transform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion offset = Quaternion.Euler(90f, -90f, 0f);
            transform.rotation = targetRotation * offset;
        }
    }

    public void SetWaste(GameObject waste)
    {
        _waste = waste;
        if (_waste.tag == "Untagged")
        {
            _waste.tag = "Waste";
        }

        switch (GetWasteType(waste))
        {
            case "Food":
                GetComponent<SpriteRenderer>().color = _foodColor;
                break;
            case "Packaging":
                GetComponent<SpriteRenderer>().color = _packagingColor;
                break;
            case "Glass":
                GetComponent<SpriteRenderer>().color = _glassColor;
                break;
            default:
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
        }
    }

    private string GetWasteType(GameObject waste)
    {
        if (waste == null) return "No Waste Set";
        foreach (Transform child in waste.transform)
        {
            if (child.tag != "Untagged")
            {
                _waste = child.gameObject;
                return child.tag;
            }
        }
        return "Unknown Waste Type";
    }
}
