using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mine : MonoBehaviour
{
    public float size = -5f;
    public int points = -100;
    public float damage = 10;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            

            GameManager.instance.MineHit(points, size, damage);

            Vector2 newPosition = new Vector2(Random.Range(-50f, 50f), Random.Range(-50f, 50f));
            transform.position = newPosition;

        }
    }
}
