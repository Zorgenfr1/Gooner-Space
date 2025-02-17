using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections; 


public class MineralUI : MonoBehaviour
{
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI priceText;
    public Button sellButton;

    private string mineralTypeBenis;
    private float size;
    private float price;

    public void Setup(string type, float size, float price)
    {
        mineralTypeBenis = type;
        this.size = size;
        this.price = price;

        typeText.text = type;
        sizeText.text = $"{size:F2} TON";
        priceText.text = $"{price:F2} $$$";

        sellButton.onClick.RemoveAllListeners();
        sellButton.onClick.AddListener(() => SellMineral());
    }

    public void SellMineral()
    {
        Enum.TryParse(mineralTypeBenis, out MineralType mineralType);
        MiningSystem.instance.SellMineral(mineralType, size);
    }
}
