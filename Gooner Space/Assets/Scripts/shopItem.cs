using UnityEngine;

public enum ShopItemType
{
    Consumable,
    Upgrade,
    Refuel
}
public class ShopItem : MonoBehaviour
{
    public string itemName;
    public float price;
    public ShopItemType itemType;
    public int itemLimit;
    public int priceGrowth;
    public int increasement;

    private int amountBought = 0;

    public void IncreasePrice()
    {
        price += priceGrowth; 
        if (itemLimit > 0)
        {
            itemLimit--;
        }
    }
    public bool CanBuy(float playerMoney)
    {
        switch (itemType)
        {
            case ShopItemType.Consumable:
                return (itemLimit == 0 || amountBought < itemLimit) && playerMoney >= price;

            case ShopItemType.Upgrade:
                return playerMoney >= price;

            case ShopItemType.Refuel:
                if (itemName == "Fuel Refill" && PlayerStats.instance.RemainingFuel >= PlayerStats.instance.MaxFuel)
                {
                    return false;
                }
                if (itemName == "Life Refill" && PlayerStats.instance.RemainingLife >= PlayerStats.instance.MaxLife)
                {
                    return false;
                }
                return playerMoney >= price;

            default:
                return false; 
        }
    }

    public int GetAmountBought()
    {
        return amountBought;
    }

    public void ResetAmountBought()
    {
        amountBought = 0;
    }
}
