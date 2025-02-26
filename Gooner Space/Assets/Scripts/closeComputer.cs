using UnityEngine;
using UnityEngine.SceneManagement;

public class closeComputer : MonoBehaviour
{
    private bool isMouseOver = false;
    public GameObject shopComputerButton;
    private string sceneToLoad;
    private bool enterAllowed;

    AudioManager audioManager;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        audioManager.PlaySFX(audioManager.computerBootUp);
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
                sceneToLoad = "FrodeMaster";
                enterAllowed = true;
                GameManager.instance.SaveData();
            }

            if (enterAllowed && Input.GetMouseButtonDown(0))
            {
                audioManager.PlaySFX(audioManager.computerShutDown);
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            audioManager.PlaySFX(audioManager.computerShutDown);
            sceneToLoad = "FrodeMaster";
            SceneManager.LoadScene(sceneToLoad);
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
