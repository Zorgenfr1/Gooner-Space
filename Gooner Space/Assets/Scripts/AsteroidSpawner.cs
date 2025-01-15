using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public int numberOfAsteroids = 100;
    public float spawnRadius = 50f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateAsteroids();
    }

    void GenerateAsteroids()
    {
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            Vector2 spawnPosition = Random.insideUnitCircle * spawnRadius;

            GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

            float randomScale = Random.Range(0.5f, 2f);
            newAsteroid.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            newAsteroid.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
