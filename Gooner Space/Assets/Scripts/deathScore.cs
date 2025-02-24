using UnityEngine;
using TMPro;

public class deathScore : MonoBehaviour 
{
    public TextMeshProUGUI yourScoreText;

    private void Start()
    {
        yourScoreText.text = "Your Score: " + (PlayerStats.instance.PlayerScore).ToString("F0");
    }
}
