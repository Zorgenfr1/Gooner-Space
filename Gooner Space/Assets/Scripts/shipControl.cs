using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class shipControl : MonoBehaviour
{
    public TMP_InputField inputX;
    public TMP_InputField inputY;
    public float moveSpeed = 5f;
    public float maxVectorLength = 10f;

    private Rigidbody2D rb;
    private Vector2 targetVector;
    private Vector2 startPosition;
    private bool isMoving = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyVector()
    {
        if (float.TryParse(inputX.text, out float x) && float.TryParse(inputY.text, out float y))
        {
            Vector2 inputVector = new Vector2(x, y);

            if (inputVector.magnitude > maxVectorLength)
            {
                inputVector = inputVector.normalized * maxVectorLength; 
                Debug.Log($"Input vector exceeded max length. Clamped to: {inputVector}");
            }
            targetVector = new Vector2(x, y);
            startPosition = rb.position;
            isMoving = true;

            RotatePlayerToFaceDirection(targetVector);
        }
        else
        {
            Debug.LogWarning("Invalid input. Please enter numeric values.");
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            float vectorLength = Mathf.Sqrt(targetVector.x * targetVector.x + targetVector.y * targetVector.y);

            if (maxVectorLength >= vectorLength)
            {
                float distanceTraveled = Vector2.Distance(startPosition, rb.position);

                if (distanceTraveled >= targetVector.magnitude)
                {
                    isMoving = false;
                    rb.velocity = Vector2.zero;
                    return;
                }

                Vector2 direction = targetVector.normalized;
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                Debug.LogWarning("Target vector length exceeds the maximum allowed length.");
                isMoving = false;
            }
                
        }
    }
    void RotatePlayerToFaceDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle - 90f;
        }

    }
}
