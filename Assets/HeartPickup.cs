using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    private float timer = 0f;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 3f && timer < 5f)
        {
            // Blink every 0.2 seconds
            sr.enabled = Mathf.FloorToInt(timer * 5) % 2 == 0;
        }

        if (timer >= 5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null && playerHealth.health < playerHealth.numOfHearts)
            {
                playerHealth.AddHealth(1);
                Destroy(gameObject); // Remove the heart
            }
            //Destroy(gameObject);
        }
    }
}
