using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

private Vector2 lastTapPosition;
private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.localEulerAngles;

    }

 
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
          Vector2 currentTapPosition = Input.mousePosition;
        if (lastTapPosition == Vector2.zero)
        {
            lastTapPosition = currentTapPosition;
        }
            float distance = lastTapPosition.x - currentTapPosition.x;
            lastTapPosition = currentTapPosition;
            transform.Rotate(Vector3.up * distance * 0.1f, Space.World);
        }
        if (Input.GetMouseButtonUp(0))
        {
            lastTapPosition = Vector2.zero;
        }
    }
}
