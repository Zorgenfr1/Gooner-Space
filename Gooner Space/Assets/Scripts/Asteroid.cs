using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public MineralType mineralType;
    public float size;
    public int points = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats.instance.AddScore(points);
            MiningSystem.instance.AddMineral(mineralType, size);
            PlayerStats.instance.shipCapacity -= size;
            UIManager.instance.UpdateCargoUI(PlayerStats.instance.maxSize, PlayerStats.instance.shipCapacity);

            Vector2 newPosition = new Vector2(Random.Range(-50f, 50f), Random.Range(-50f, 50f));
            transform.position = newPosition;
        }
    }
}
