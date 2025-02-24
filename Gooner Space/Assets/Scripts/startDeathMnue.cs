using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Drawing;

public class startDeathMenu : MonoBehaviour
{
    public Button continueButton;
    public TextMeshProUGUI highscoreText;

    private void Start()
    {
        CheckContinueAvailability();
        highscoreFunction();
    }

    private void highscoreFunction() 
    {
        highscoreText.text = "Current Best Score: " + (GameManager.instance.highscore).ToString("F0");
    }

    public void newGame() 
    {
        //Remove when test is done
        TimeTracker.instance.StartTime(Time.time.ToString());

        GameManager.instance.firstTimePlaying = false;

        PlayerStats.instance.ResetStats();

        MiningSystem.instance.ResetMinedMinerals();

        GameManager.instance.playerHasMovedDummy = false;

        SaveSystem.SaveGame();

        SceneManager.LoadScene(1);

    }
    public void continueGame() // start
    {
        if (File.Exists(Application.persistentDataPath + "/savefile.json") && !GameManager.instance.isGameOver && GameManager.instance.firstTimePlaying == false)
        {
            if (GameManager.instance == null) Instantiate(Resources.Load("GameManager"));
            if (PlayerStats.instance == null) Instantiate(Resources.Load("PlayerStats"));
            if (MiningSystem.instance == null) Instantiate(Resources.Load("MiningSystem"));

            SaveSystem.LoadGame();
            SceneManager.LoadScene(1);
        }
    }

    public void CheckContinueAvailability()
    {
        if (GameManager.instance.isGameOver == true || GameManager.instance.firstTimePlaying == true)
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        }
    }

    public void closeGame() // start death
    {
        SaveSystem.SaveGame();

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void mainMenu() // death
    {
        SceneManager.LoadScene("meyerStart");
    }
}
