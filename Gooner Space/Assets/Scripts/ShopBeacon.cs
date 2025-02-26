using UnityEngine;
using UnityEngine.UI;

public class CircularBeacon : MonoBehaviour
{
    public CircleCollider2D shopCollider;
    public RectTransform icon;
    public Transform player;
    public float radius = 150f;
    public float verticalScale = 50f;
    public Camera playerCamera;
    public float smoothSpeed = 5f; 

    private Vector2 smoothedPosition;
    private float smoothedRotation;

    void Start()
    {
        smoothedPosition = icon.anchoredPosition;
        smoothedRotation = icon.rotation.eulerAngles.z;
    }

    void Update()
    {
        if (shopCollider == null || player == null || icon == null || playerCamera == null) return;

        Vector3 closestPoint = shopCollider.ClosestPoint(player.position);
        Vector3 worldDirection = closestPoint - player.position;
        worldDirection.y = 0;

        Vector3 screenPos = playerCamera.WorldToScreenPoint(closestPoint);
        bool isOffScreen = screenPos.z < 0 || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height;

        if (isOffScreen)
        {
            icon.gameObject.SetActive(true);

            float angle = Mathf.Atan2(worldDirection.z, worldDirection.x);

            Vector2 targetPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

            float heightDifference = closestPoint.y - player.position.y;
            targetPosition.y += Mathf.Clamp(heightDifference * verticalScale, -radius, radius);

            smoothedPosition = Vector2.Lerp(smoothedPosition, targetPosition, Time.deltaTime * smoothSpeed);
            icon.anchoredPosition = smoothedPosition;

            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Vector3 directionOnScreen = screenPos - screenCenter;
            float targetRotation = Mathf.Atan2(directionOnScreen.y, directionOnScreen.x) * Mathf.Rad2Deg;

            smoothedRotation = Mathf.LerpAngle(smoothedRotation, targetRotation, Time.deltaTime * smoothSpeed);
            icon.rotation = Quaternion.Euler(0, 0, smoothedRotation);
        }
        else
        {
            icon.gameObject.SetActive(false);
        }
    }
}
