using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class computerScene : MonoBehaviour
{
    private bool isMouseOver = false;
    public GameObject shopComputerBalls;
    private string sceneToLoad;
    private bool enterAllowed;
    public TextMeshProUGUI itemNameText;
    public string itemName;

    public float textPositionX;
    public float textPositionY;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
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
                sceneToLoad = "shopComputer";
                enterAllowed = true;
                itemNameText.gameObject.SetActive(true);
                itemNameText.text = itemName;
                Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
                itemNameText.transform.position = screenPos + new Vector3(textPositionX, textPositionY, 0);
                SaveSystem.SaveGame();
            }

            if (enterAllowed && Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
        else
        {
            if (isMouseOver)
            {
                itemNameText.gameObject.SetActive(false);
                enterAllowed = false;
                isMouseOver = false;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}