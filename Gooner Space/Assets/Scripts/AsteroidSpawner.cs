using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public int numberOfAsteroids = 50;
    public float spawnRadius = 20f; // Outer spawn radius
    public float innerBufferRadius = 10f; // Inner buffer zone where no asteroids are spawned
    public Transform playerTransform;
    public List<string> mineralTypes = new List<string> { "Iron", "Gold", "Diamond", "Copper" };
    public Sprite[] astroidSprites;
    public int[] mineralPoints = { 20, 100, 150, 50 };

    public string selectedMineral;

    private List<GameObject> activeAsteroids = new List<GameObject>();

    void Start()
    {
        GenerateAsteroidsAroundPlayer();
    }

    void Update()
    {
        ManageAsteroids();
    }

   
    void GenerateAsteroidsAroundPlayer()
    {
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            Vector2 spawnPosition;
            do
            {
                spawnPosition = Random.insideUnitCircle * spawnRadius;
                spawnPosition += (Vector2)playerTransform.position; // Offset by player's position
            }
            while (IsPositionInInnerBuffer(spawnPosition)); // Ensure it's outside the inner buffer

            GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            float size = Random.Range(0.5f, 2f); // Random scale
            newAsteroid.transform.localScale = Vector3.one * size;
            newAsteroid.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

            SpriteRenderer spriteRenderer = newAsteroid.GetComponent<SpriteRenderer>();

            int randomIndex = Random.Range(0, mineralTypes.Count);
            selectedMineral = mineralTypes[randomIndex];
            spriteRenderer.sprite = astroidSprites[randomIndex];

            asteroid asteroidScript = newAsteroid.GetComponent<asteroid>();
            newAsteroid.GetComponent<asteroid>().points = (int)(size * mineralPoints[randomIndex]);

            activeAsteroids.Add(newAsteroid);
        }
    }

    void ManageAsteroids()
    {
        foreach (GameObject asteroid in activeAsteroids)
        {
            float distance = Vector2.Distance(asteroid.transform.position, playerTransform.position);

            // If the asteroid is outside the spawn radius, relocate it
            if (distance > spawnRadius)
            {
                Vector2 newPosition;
                do
                {
                    newPosition = Random.insideUnitCircle * spawnRadius;
                    newPosition += (Vector2)playerTransform.position; // Offset by player's position
                }
                while (IsPositionInInnerBuffer(newPosition)); // Ensure it's outside the inner buffer

                asteroid.transform.position = newPosition;

            }
        }
    }

    bool IsPositionInInnerBuffer(Vector2 position)
    {
        float distance = Vector2.Distance(position, playerTransform.position);
        return distance < innerBufferRadius; // True if within the inner buffer
    }

    void OnDrawGizmos()
    {
        if (playerTransform != null)
        {
            // Draw the outer spawn radius
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(playerTransform.position, spawnRadius);

            // Draw the inner buffer radius
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerTransform.position, innerBufferRadius);
        }
    }
}
