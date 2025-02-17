using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
    }

    public void ChangeScene(string sceneName)
    {
        SaveData();
        SceneManager.LoadScene(sceneName);
    }

    public void OnApplicationQuit()
    {
        SaveData();
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
}
