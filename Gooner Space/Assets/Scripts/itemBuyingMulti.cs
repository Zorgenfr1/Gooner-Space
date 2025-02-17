using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class itemBuyingMulti : MonoBehaviour
{
    private bool isMouseOver = false;

    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI priceText;
    public string itemName;

    public float textPositionX;
    public float textPositionY;

    public float priceTextPositionX;
    public float pricetextPositionY;

    public float buttonPositionX;
    public float buttonPositionY;

    public int whatItemIsThis;
    public float price = 1.5f;
    private float buyingPrice;
    public Button buyButton;

    void Start()
    {
        itemNameText.gameObject.SetActive(false);
        priceText.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            if (!isMouseOver)
            {
                isMouseOver = true;
                switch (whatItemIsThis)
                {
                    case 0: // Fuel
                        float fullFuelPrice = (PlayerStats.instance.RemainingFuel / PlayerStats.instance.MaxFuel) * price * 100;
                        buyingPrice = Mathf.Min(fullFuelPrice, PlayerStats.instance.PlayerMoney);
                        break;
                    case 1: // Life
                        float fullLifePrice = (PlayerStats.instance.RemainingLife / PlayerStats.instance.MaxLife) * price * 100;
                        buyingPrice = Mathf.Min(fullLifePrice, PlayerStats.instance.PlayerMoney);
                        break;
                }

                itemNameText.text = itemName;
                priceText.text = $"{buyingPrice:F2}$";
                itemNameText.gameObject.SetActive(true);
                priceText.gameObject.SetActive(true);
                buyButton.gameObject.SetActive(true);

                Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
                itemNameText.transform.position = screenPos + new Vector3(textPositionX, textPositionY, 0);
                priceText.transform.position = screenPos + new Vector3(priceTextPositionX, pricetextPositionY, 0);
                buyButton.transform.position = screenPos + new Vector3(buttonPositionX, buttonPositionY, 0);

                switch (whatItemIsThis)
                {
                    case 0: // Fuel
                        buyButton.interactable = PlayerStats.instance.RemainingFuel < PlayerStats.instance.MaxFuel
                                                 && PlayerStats.instance.PlayerMoney > 0;
                        break;
                    case 1: // Life
                        buyButton.interactable = PlayerStats.instance.RemainingLife < PlayerStats.instance.MaxLife
                                                 && PlayerStats.instance.PlayerMoney > 0;
                        break;
                }
            }
        }
        else
        {
            if (isMouseOver)
            {
                isMouseOver = false;

                itemNameText.gameObject.SetActive(false);
                priceText.gameObject.SetActive(false);
                buyButton.gameObject.SetActive(false);
            }
        }
    }


    private void itemLogic()
    {
        switch (whatItemIsThis)
        {
            case 0: //fuel
                float fullFuelPrice = (PlayerStats.instance.RemainingFuel / PlayerStats.instance.MaxFuel) * price * 100;
                buyingPrice = Mathf.Min(fullFuelPrice, PlayerStats.instance.PlayerMoney);

                PlayerStats.instance.RemainingFuel += (buyingPrice / fullFuelPrice) * 100;
                PlayerStats.instance.PlayerMoney -= buyingPrice;

                PlayerStats.instance.RemainingFuel = Mathf.Min(PlayerStats.instance.RemainingFuel, PlayerStats.instance.MaxFuel);
                UIManager.instance.UpdateFuelUI(PlayerStats.instance.RemainingFuel);
                break;
            case 1: //life
                float fullLifePrice = (PlayerStats.instance.RemainingLife / PlayerStats.instance.MaxLife) * price * 100;
                buyingPrice = Mathf.Min(fullLifePrice, PlayerStats.instance.PlayerMoney);

                PlayerStats.instance.RemainingLife += (buyingPrice / fullLifePrice) * 100;
                PlayerStats.instance.PlayerMoney -= buyingPrice;

                PlayerStats.instance.RemainingLife = Mathf.Min(PlayerStats.instance.RemainingLife, PlayerStats.instance.MaxLife);
                UIManager.instance.UpdateLifeUI(PlayerStats.instance.RemainingLife);
                break;
        }
    }

    public void buyFunction()
    {
        itemLogic();
        priceText.text = $"{buyingPrice:F2}$";
        buyButton.interactable = false;
        UIManager.instance.UpdateMoneyUI(PlayerStats.instance.PlayerMoney);
    }
}
