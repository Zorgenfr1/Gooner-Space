using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class exitShop : MonoBehaviour
{
    public TMP_Text clickToExit;
    private bool isMouseOver = false;

    void Start()
    {
        clickToExit.gameObject.SetActive(false);
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (GetComponent<Collider2D>().OverlapPoint(mousePos))
        {
            if (!isMouseOver)
            {
                isMouseOver = true;
                clickToExit.gameObject.SetActive(true);
            }
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            if (isMouseOver)
            {
                isMouseOver = false;

                clickToExit.gameObject.SetActive(false);
            }
        }
    }
}
