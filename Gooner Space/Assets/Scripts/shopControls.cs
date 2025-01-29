using UnityEngine;

public class shopControls : MonoBehaviour
{
    public float scrollSpeed = 20f;
    public float edgeThreshold = 100f;
    public float minX = -10f, maxX = 10f;

    void Update()
    {
        float mouseX = Input.mousePosition.x;
        float screenWidth = Screen.width;
        float moveDirection = 0f;

        if (mouseX < edgeThreshold)
        {
            moveDirection = -1f;
        }
        else if (mouseX > screenWidth - edgeThreshold)
        {
            moveDirection = 1f;
        }

        Vector3 newPosition = transform.position + Vector3.right * moveDirection * scrollSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        transform.position = newPosition;
    }
}
