using UnityEngine;

public class FlyingEyeAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float timeToAttack = 1f;
    public int damage = 1;

    private Transform player;
    private PlayerHealth playerHealth;

    private float playerInRangeTimer = 0f;

    private Animator animator;

    private EnemyTakeDamage enemyTakeDamage;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyTakeDamage = GetComponent<EnemyTakeDamage>();
    }

    void Update()
    {

        //TESTING
        if (Time.timeScale == 0f)
            return; // Skip updates when the game is paused

        if(enemyTakeDamage != null && enemyTakeDamage.isDead) {

            return;
        }
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            playerInRangeTimer += Time.deltaTime;
            animator.SetTrigger("attackk");

            if (playerInRangeTimer >= timeToAttack)
            {
                Attack();
                playerInRangeTimer = 0f;

            }
        }
        else
        {
            playerInRangeTimer = 0f;
        }
    }

    void Attack()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
