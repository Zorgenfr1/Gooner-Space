using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceGrid : MonoBehaviour
{
    public Canvas worldSpaceCanvas;  
    public GameObject gridLinePrefab; 
    public int gridSize = 10; 
    public float cellSize = 1f; 
    public float lineThickness = 0.1f; 

    private void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        float gridWidth = gridSize * cellSize;
        float gridHeight = gridSize * cellSize;

        for (int i = 0; i <= gridSize; i++)
        {
            CreateGridLine(new Vector2(i * cellSize - gridWidth / 2, 0), new Vector2(lineThickness, gridHeight));
        }

        for (int i = 0; i <= gridSize; i++)
        {
            CreateGridLine(new Vector2(0, i * cellSize - gridHeight / 2), new Vector2(gridWidth, lineThickness));
        }
    }

    void CreateGridLine(Vector2 position, Vector2 size)
    {
        GameObject gridLine = Instantiate(gridLinePrefab);
        gridLine.transform.SetParent(worldSpaceCanvas.transform, false); 

        gridLine.transform.localPosition = position;
        RectTransform rectTransform = gridLine.GetComponent<RectTransform>();
        rectTransform.sizeDelta = size; 

        Image image = gridLine.GetComponent<Image>();

        image.color = new Color(0.3f, 0.3f, 0.3f, 0.2f);

    }

}
