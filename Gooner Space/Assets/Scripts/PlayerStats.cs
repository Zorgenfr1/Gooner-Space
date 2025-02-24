using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int PlayerScore;
    public float PlayerMoney;
    public float RemainingLife;
    public float RemainingFuel;
    public float moveSpeedPlayer = 5f;
    public float maxVectorLengthPlayer = 10f;
    public float maxSize = 20f;
    public float shipCapacity = 20f;
    public bool emergency;
    public bool noFuel;
    public int[] MineralNumbers;

    public float MaxLife = 100f; 
    public float MaxFuel = 100f;

    private Dictionary<(MineralType, float), int> asteroidCollection = new Dictionary<(MineralType, float), int>();

    private void Start()
    {
        RemainingLife = MaxLife;
        RemainingFuel = MaxFuel;
    }
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

    public void AddScore(int points)
    {
        PlayerScore += points;
        UIManager.instance.UpdateScoreUI(PlayerScore);
    }

    public void TakeDamage(float damage)
    {
        RemainingLife -= damage;
        RemainingLife = Mathf.Clamp(RemainingLife, 0, MaxLife);
        UIManager.instance.UpdateLifeUI(RemainingLife);
    }

    public void AdjustFuel(float vectorLength)
    {
        RemainingFuel -= vectorLength;
        RemainingFuel = Mathf.Clamp(RemainingFuel, 0, MaxFuel);
        UIManager.instance.UpdateFuelUI(RemainingFuel);
        if (RemainingFuel < 10)
        {
            noFuel = true;
        }
    }

    public List<MineralEntry> GetAsteroidCollection()
    {
        List<MineralEntry> minerals = new List<MineralEntry>();
        foreach (var entry in asteroidCollection)
        {
            minerals.Add(new MineralEntry(entry.Key.Item1.ToString(), entry.Key.Item2, entry.Value));
        }
        return minerals;
    }

    public void SetData(GameData data)
    {
        PlayerScore = data.playerScore;
        PlayerMoney = data.playerMoney;
        RemainingLife = data.remainingLife;
        RemainingFuel = data.remainingFuelLength;
        MineralNumbers = data.mineralNumbers;

        MaxLife = data.maxLife;
        MaxFuel = data.maxFuel;

        moveSpeedPlayer = data.moveSpeedPlayer;
        maxVectorLengthPlayer = data.maxVectorLengthPlayer;
        maxSize = data.maxSize;
        shipCapacity = data.shipCapacity;

        noFuel = data.noFuel;
        emergency = data.emergency;
    }

    public void ResetStats()
    {
        PlayerScore = 0;
        PlayerMoney = 100;
        MaxLife = 100;
        MaxFuel = 100;
        RemainingLife = MaxLife;
        RemainingFuel = MaxFuel;
        moveSpeedPlayer = 5f;
        maxVectorLengthPlayer = 10f;
        maxSize = 20f;
        shipCapacity = 20f;
        noFuel = false;
        emergency = false;
        MineralNumbers = new int[4]; 
    }
}

[System.Serializable]
public class MineralEntry
{
    public string MineralType;
    public float Size;
    public int Count;

    public MineralEntry(string type, float size, int count)
    {
        MineralType = type;
        Size = size;
        Count = count;
    }
}

