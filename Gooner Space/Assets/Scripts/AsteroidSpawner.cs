using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public int numberOfAsteroids = 50;
    public float spawnRadius = 20f; 
    public float innerBufferRadius = 10f; 
    public Transform playerTransform;
    public Sprite[] astroidSprites;
    public int[] mineralPoints = { 20, 100, 150, 50 };

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
                spawnPosition += (Vector2)playerTransform.position;
            }
            while (IsPositionInInnerBuffer(spawnPosition)); 

            GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            float size = Random.Range(0.5f, 2f); 
            newAsteroid.transform.localScale = Vector3.one * size;
            newAsteroid.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

            MineralType selectedMineral = (MineralType)Random.Range(0, System.Enum.GetValues(typeof(MineralType)).Length);

            SpriteRenderer spriteRenderer = newAsteroid.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = astroidSprites[(int)selectedMineral];

            Asteroid asteroidScript = newAsteroid.GetComponent<Asteroid>();
            asteroidScript.points = (int)(size * mineralPoints[(int)selectedMineral]);  
            asteroidScript.mineralType = selectedMineral;  
            asteroidScript.size = size;

            activeAsteroids.Add(newAsteroid);
        }
    }

    void ManageAsteroids()
    {
        foreach (GameObject asteroid in activeAsteroids)
        {
            float distance = Vector2.Distance(asteroid.transform.position, playerTransform.position);

            if (distance > spawnRadius)
            {
                Vector2 newPosition;
                do
                {
                    newPosition = Random.insideUnitCircle * spawnRadius;
                    newPosition += (Vector2)playerTransform.position; 
                }
                while (IsPositionInInnerBuffer(newPosition)); 

                asteroid.transform.position = newPosition;

            }
        }
    }

    bool IsPositionInInnerBuffer(Vector2 position)
    {
        float distance = Vector2.Distance(position, playerTransform.position);
        return distance < innerBufferRadius; 
    }

    void OnDrawGizmos()
    {
        if (playerTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(playerTransform.position, spawnRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerTransform.position, innerBufferRadius);
        }
    }
}
