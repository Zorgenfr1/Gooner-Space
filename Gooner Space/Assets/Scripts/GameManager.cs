using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerScore = 0;

    public void AddPoints(int points)
    {
        playerScore += points;
        Debug.Log("Player's current score: " + playerScore);

    }
}
