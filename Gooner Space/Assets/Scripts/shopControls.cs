using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class shopControls : MonoBehaviour
{
    public float scrollSpeed = 20f;
    public float edgeThreshold = 100f;
    public float minX = -10f, maxX = 10f;

    private bool hasHitLeftEdge = false;  
    private bool hasHitRightEdge = false;

    public Image exitArrow;
    public Image shopArrow;

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

        if (newPosition.x == minX && !hasHitLeftEdge)
        {
            hasHitLeftEdge = true;
            exitArrow.gameObject.SetActive(false);
        }
        else if (newPosition.x == maxX && !hasHitRightEdge)
        {
            hasHitRightEdge = true;
            shopArrow.gameObject.SetActive(false);
        }

        transform.position = newPosition;

        if (newPosition.x > minX && hasHitLeftEdge)
        {
            hasHitLeftEdge = false;
            exitArrow.gameObject.SetActive(true);
        }

        if (newPosition.x < maxX && hasHitRightEdge)
        {
            hasHitRightEdge = false;
            shopArrow.gameObject.SetActive(true);
        }
    }
}
