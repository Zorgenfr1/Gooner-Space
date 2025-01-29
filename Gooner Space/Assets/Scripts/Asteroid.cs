using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    public string mineralType;
    public float size;
    public int points = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision detected with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player collided with asteroid!");

            GameManager.instance.AddPoints(points);
            GameManager.instance.SetAsteroidType(mineralType);
            GameManager.instance.ControlCapacity(size);

            Vector2 newPosition = new Vector2(Random.Range(-50f, 50f), Random.Range(-50f, 50f));
            transform.position = newPosition;

            //AsteroidSpawner.ManageAsteroids();
            //Move this astroid
            //Destroy(gameObject);
        }
    }
}
