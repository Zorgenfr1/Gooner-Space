using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TMP_Text scoreText;
    public TMP_Text moneyText;
    public TMP_Text messageText;
    public TMP_Text sizeText;
    public TMP_Text fuelText;
    public TMP_Text lifeText;
    public UnityEngine.UI.Image fuelImage;
    public UnityEngine.UI.Image lifeImage;

    public GameObject gameMenu;
    private bool isMenuOpen = false;

    AudioManager audioManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance.gameObject);
            instance = this;  
            DontDestroyOnLoad(gameObject);
            UpdateScoreUI(PlayerStats.instance.PlayerScore);
            UpdateMoneyUI(PlayerStats.instance.PlayerMoney);
            UpdateLifeUI(PlayerStats.instance.RemainingLife);
            UpdateFuelUI(PlayerStats.instance.RemainingFuel);
            UpdateCargoUI(PlayerStats.instance.maxSize, PlayerStats.instance.shipCapacity);
        }
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        UpdateScoreUI(PlayerStats.instance.PlayerScore);
        UpdateMoneyUI(PlayerStats.instance.PlayerMoney);
        UpdateLifeUI(PlayerStats.instance.RemainingLife);
        UpdateFuelUI(PlayerStats.instance.RemainingFuel);
        UpdateCargoUI(PlayerStats.instance.maxSize, PlayerStats.instance.shipCapacity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void UpdateScoreUI(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateMoneyUI(float money)
    {
        moneyText.text = "Money: $" + money.ToString("F2");
    }
    public void UpdateCargoUI(float size, float maxSize)
    {
        size = PlayerStats.instance.shipCapacity;
        maxSize = PlayerStats.instance.maxSize;
        sizeText.text = "Cargo: " + (maxSize-size).ToString("F1")+" Ton/"+ maxSize.ToString("F1")+" Ton";
    }

    public void UpdateFuelUI(float fuel)
    {
        fuelImage.fillAmount = fuel / PlayerStats.instance.MaxFuel;
        fuelText.text = (fuel / PlayerStats.instance.MaxFuel * 100).ToString("F2") + "%";
    }

    public void UpdateLifeUI(float life)
    {
        lifeImage.fillAmount = life / PlayerStats.instance.MaxLife;
        lifeText.text = (life / PlayerStats.instance.MaxLife * 100).ToString("F2") + "%";
    }

    public void ShowMessage(string message){
	    messageText.text = message;
	    messageText.gameObject.SetActive(true);
	    Invoke(nameof(HideMessage), 2f);
    }

    void OpenMenu()
    {
        isMenuOpen = true;
        Time.timeScale = 0f; 
        gameMenu.SetActive(true); 
    }

    void CloseMenu()
    {
        isMenuOpen = false;
        Time.timeScale = 1f; 
        gameMenu.SetActive(false); 
    }

    public void SaveData()
    {
        audioManager.PlaySFX(audioManager.buttonConfirm);

        SaveSystem.SaveGame();
    }

    public void closeGame() 
    {
        audioManager.PlaySFX(audioManager.buttonConfirm);

        SaveSystem.SaveGame();

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void HideMessage(){ messageText.gameObject.SetActive(false); }
}
