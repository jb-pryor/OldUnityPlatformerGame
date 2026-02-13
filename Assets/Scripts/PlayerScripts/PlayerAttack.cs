using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator m_animator;
    private float m_timeSinceAttack = 0.0f;
    private int m_currentAttack = 0;

    public float attackRange = 5.0f;         // Set the attack range in the inspector
    public int attackDamage = 1;

    private Transform attackPoint;           // Reference to the attack position (you can set this manually)

    private bool isBoosted = false;
    private int baseAttackDamage;

    private PlayerHealth playerHealth;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        attackPoint = transform;  // Assuming the attackPoint is the player's position (or you can assign a specific child Transform)

        // for powerups
        baseAttackDamage = attackDamage; // Save original attack damage
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if(playerHealth != null && playerHealth.isDead) {
            return;
        }
        m_timeSinceAttack += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f)
        {
            m_currentAttack++;

            if (m_currentAttack > 3)
                m_currentAttack = 1;

            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            m_animator.SetTrigger("Attack" + m_currentAttack);
            m_timeSinceAttack = 0.0f;
            DoAttack();
        }
    }

    void DoAttack()
    {
        // Find all colliders within the attack range
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        // Debugging: Log the number of colliders detected
       // Debug.Log($"Colliders detected: {hitColliders.Length}");

        // Loop through each collider detected
        foreach (Collider2D hit in hitColliders)
        {
            // Calculate the distance between the attack point and the enemy
            float distance = Vector2.Distance(attackPoint.position, hit.transform.position);

            // If the collider is on the "EnemyLayer" and within the attack range
            if (hit.gameObject.layer == LayerMask.NameToLayer("EnemyLayer") && distance <= attackRange) // && distance <= attackRange hit.gameObject.layer == LayerMask.NameToLayer("EnemyLayer")
            {
                //Debug.Log($"Enemy in range and hit: {hit.name}");

                // Try to get the EnemyTakeDamage component
                EnemyTakeDamage enemy = hit.GetComponent<EnemyTakeDamage>();
                if (enemy != null)
                {
                    Debug.Log($"Dealing {attackDamage} to {hit.name}!");
                    enemy.TakeDamage(attackDamage);  // Apply damage
                }
            }
            else
            {
                //Debug.Log($"Not an enemy or out of range: {hit.name}");
            }
        }

        // Optional: You can visualize the attack range in the Scene view (debug purposes)
        Debug.DrawLine(attackPoint.position, attackPoint.position + Vector3.up * attackRange, Color.red, 0.1f);
    }

    // attack boost method, this will allow player to do more damage
    public void BoostAttack(float duration)
    {
        if (!isBoosted)
            StartCoroutine(BoostAttackRoutine(duration));
    }

    private IEnumerator BoostAttackRoutine(float duration)
    {
        isBoosted = true;
        attackDamage = baseAttackDamage * 2;
        Debug.Log($"Attack Boosted! New damage: {attackDamage}");

        yield return new WaitForSeconds(duration);

        attackDamage = baseAttackDamage;
        isBoosted = false;
        Debug.Log("Attack boost ended. Damage back to normal.");
    }
}
