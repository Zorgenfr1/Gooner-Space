using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public int numberOfAsteroids = 100;
    public float spawnRadius = 50f; // Outer spawn radius
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

            newAsteroid.GetComponent<asteroid>().points = (int)(size * mineralPoints[randomIndex]);

            activeAsteroids.Add(newAsteroid);
        }
    }

    void ManageAsteroids()
    {
        // Destroy asteroids outside the outer spawn radius
        for (int i = activeAsteroids.Count - 1; i >= 0; i--)
        {
            GameObject asteroid = activeAsteroids[i];
            float distance = Vector2.Distance(asteroid.transform.position, playerTransform.position);
            if (distance > spawnRadius)
            {
                Destroy(asteroid);
                activeAsteroids.RemoveAt(i);
            }
        }

        // Spawn new asteroids to maintain the target number, ensuring they are outside the inner buffer
        while (activeAsteroids.Count < numberOfAsteroids)
        {
            Vector2 spawnPosition;
            do
            {
                spawnPosition = Random.insideUnitCircle * spawnRadius;
                spawnPosition += (Vector2)playerTransform.position; // Offset by player's position
            }
            while (IsPositionInInnerBuffer(spawnPosition)); // Ensure it's outside the inner buffer

            GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            newAsteroid.transform.localScale = Vector3.one * Random.Range(0.5f, 2f); // Random scale
            newAsteroid.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

            activeAsteroids.Add(newAsteroid);
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
