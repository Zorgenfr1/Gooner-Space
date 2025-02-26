using UnityEngine;

public class Mine : MonoBehaviour
{
    public float size = -5f;
    public int points = -100;
    public float damage = 10;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats.instance.AddScore(points);
            PlayerStats.instance.TakeDamage(damage);

            audioManager.PlaySFX(audioManager.mineExplosion);

            Vector2 newPosition = new Vector2(Random.Range(-50f, 50f), Random.Range(-50f, 50f));
            transform.position = newPosition;
        }
    }
}

