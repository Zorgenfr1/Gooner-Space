using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class shipControl : MonoBehaviour
{
    public TMP_InputField inputX;
    public TMP_InputField inputY;
    public float moveSpeed;
    public float maxVectorLength;

    private Rigidbody2D rb;
    private Vector2 targetVector;
    private Vector2 startPosition;
    private bool isMoving = false;

    public TMP_Text info;
    private bool playerHasMoved;
    public TMP_Text xytext;

    public Button moveButton;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        maxVectorLength = PlayerStats.instance.maxVectorLengthPlayer;
        moveSpeed = PlayerStats.instance.moveSpeedPlayer;

        if (GameManager.instance.playerHasMovedDummy == true)
        {
            xytext.text = "";
        }

        if (moveButton != null)
        {
            moveButton.interactable = true;
        }
    }
    void Update()
    {
        if (PlayerStats.instance.noFuel == true && PlayerStats.instance.emergency == true)
        {
            info.text = "Press E to return to shop";
            if (Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene("FrodeMaster");
                PlayerStats.instance.emergency = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (EventSystem.current.currentSelectedGameObject == inputX.gameObject)
            {
                inputY.Select(); 
            }
            else if (EventSystem.current.currentSelectedGameObject == inputY.gameObject)
            {
                moveButton.Select(); 
            }
            else if (EventSystem.current.currentSelectedGameObject == moveButton.gameObject)
            {
                inputX.Select(); 
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (EventSystem.current.currentSelectedGameObject == moveButton.gameObject)
            {
                moveButton.onClick.Invoke(); 
            }
        }
    }

    public void ApplyVector()
    {
        if (isMoving)
        {
            info.text = "The ship is already moving, please replace me with sound";
            return;
        }

        if (float.TryParse(inputX.text, out float x) && float.TryParse(inputY.text, out float y))
        {
            info.text = "";

            Vector2 inputVector = new Vector2(x, y);

            if (inputVector.magnitude > maxVectorLength)
            {
                inputVector = inputVector.normalized * maxVectorLength;
                info.text = "Input vector exceeded max length";
            }

            else if (inputVector.magnitude <= maxVectorLength)
            {
                targetVector = new Vector2(x, y);
                startPosition = rb.position;
                isMoving = true;

                if (moveButton != null)
                {
                    moveButton.interactable = false;
                }
            }

            if (GameManager.instance.playerHasMovedDummy == false)
            {
                GameManager.instance.playerHasMovedDummy = true;
                xytext.text = "";
            }

            RotatePlayerToFaceDirection(targetVector);
        }
        else
        {
            info.text = "You can only enter numbers";
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            float vectorLength = Mathf.Sqrt(targetVector.x * targetVector.x + targetVector.y * targetVector.y);

            if (maxVectorLength >= vectorLength)
            {
                if (rb.position == startPosition)
                {
                        if (PlayerStats.instance != null)
                        {
                            PlayerStats.instance.AdjustFuel(vectorLength);
                        }
                }

                float distanceTraveled = Vector2.Distance(startPosition, rb.position);

                if (distanceTraveled >= targetVector.magnitude)
                {
                    isMoving = false;
                    rb.linearVelocity = Vector2.zero;

                    if (moveButton != null)
                    {
                        moveButton.interactable = true;
                    }

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

            playerHasMoved = GameManager.instance.playerHasMovedDummy;

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