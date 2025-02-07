using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerScore = 0;
    public float playerMoney = 0f;
    public int[] mineralnumbers = { 0, 0, 0, 0 };

    public float currentCapacity;
    public float maxCapacity = 20f;

    public float remainingLife;
    public float fullLife = 100;

    public Image lifeImage;
    public TMP_Text lifePercentage;
    public TMP_Text Pointstext;
    public TMP_Text moneyText;
    public TMP_Text sellMessage;

    public Image fuelImage;
    public TMP_Text fuelPercentage;
    public float maxFuelLength;
    public float remainingFuelLength;

    public Dictionary<string, int> asteroidCollection = new Dictionary<string, int>();

    private Dictionary<string, float> mineralPrices = new Dictionary<string, float>
    {
        {"Iron", 10f},
        {"Gold", 50f},
        {"Diamond", 100f},
        {"Copper", 20f}
    };

    [System.Serializable]
    class GameData
    {
        public int playerScore;
        public float playerMoney;
        public float remainingLife;
        public float remainingFuelLength;
        public int[] mineralnumbers;
        public Dictionary<string, int> asteroidCollection;
    }



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //else if (instance != this) //this does not work and i don't know why 
        //{
          //Destroy(gameObject);  
        //}
    }

    void Start()
    {
        LoadData();
        remainingLife = Mathf.Clamp(remainingLife, 0f, fullLife);
        remainingFuelLength = Mathf.Clamp(remainingFuelLength, 0f, maxFuelLength);
        UpdateUI();
    }

    public void SaveData()
    {
        GameData data = new GameData
        {
            playerScore = playerScore,
            playerMoney = playerMoney,
            remainingLife = remainingLife,
            remainingFuelLength = remainingFuelLength,
            mineralnumbers = mineralnumbers,
            asteroidCollection = asteroidCollection
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);

            playerScore = data.playerScore;
            playerMoney = data.playerMoney;
            remainingLife = data.remainingLife;
            remainingFuelLength = data.remainingFuelLength;
            mineralnumbers = data.mineralnumbers;
            asteroidCollection = data.asteroidCollection;

        }
        else
        {
            Debug.Log("No save file found, using default values.");
        }
    }


    public void OnApplicationQuit()
    {
        SaveData();
    }

    public void ChangeScene(string sceneName)
    {
        SaveData();
        SceneManager.LoadScene(sceneName);
    }

    public void FuelLogic(float vectorLength)
    {
        remainingFuelLength -= vectorLength;
        if (remainingFuelLength < 0)
        {
            remainingFuelLength = 0;
        }

        fuelImage.fillAmount = remainingFuelLength / maxFuelLength;
        fuelPercentage.text = (remainingFuelLength / maxFuelLength * 100).ToString("F2") + "%";

        SaveData();
    }
        

    public void AddPoints(int points)
    {
        if (currentCapacity < maxCapacity) 
        {
            playerScore += points;
            UpdateUI();
        }
    }

    public enum MineralType
    {
        Iron,
        Gold,
        Diamond,
        Copper
    }

    public void SetAsteroidType(MineralType mineralType)
    {
        switch (mineralType)
        {
            case MineralType.Iron:
                mineralnumbers[0] += 1;
                break;
            case MineralType.Gold:
                mineralnumbers[1] += 1;
                break;
            case MineralType.Diamond:
                mineralnumbers[2] += 1;
                break;
            case MineralType.Copper:
                mineralnumbers[3] += 1;
                break;
        }
    }

    public void ControlCapacity(float size)
    {
        if (currentCapacity + size <= maxCapacity) 
        {
            currentCapacity += size;
        }
    }

    public void MineHit(int points, float size, float damage)
    {
        if (currentCapacity + size <= maxCapacity)
        {
            currentCapacity += size;
        }

        playerScore += points;

        for (int i = 0; i < mineralnumbers.Length; i++)
        {
            if (mineralnumbers[i] > 2)
            {
                mineralnumbers[i] -= 2;
            }
        }

        remainingLife -= damage;
        if (remainingLife < 0) remainingLife = 0;

        UpdateUI();
    }

    public float CalculatePrice(string mineralType, float size)
    {
        if (mineralPrices.ContainsKey(mineralType))
        {
            return mineralPrices[mineralType] * size;
        }
        return 0f;
    }

    public void SellMineral(string mineralType, float size)
    {
        int mineralIndex = GetMineralIndex(mineralType);
        if (mineralIndex != -1 && mineralnumbers[mineralIndex] > 0)
        {
            float price = CalculatePrice(mineralType, size);

            mineralnumbers[mineralIndex] -= 1;
            playerMoney += price;

            if (sellMessage != null)
            {
                sellMessage.text = $"Sold {mineralType} for {price} money!";
            }

            UpdateUI();
        }
        else
        {
            if (sellMessage != null)
            {
                sellMessage.text = "Not enough minerals to sell!";
            }
        }
    }

    private int GetMineralIndex(string mineralType)
    {
        switch (mineralType)
        {
            case "Iron":
                return 0;
            case "Gold":
                return 1;
            case "Diamond":
                return 2;
            case "Copper":
                return 3;
            default:
                return -1;
        }
    }

    private void UpdateUI()
    {
        if (Pointstext != null)
        {
            Pointstext.text = "POINTS: " + playerScore;
        }

        if (moneyText != null)
        {
            moneyText.text = "Money: " + playerMoney.ToString("F2") + "$";  
        }

        if (lifeImage != null)
        {
            lifeImage.fillAmount = remainingLife / fullLife;
        }

        if (lifePercentage != null)
        {
            lifePercentage.text = (remainingLife / fullLife * 100).ToString("F2") + "%";
        }

        if (fuelPercentage != null)
        {
            fuelPercentage.text = (remainingFuelLength / maxFuelLength * 100).ToString("F2") + "%";
        }

        if (fuelImage != null)
        {
            fuelImage.fillAmount = remainingFuelLength / maxFuelLength;
        }

    }
}
