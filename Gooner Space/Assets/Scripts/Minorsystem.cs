using UnityEngine;
using System.Collections.Generic;
using System;

public class MiningSystem : MonoBehaviour
{
    public static MiningSystem instance;

    private Dictionary<(MineralType, float), int> minedMinerals = new Dictionary<(MineralType, float), int>();

    private Dictionary<MineralType, float> mineralPrices = new Dictionary<MineralType, float>
    {
        {MineralType.Iron, 10f},
        {MineralType.Gold, 50f},
        {MineralType.Diamond, 100f},
        {MineralType.Copper, 20f}
    };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);  
        }
    }

    public void SetMinedMinerals(List<MinedMineralEntry> savedMinerals)
    {
        minedMinerals.Clear();
        foreach (var entry in savedMinerals)
        {
            var key = (Enum.Parse<MineralType>(entry.MineralType), entry.Size);
            minedMinerals[key] = entry.Count;
        }
    }

    public float CalculatePrice(MineralType mineralType, float size)
    {
        if (mineralPrices.ContainsKey(mineralType))
        {
            return mineralPrices[mineralType] * size;
        }
        return 0f;
    }

    public void AddMineral(MineralType mineralType, float size)
    {
        var key = (mineralType, size);

        if (minedMinerals.ContainsKey(key))
        {
            minedMinerals[key]++;
        }
        else
        {
            minedMinerals[key] = 1;
        }
    }

    public Dictionary<(MineralType, float), int> GetMinedMinerals()
    {
        return minedMinerals;
    }

    public void DebugMinerals()
    {
        foreach (var entry in minedMinerals)
        {
            string mineralInfo = $"{entry.Key.Item1} (Size: {entry.Key.Item2}): {entry.Value} pieces";
            Debug.Log(mineralInfo);
        }
    }

    public void SellMineral(MineralType mineralType, float size)
    {
        var key = (mineralType, size);

        if (minedMinerals.ContainsKey(key))
        {
            int count = minedMinerals[key];

            float pricePerUnit = CalculatePrice(mineralType, size);
            float earnings = pricePerUnit * count;
            TimeTracker.instance.Money(earnings);
            TimeTracker.instance.ComputerTime(Time.time.ToString());

            PlayerStats.instance.PlayerMoney += earnings;
            PlayerStats.instance.shipCapacity += size;
            UIManager.instance.UpdateMoneyUI(PlayerStats.instance.PlayerMoney);

            minedMinerals.Remove(key); 
        }
    }

    public void ResetMinedMinerals()
    {
        minedMinerals.Clear();
    }
}

public enum MineralType
{
    Iron,
    Gold,
    Diamond,
    Copper
}