using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Drawing;


public class GameManager : MonoBehaviour
{
    public int playerScore = 0;
    public static GameManager instance;
    public int[] mineralnumbers = { 0, 0, 0, 0 };

    public float currentCapacity;
    public float maxCapacity = 20f;

    public float remainingLife;
    public float fullLife = 100;
    public Image lifeImage;
    public TMP_Text lifePercentage;

    public TMP_Text Pointstext;
    void Start()
    {
        remainingLife = fullLife;
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }
    public void AddPoints(int points)
    {
        while (currentCapacity < maxCapacity)
        {
            playerScore += points;
            //Debug.Log("Player's current score: " + playerScore);
            Pointstext.text = "POINTS: " + playerScore;
        }
    }
    public void SetAsteroidType(string mineralType)
    {
        while (currentCapacity < maxCapacity)
        {
            if (mineralType == "Iron")
            {
                mineralnumbers[0] += 1;
            }
            if (mineralType == "Gold")
            {
                mineralnumbers[1] += 1;
            }
            if (mineralType == "Diamond")
            {
                mineralnumbers[2] += 1;
            }
            if (mineralType == "Copper")
            {
                mineralnumbers[3] += 1;
            }
        }
    }

    public void ControlCapacity(float size)
    {
        while (currentCapacity < maxCapacity) { 
        currentCapacity += size;
        }
    }

    public void MineHit(int points, float size, float damage)
    {
        currentCapacity += size;
        playerScore += points;
        Pointstext.text = "POINTS: " + playerScore;

        for (int i = 0; i < mineralnumbers.Length; i++)
        {
            if (mineralnumbers[i] > 2)
            {
                mineralnumbers[i] -= 2;
            }
        }

        remainingLife -= damage;
        lifeImage.fillAmount = remainingLife / fullLife;
        lifePercentage.text = (remainingLife / fullLife * 100).ToString("F2") + "%";
    }
}
