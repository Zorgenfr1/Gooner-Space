using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    public int points = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with asteroid!");

            GameManager.instance.AddPoints(points);

            AsteroidSpawner.ManageAsteroids();
            //Move this astroid
            //Destroy(gameObject);
        }
    }
}
