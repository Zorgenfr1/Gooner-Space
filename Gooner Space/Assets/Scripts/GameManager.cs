using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool playerHasMovedDummy = false;
    public bool isGameOver = false;
    public bool firstTimePlaying = true;
    public float highscore = 0f;

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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateUI();
        SaveData();
    }

    public void ChangeScene(string sceneName)
    {
        SaveData();
        SceneManager.LoadScene(sceneName);
    }

    public void OnApplicationQuit()
    {
        SaveSystem.SaveGame();
    }

    public void SaveData()
    {
        SaveSystem.SaveGame();
    }

    public void LoadData()
    {
        SaveSystem.LoadGame();
    }


    private void UpdateUI()
    {
        UIManager.instance.UpdateScoreUI(PlayerStats.instance.PlayerScore);
        UIManager.instance.UpdateMoneyUI(PlayerStats.instance.PlayerMoney);
        UIManager.instance.UpdateLifeUI(PlayerStats.instance.RemainingLife);
        UIManager.instance.UpdateFuelUI(PlayerStats.instance.RemainingFuel);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            MiningSystem.instance.DebugMinerals();
        }
    }

    void FixedUpdate()
    {

        if (!isGameOver)
        {
            if (!PlayerStats.instance.emergency && PlayerStats.instance.RemainingFuel <= 1)
            {
                isGameOver = true; 
                if(PlayerStats.instance.PlayerScore > highscore)
                {
                    highscore = PlayerStats.instance.PlayerScore;
                }

                GameObject player = GameObject.FindGameObjectWithTag("Player");

                PlayerExplosion playerExplosion = player.GetComponent<PlayerExplosion>();
                if (playerExplosion != null)
                {
                    playerExplosion.TriggerDeath();
                }


                Invoke("LoadGameOverScene", 2f);
            }

            else if (PlayerStats.instance.RemainingLife <= 0)
            {
                isGameOver = true;
                if (PlayerStats.instance.PlayerScore > highscore)
                {
                    highscore = PlayerStats.instance.PlayerScore;
                }

                GameObject player = GameObject.FindGameObjectWithTag("Player");

                PlayerExplosion playerExplosion = player.GetComponent<PlayerExplosion>();
                if (playerExplosion != null)
                {
                    playerExplosion.TriggerDeath(); 
                }


                Invoke("LoadGameOverScene", 2f);
            }
        }
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene("MeyerDeath");
    }

}
