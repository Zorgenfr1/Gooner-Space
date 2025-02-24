using UnityEngine;

public class PlayerExplosion : MonoBehaviour
{
    public GameObject piecePrefab; 
    public int numberOfPieces = 15; 
    public float explosionForce = 5f; 
    public float lifeTime = 2f; 
    public float randomRotationRange = 360f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TriggerDeath()
    {

        spriteRenderer.enabled = false;

        for (int i = 0; i < numberOfPieces; i++)
        {
            GameObject pieces = Instantiate(piecePrefab, transform.position, Quaternion.identity);

            pieces.transform.position = new Vector3(
                transform.position.x + Random.Range(-0.5f, 0.5f),
                transform.position.y + Random.Range(-0.5f, 0.5f),
                transform.position.z
            );

            foreach (Transform child in pieces.transform)
            {
                Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    float randomForce = Random.Range(explosionForce * 0.5f, explosionForce * 1.5f);
                    rb.AddForce(Random.insideUnitCircle * randomForce, ForceMode2D.Impulse);

                    float randomRotationSpeed = Random.Range(-randomRotationRange, randomRotationRange);
                    rb.angularVelocity = randomRotationSpeed;
                }
            }

            Destroy(pieces, lifeTime);
        }

        Destroy(gameObject);
    }
}

