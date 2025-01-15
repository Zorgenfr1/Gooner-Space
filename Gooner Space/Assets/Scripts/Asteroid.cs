using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    public List<string> mineralTypes = new List<string> { "Iron", "Gold", "Diamond", "Copper" };
    public string selectedMineral;

    public int basePoints;
    public int sizePoints;
    public int totalPoints;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        int randomIndex = Random.Range(0, mineralTypes.Count);
        selectedMineral = mineralTypes[randomIndex];

        basePoints = GetPointsForMineral(selectedMineral);

        float size = transform.localScale.x;
        sizePoints = Mathf.RoundToInt(size * 10);

        totalPoints = basePoints + sizePoints;

        Renderer renderer = GetComponent<Renderer>();
        switch (selectedMineral)
        {
            case "Iron":
                renderer.material.color = Color.grey;
                break;

            case "Gold":
                renderer.material.color = Color.yellow;
                break;

            case "Diamond":
                renderer.material.color = Color.blue;
                break;

            case "Copper":
                renderer.material.color = Color.red;
                break;
        }

    }

    int GetPointsForMineral(string mineral)
    {
        switch (mineral)
        {
            default: // "Iron"
                return 20;
            case "Gold":
                return 100;
            case "Diamond":
                return 150;
            case "Copper":
                return 50;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (gameManager != null)
            {
                gameManager.AddPoints(totalPoints);
            }

            Destroy(gameObject);

        }
    }
}
