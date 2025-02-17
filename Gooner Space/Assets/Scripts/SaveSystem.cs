using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

public static class SaveSystem
{
    private static string savePath = Application.persistentDataPath + "/savefile.json";

    public static void SaveGame()
    {
        GameData data = new GameData(PlayerStats.instance);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public static void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            data.ApplyData();
        }
        else
        {
            Debug.Log("No save file found, using default values.");
        }
    }
}

[System.Serializable]
public class GameData
{
    public int playerScore;
    public float playerMoney;
    public float remainingLife;
    public float remainingFuelLength;
    public int[] mineralNumbers;
    public float maxLife; 
    public float maxFuel;
    public List<MineralEntry> asteroidCollection;

    public GameData(PlayerStats stats)
    {
        playerScore = stats.PlayerScore;
        playerMoney = stats.PlayerMoney;
        remainingLife = stats.RemainingLife;
        remainingFuelLength = stats.RemainingFuel;
        mineralNumbers = stats.MineralNumbers;
        asteroidCollection = stats.GetAsteroidCollection();
        maxLife = stats.MaxLife; 
        maxFuel = stats.MaxFuel;
    }

    public void ApplyData()
    {
        PlayerStats.instance.SetData(this);
    }
}

