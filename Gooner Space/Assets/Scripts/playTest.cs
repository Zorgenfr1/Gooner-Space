using UnityEngine;
using System.IO;

public static class PlaytestTimerSystem
{
    private static string savePath = Application.persistentDataPath + "/playtestTimer.csv";
    private static float elapsedTime;
    private static bool isTimerRunning;

    private static int playerScore;
    private static float playerMoney;
    private static float remainingLife;
    private static float remainingFuel;
    private static string deathCause = "";

    static PlaytestTimerSystem()
    {
        if (File.Exists(savePath))
        {
            string[] lines = File.ReadAllLines(savePath);
            if (lines.Length > 0)
            {
                string[] data = lines[0].Split(',');
                elapsedTime = float.Parse(data[0]);
                isTimerRunning = bool.Parse(data[1]);
                playerScore = int.Parse(data[2]);
                playerMoney = float.Parse(data[3]);
                remainingLife = float.Parse(data[4]);
                remainingFuel = float.Parse(data[5]);
                deathCause = data[6];
            }
        }
        else
        {
            elapsedTime = 0f;
            isTimerRunning = false;
            playerScore = 0;
            playerMoney = 0f;
            remainingLife = 100f; 
            remainingFuel = 100f; 
            deathCause = "";
        }
    }


    public static void StartTimer()
    {
        isTimerRunning = true;
    }

    public static void StopTimer()
    {
        isTimerRunning = false;
    }

    public static void ResetTimer()
    {
        elapsedTime = 0f;
    }

    public static void UpdateTimer()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime; 
        }
    }

    public static void SaveTimer()
    {
        playerScore = PlayerStats.instance.PlayerScore;
        playerMoney = PlayerStats.instance.PlayerMoney;
        remainingLife = PlayerStats.instance.RemainingLife;
        remainingFuel = PlayerStats.instance.RemainingFuel;

        string data = $"{elapsedTime},{isTimerRunning},{playerScore},{playerMoney},{remainingLife},{remainingFuel},{deathCause}"; // Save everything in CSV format
        File.WriteAllText(savePath, data);
    }

    public static void LoadTimer()
    {
        if (File.Exists(savePath))
        {
            string[] lines = File.ReadAllLines(savePath);
            if (lines.Length > 0)
            {
                string[] data = lines[0].Split(',');
                elapsedTime = float.Parse(data[0]);
                isTimerRunning = bool.Parse(data[1]);
                playerScore = int.Parse(data[2]);
                playerMoney = float.Parse(data[3]);
                remainingLife = float.Parse(data[4]);
                remainingFuel = float.Parse(data[5]);
                deathCause = data[6];
            }
        }
    }

    public static void SetDeathCause(string cause)
    {
        deathCause = cause;
    }

    public static string GetFormattedTime()
    {
        return Mathf.FloorToInt(elapsedTime).ToString();
    }


    public static string GetDeathCause()
    {
        return deathCause;
    }
}

