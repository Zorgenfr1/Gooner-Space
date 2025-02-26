using UnityEngine;

public class TimerUpdater : MonoBehaviour
{
    public static TimerUpdater instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        PlaytestTimerSystem.UpdateTimer();
    }
}

