using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class itemSingleBuying : MonoBehaviour
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

    public float priceOfItem;
    public float growthCurve;
    private int amountOfItemBought =0;
    public bool amountOfItemLimitOrNot;
    public int amountOfItemLimit;
    public int whatItemIsThis;
    public float increasement;
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

                itemNameText.text = itemName;
                priceText.text = $"{priceOfItem:F2}$";
                itemNameText.gameObject.SetActive(true);
                priceText.gameObject.SetActive(true);
                buyButton.gameObject.SetActive(true);

                Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
                itemNameText.transform.position = screenPos + new Vector3(textPositionX , textPositionY, 0);
                priceText.transform.position = screenPos + new Vector3(priceTextPositionX, pricetextPositionY, 0);
                buyButton.transform.position = screenPos + new Vector3(buttonPositionX, buttonPositionY, 0);

                if (whatItemIsThis == 4) 
                {
                    buyButton.interactable = !PlayerStats.instance.emergency && PlayerStats.instance.PlayerMoney >= priceOfItem;
                }
                else if (amountOfItemLimitOrNot)
                {
                    buyButton.interactable = amountOfItemBought < amountOfItemLimit &&
                                             PlayerStats.instance.PlayerMoney >= priceOfItem;
                }
                else
                {
                    buyButton.interactable = PlayerStats.instance.PlayerMoney >= priceOfItem;
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

    private void itemPrice()
    {
        priceOfItem *= (growthCurve + 1);
    }

    private void itemLogic()
    {
        switch (whatItemIsThis)
        {
            case 0:
                PlayerStats.instance.maxVectorLengthPlayer += increasement;
                PlayerStats.instance.moveSpeedPlayer += 0.1f;
                break;
            case 1:
                PlayerStats.instance.MaxLife += increasement;
                UIManager.instance.UpdateLifeUI(PlayerStats.instance.RemainingLife);
                break;
            case 2:
                PlayerStats.instance.shipCapacity += increasement;
                PlayerStats.instance.maxSize += increasement;
                UIManager.instance.UpdateCargoUI(PlayerStats.instance.maxSize, PlayerStats.instance.shipCapacity);
                break;
            case 3:
                PlayerStats.instance.MaxFuel += increasement;
                UIManager.instance.UpdateFuelUI(PlayerStats.instance.RemainingFuel);
                break;
            case 4:
                PlayerStats.instance.emergency = true;
                buyButton.interactable = false;
                break;
        }
        UIManager.instance.UpdateMoneyUI(PlayerStats.instance.PlayerMoney);
    }

    public void buyFunction()
    {
        if (PlayerStats.instance.PlayerMoney >= priceOfItem)
        {
            itemLogic();
            amountOfItemBought++;
            PlayerStats.instance.PlayerMoney -= priceOfItem;
            itemPrice();
            priceText.text = $"{priceOfItem:F2}$";
            UIManager.instance.UpdateMoneyUI(PlayerStats.instance.PlayerMoney);
        }
    }
}
