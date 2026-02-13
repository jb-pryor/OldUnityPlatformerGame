using UnityEngine;
using Pathfinding;

public class EnemyTakeDamage : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    private Animator animator;
    public bool isDead = false;

    private AIPath aiPath;
    private AIDestinationSetter destinationSetter;

    public GameObject heartPrefab;


    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();

        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return; // Prevent taking damage after death

        currentHealth -= amount;
        animator.SetTrigger("takeDamage");
        Debug.Log($"{gameObject.name} took {amount} damage! Remaining health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.ResetTrigger("attackk");
        //animator.ResetTrigger("takeDamage");
        animator.SetTrigger("dead");
        Debug.Log($"{gameObject.name} has been defeated.");
        if (aiPath != null)
            aiPath.enabled = false;

        if (destinationSetter != null)
            destinationSetter.enabled = false;

        // Disable other scripts or behaviors if needed here
        // For example:
        // GetComponent<EnemyMovement>().enabled = false;

        if (Random.value <= 0.5f && heartPrefab != null) //0.33f
        {
            GameObject heart = Instantiate(heartPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = heart.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float upwardForce = 5f; // tweak this value to control height
                float sidewaysForce = 2f;

                // Randomly pick left (-1) or right (+1)
                int direction = Random.value < 0.5f ? -1 : 1;

                Vector2 force = new Vector2(sidewaysForce * direction, upwardForce);
                rb.AddForce(force, ForceMode2D.Impulse);
            }
        }



        StartCoroutine(DeathDelay());
    }

    private System.Collections.IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2f); // Wait 2 seconds before destroying
        Destroy(gameObject);
    }
}
