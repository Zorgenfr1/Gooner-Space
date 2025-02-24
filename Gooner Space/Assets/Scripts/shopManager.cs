using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopManager : MonoBehaviour
{
    public ShopItem shopItem; 
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI priceText;
    public Button itemButton;

    private bool isMouseOver = false;

    public float textPositionX;
    public float textPositionY;

    public float priceTextPositionX;
    public float pricetextPositionY;

    void Start()
    {
        HideUI();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            if (!isMouseOver)
            {
                ShowUI();
            }
        }
        else
        {
            if (isMouseOver)
            {
                HideUI();
            }
        }
    }

    void ShowUI()
    {
        isMouseOver = true;

        itemNameText.text = shopItem.itemName;
        priceText.text = $"{shopItem.price:F2}$";
        itemNameText.gameObject.SetActive(true);
        priceText.gameObject.SetActive(true);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        itemNameText.transform.position = screenPos + new Vector3(textPositionX, textPositionY, 0);
        priceText.transform.position = screenPos + new Vector3(priceTextPositionX, pricetextPositionY, 0);

        if (shopItem.CanBuy(PlayerStats.instance.PlayerMoney))
        {
            itemButton.interactable = true;
        }
        else
        {
            itemButton.interactable = false;
        }

        if (shopItem.GetAmountBought() >= shopItem.itemLimit && shopItem.itemType == ShopItemType.Consumable)
        {
            priceText.text = "Item Limit Reached";
            itemButton.interactable = false;
        }

        if (shopItem.itemType == ShopItemType.Refuel)
        {
            if (shopItem.itemName == "Fuel Refill")
            {
                if (PlayerStats.instance.RemainingFuel >= PlayerStats.instance.MaxFuel)
                {
                    priceText.text = "Already Full";
                    itemButton.interactable = false;
                }
                else
                {
                    float fuelNeeded = PlayerStats.instance.MaxFuel - PlayerStats.instance.RemainingFuel;
                    float realPrice = fuelNeeded * shopItem.price;  

                    priceText.text = $"{realPrice:F2}$";
                }
            }

            else if (shopItem.itemName == "Life Refill")
            {
                if (PlayerStats.instance.RemainingLife >= PlayerStats.instance.MaxLife)
                {
                    priceText.text = "Already Full";
                    itemButton.interactable = false;
                }
                else
                {
                    float lifeNeeded = PlayerStats.instance.MaxLife - PlayerStats.instance.RemainingLife;
                    float realPrice = lifeNeeded * shopItem.price; 

                    priceText.text = $"{realPrice:F2}$";
                }
            }
        }
        else if (shopItem.itemType == ShopItemType.Consumable && shopItem.GetAmountBought() < shopItem.itemLimit)
        {
            priceText.text = $"{shopItem.price:F2}$";
        }
    }


    void HideUI()
    {
        isMouseOver = false;
        itemNameText.gameObject.SetActive(false);
        priceText.gameObject.SetActive(false);
    }

    public void BuyItem()
    {
        if (shopItem.CanBuy(PlayerStats.instance.PlayerMoney))
        {
            if (shopItem.itemType == ShopItemType.Refuel)
            {
                if (shopItem.itemName == "Fuel Refill")
                {
                    float fuelToFill = PlayerStats.instance.MaxFuel - PlayerStats.instance.RemainingFuel;

                    if (fuelToFill > 0)
                    {
                        float maxAffordableFuel = PlayerStats.instance.PlayerMoney / shopItem.price;
                        float fuelToBuy = Mathf.Min(fuelToFill, maxAffordableFuel);

                        PlayerStats.instance.PlayerMoney -= fuelToBuy * shopItem.price;
                        PlayerStats.instance.RemainingFuel += fuelToBuy;
                        PlayerStats.instance.RemainingFuel = Mathf.Min(PlayerStats.instance.RemainingFuel, PlayerStats.instance.MaxFuel);

                        UIManager.instance.UpdateFuelUI(PlayerStats.instance.RemainingFuel);
                        priceText.text = $"{fuelToBuy * shopItem.price:F2}$";  
                    }
                    else
                    {
                        priceText.text = "Fuel Already Full";
                    }
                }
                else if (shopItem.itemName == "Life Refill")
                {
                    float lifeToFill = PlayerStats.instance.MaxLife - PlayerStats.instance.RemainingLife;

                    if (lifeToFill > 0)
                    {
                        float maxAffordableLife = PlayerStats.instance.PlayerMoney / shopItem.price;
                        float lifeToBuy = Mathf.Min(lifeToFill, maxAffordableLife);

                        PlayerStats.instance.PlayerMoney -= lifeToBuy * shopItem.price;
                        PlayerStats.instance.RemainingLife += lifeToBuy;
                        PlayerStats.instance.RemainingLife = Mathf.Min(PlayerStats.instance.RemainingLife, PlayerStats.instance.MaxLife);

                        UIManager.instance.UpdateLifeUI(PlayerStats.instance.RemainingLife);
                        priceText.text = $"{lifeToBuy * shopItem.price:F2}$"; 
                    }
                    else
                    {
                        priceText.text = "Life Already Full";
                    }
                }
            }
            else
            {
                ApplyItemEffect(); 
                PlayerStats.instance.PlayerMoney -= shopItem.price;
                shopItem.IncreasePrice();
                priceText.text = $"{shopItem.price:F2}$";
                UIManager.instance.UpdateMoneyUI(PlayerStats.instance.PlayerMoney);
            }

            UIManager.instance.UpdateMoneyUI(PlayerStats.instance.PlayerMoney);
        }
    }




    void ApplyItemEffect()
    {
        switch (shopItem.itemType)
        {
            case ShopItemType.Consumable:
                ApplyConsumableEffect();
                break;

            case ShopItemType.Upgrade:
                ApplyUpgradeEffect();
                break;

            case ShopItemType.Refuel:
                ApplyRefuelEffect();
                break;
        }
    }

    void ApplyConsumableEffect()
    {
        switch (shopItem.itemName)
        {
            case "Emergency Button":
                PlayerStats.instance.emergency = true;
                UIManager.instance.UpdateMoneyUI(PlayerStats.instance.PlayerMoney);
                itemButton.interactable = false; 
                break;

            default:
                break;
        }
    }

    void ApplyUpgradeEffect()
    {
        switch (shopItem.itemName) 
        {
            case "Length Upgrade":
                PlayerStats.instance.maxVectorLengthPlayer += shopItem.increasement;
                PlayerStats.instance.moveSpeedPlayer += (shopItem.increasement * 0.1f); 
                break;

            case "Health Upgrade":
                PlayerStats.instance.MaxLife += shopItem.increasement;
                UIManager.instance.UpdateLifeUI(PlayerStats.instance.RemainingLife); 
                break;

            case "Cargo Upgrade":
                PlayerStats.instance.shipCapacity += shopItem.increasement;
                PlayerStats.instance.maxSize += shopItem.increasement; 
                UIManager.instance.UpdateCargoUI(PlayerStats.instance.maxSize, PlayerStats.instance.shipCapacity);
                break;

            case "Fuel Upgrade":
                PlayerStats.instance.MaxFuel += shopItem.increasement;
                UIManager.instance.UpdateFuelUI(PlayerStats.instance.RemainingFuel); 
                break;

            default:
                break;
        }
    }

    void ApplyRefuelEffect()
    {
        float fuelPrice = (PlayerStats.instance.RemainingFuel / PlayerStats.instance.MaxFuel) * shopItem.price * 100;
        float lifePrice = (PlayerStats.instance.RemainingLife / PlayerStats.instance.MaxLife) * shopItem.price * 100;

        switch (shopItem.itemName)
        {
            case "Fuel Refill":
                if (PlayerStats.instance.RemainingFuel >= PlayerStats.instance.MaxFuel)
                {
                    priceText.text = "Already Full";
                    return;
                }
                float fuelToBuy = Mathf.Min(fuelPrice, PlayerStats.instance.PlayerMoney);
                PlayerStats.instance.RemainingFuel += (fuelToBuy / fuelPrice) * 100;
                PlayerStats.instance.RemainingFuel = Mathf.Min(PlayerStats.instance.RemainingFuel, PlayerStats.instance.MaxFuel);
                UIManager.instance.UpdateFuelUI(PlayerStats.instance.RemainingFuel);
                break;

            case "Life Refill":
                if (PlayerStats.instance.RemainingLife >= PlayerStats.instance.MaxLife)
                {
                    priceText.text = "Already Full";
                    return;
                }
                float lifeToBuy = Mathf.Min(lifePrice, PlayerStats.instance.PlayerMoney);
                PlayerStats.instance.RemainingLife += (lifeToBuy / lifePrice) * 100;
                PlayerStats.instance.RemainingLife = Mathf.Min(PlayerStats.instance.RemainingLife, PlayerStats.instance.MaxLife);
                UIManager.instance.UpdateLifeUI(PlayerStats.instance.RemainingLife);
                break;
        }
    }
}
