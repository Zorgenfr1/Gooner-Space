using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public TMP_Text numbers;
    public TMP_Text lengthText;
    public TMP_Text info;

    public TMP_Text xytext;
    private bool isFirstInput = true;

    public Image fuelImage;
    public TMP_Text fuelPercentage;
    public float maxFuelLength;
    public float remainingFuelLength;
    private bool logDistance = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        remainingFuelLength = maxFuelLength;
        numbers.text = "";
        lengthText.text = "";
        info.text = "";
    }

    public void ApplyVector()
    {
        if (float.TryParse(inputX.text, out float x) && float.TryParse(inputY.text, out float y))
        {
            numbers.text = "";
            lengthText.text = "";
            info.text = "";

            Vector2 inputVector = new Vector2(x, y);

            if (inputVector.magnitude > maxVectorLength)
            {
                inputVector = inputVector.normalized * maxVectorLength;
                lengthText.text = "Input vector exceeded max length";
                info.text = "Input vector exceeded max length";
            }
            targetVector = new Vector2(x, y);
            startPosition = rb.position;
            isMoving = true;
            logDistance = true;

            else if (inputVector.magnitude <= maxVectorLength)
            {
                targetVector = new Vector2(x, y);
                startPosition = rb.position;
                isMoving = true;
                logDistance = true;
            }

            if (isFirstInput)
            {
                xytext.text = "";
                isFirstInput = false;
            }

            RotatePlayerToFaceDirection(targetVector);
        }
        else
        {
            numbers.text = "You can only enter numbers";
            info.text = "You can only enter numbers";
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            float vectorLength = Mathf.Sqrt(targetVector.x * targetVector.x + targetVector.y * targetVector.y);

            if (logDistance == true)
            {
                logDistance = false;
                UpdateUi(vectorLength);
            }

            if (maxVectorLength >= vectorLength)
            {
                float distanceTraveled = Vector2.Distance(startPosition, rb.position);

                if (distanceTraveled >= targetVector.magnitude)
                {
                    isMoving = false;
                    rb.linearVelocity = Vector2.zero;
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

    void UpdateUi(float vectorLength)
    {
        remainingFuelLength -= vectorLength;
        fuelImage.fillAmount = remainingFuelLength / maxFuelLength;
        fuelPercentage.text = (remainingFuelLength / maxFuelLength * 100).ToString("F2") + "%";

    }
}