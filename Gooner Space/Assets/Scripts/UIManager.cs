using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TMP_Text scoreText;
    public TMP_Text moneyText;
    public TMP_Text fuelText;
    public TMP_Text lifeText;
    public Image fuelImage;
    public Image lifeImage;

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
        }
    }
    private void Start()
    {
        UpdateScoreUI(PlayerStats.instance.PlayerScore);
        UpdateMoneyUI(PlayerStats.instance.PlayerMoney);
        UpdateLifeUI(PlayerStats.instance.RemainingLife);
        UpdateFuelUI(PlayerStats.instance.RemainingFuel);
    }

    public void UpdateScoreUI(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateMoneyUI(float money)
    {
        moneyText.text = "Money: $" + money.ToString("F2");
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
}
