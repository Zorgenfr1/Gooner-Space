using UnityEngine;

public class TimeTracker : MonoBehaviour
{
    string[] data = { "", "", "", "", "", "" };
    float totalMoney = 0;
    string moneyAsString;



    public static TimeTracker instance; // Singleton instance

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this); // Ensure only one instance exists
        }
    }


    // Update is called once per frame
    public void StartTime(string startTime)
    {
        data[0] = startTime;
    }
    public void HighScore(string highscore)
    {
        data[1] = highscore;
    }

    public void Money(float money)
    {
        totalMoney += money;

        moneyAsString = totalMoney.ToString();

        data[2] = moneyAsString;
    }

    public void ComputerTime(string computerTime)
    {
        data[3] = computerTime;
    }
    public void GameTime(string gameTime)
    {
        data[4] = gameTime;
    }

    public void DeathWay(string deathWay)
    {
        data[5] = deathWay;
    }
}
