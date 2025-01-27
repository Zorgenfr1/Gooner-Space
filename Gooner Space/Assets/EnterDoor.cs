using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnterDoor : MonoBehaviour
{
    private bool enterAllowed;
    private string sceneToLoad;
    public GameObject ShopPlanet;
    public TMP_Text shopPlanetText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == ShopPlanet)
        {
            shopPlanetText.text = "Press E to enter the shop";
            sceneToLoad = "FrodeMaster";
            enterAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == ShopPlanet)
        {
            shopPlanetText.text = "";
            enterAllowed = false;
        }
    }

    private void Update()
    {
        if (enterAllowed && Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}