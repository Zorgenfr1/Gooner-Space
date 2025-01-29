using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    public enum AsteroidSize { Small, Medium, Large }

    public string mineralType;
    public AsteroidSize sizeCategory;
    public int points = 0;

    public void Initialize(string mineral, float size, int basePoints)
    {
        mineralType = mineral;
        points = (int)(size * basePoints);

        if (size < 0.8f) sizeCategory = AsteroidSize.Small;
        else if (size < 1.5f) sizeCategory = AsteroidSize.Medium;
        else sizeCategory = AsteroidSize.Large;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision detected with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player collided with asteroid!");

            GameManager.instance.AddPoints(points);

            Vector2 newPosition = new Vector2(Random.Range(-50f, 50f), Random.Range(-50f, 50f));
            transform.position = newPosition;

            //AsteroidSpawner.ManageAsteroids();
            //Move this astroid
            //Destroy(gameObject);
        }
    }
}
