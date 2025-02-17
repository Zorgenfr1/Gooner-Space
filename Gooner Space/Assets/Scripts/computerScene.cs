using UnityEngine;
using UnityEngine.SceneManagement;

public class computerScene : MonoBehaviour
{
    private bool isMouseOver = false;
    public GameObject shopComputerBalls;
    private string sceneToLoad;
    private bool enterAllowed;

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