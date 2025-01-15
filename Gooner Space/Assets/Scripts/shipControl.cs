using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class shipControl : MonoBehaviour
{
    public TMP_InputField inputX;
    public TMP_InputField inputY;
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyVector()
    {
        float x = float.Parse(inputX.text);
        float y = float.Parse(inputY.text);

        movement = new Vector2(x, y);
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
