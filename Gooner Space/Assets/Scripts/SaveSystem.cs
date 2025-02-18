using UnityEngine;
using System.IO;
using System.Collections.Generic;

public static class SaveSystem
{
    private static string savePath = Application.persistentDataPath + "/savefile.json";

    public static void SaveGame()
    {
        GameData data = new GameData(PlayerStats.instance, MiningSystem.instance, GameManager.instance);
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

    public float moveSpeedPlayer;
    public float maxVectorLengthPlayer;
    public float maxSize;
    public float shipCapacity;

    public bool noFuel;
    public bool emergency;
    public bool playerHasMovedDummy;

    public List<MineralEntry> asteroidCollection;
    public List<MinedMineralEntry> minedMinerals;

    public GameData(PlayerStats stats, MiningSystem miningSystem, GameManager gameManager)
    {
        playerScore = stats.PlayerScore;
        playerMoney = stats.PlayerMoney;
        remainingLife = stats.RemainingLife;
        remainingFuelLength = stats.RemainingFuel;
        mineralNumbers = stats.MineralNumbers;

        maxLife = stats.MaxLife;
        maxFuel = stats.MaxFuel;

        moveSpeedPlayer = stats.moveSpeedPlayer;
        maxVectorLengthPlayer = stats.maxVectorLengthPlayer;
        maxSize = stats.maxSize;
        shipCapacity = stats.shipCapacity;

        noFuel = stats.noFuel;
        emergency = stats.emergency;
        playerHasMovedDummy = gameManager.playerHasMovedDummy;

        asteroidCollection = stats.GetAsteroidCollection();

        minedMinerals = new List<MinedMineralEntry>();
        foreach (var entry in miningSystem.GetMinedMinerals())
        {
            minedMinerals.Add(new MinedMineralEntry(entry.Key.Item1.ToString(), entry.Key.Item2, entry.Value));
        }
    }

    public void ApplyData()
    {
        PlayerStats.instance.SetData(this);
        MiningSystem.instance.SetMinedMinerals(minedMinerals);
        GameManager.instance.playerHasMovedDummy = playerHasMovedDummy;
    }
}


[System.Serializable]
public class MinedMineralEntry
{
    public string MineralType;
    public float Size;
    public int Count;

    public MinedMineralEntry(string type, float size, int count)
    {
        MineralType = type;
        Size = size;
        Count = count;
    }
}


