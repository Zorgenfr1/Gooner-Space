using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerScore = 0;
    public static GameManager instance;
    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }
    public void AddPoints(int points)
    {
        playerScore += points;
        Debug.Log("Player's current score: " + playerScore);

    }
}
