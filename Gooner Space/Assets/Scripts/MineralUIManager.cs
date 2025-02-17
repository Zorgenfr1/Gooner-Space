using UnityEngine;
using System.Collections.Generic;

public class MineralUISpawner : MonoBehaviour
{
    public GameObject mineralUIPrefab; 
    public Transform uiParent;
    public float verticalSpacing = 20f;

    private void Start()
    {
        SpawnMineralUI();
    }

    public void SpawnMineralUI()
    {
        foreach (Transform child in uiParent)
        {
            Destroy(child.gameObject);
        }

        List<KeyValuePair<(MineralType, float), int>> minedMineralsList = new List<KeyValuePair<(MineralType, float), int>>(MiningSystem.instance.GetMinedMinerals());

        float currentYPosition = 0f;

        foreach (var entry in minedMineralsList)
        {
            MineralType type = entry.Key.Item1;
            float size = entry.Key.Item2;
            int count = entry.Value;

            float pricePerUnit = MiningSystem.instance.CalculatePrice(type, size);
            float totalPrice = pricePerUnit * count;

            GameObject mineralUIObj = Instantiate(mineralUIPrefab, uiParent);
            MineralUI mineralUI = mineralUIObj.GetComponent<MineralUI>();

            mineralUI.Setup(type.ToString(), size, totalPrice);

            RectTransform rectTransform = mineralUIObj.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, currentYPosition);

            currentYPosition -= verticalSpacing;
        }
    }
}

