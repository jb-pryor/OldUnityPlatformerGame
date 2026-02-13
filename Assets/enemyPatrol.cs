using UnityEngine;
using System.Collections;

public class enemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public float speed = 2f;
    public float detectionRange = 20f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public float attackDelay = 0.75f;
    public int damage = 1;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    private Transform player;
    private PlayerHealth playerHealth;
    private float attackTimer = 0f;

    private bool isAttacking = false;
    private bool playerInRangeAtStart = false;

    private EnemyTakeDamage enemyTakeDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;

        enemyTakeDamage = GetComponent<EnemyTakeDamage>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (enemyTakeDamage != null && enemyTakeDamage.isDead)
        {
            #pragma warning disable CS0618
            rb.velocity = Vector2.zero;
            #pragma warning restore CS0618
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        bool playerBetweenPoints = player.position.x >= Mathf.Min(pointA.transform.position.x, pointB.transform.position.x)
                                 && player.position.x <= Mathf.Max(pointA.transform.position.x, pointB.transform.position.x);

        attackTimer += Time.deltaTime;

        if (distanceToPlayer <= detectionRange && playerBetweenPoints)
        {
            // Chase player
            Vector2 direction = (player.position - transform.position).normalized;
            #pragma warning disable CS0618
            rb.velocity = new Vector2(direction.x * speed, 0);
            #pragma warning restore CS0618

            if (direction.x > 0 && transform.localScale.x < 0)
                flip();
            else if (direction.x < 0 && transform.localScale.x > 0)
                flip();

            if (distanceToPlayer <= attackRange)
            {
                if (!isAttacking && attackTimer >= attackCooldown)
                {
                    isAttacking = true;
                    playerInRangeAtStart = true;
                    StartCoroutine(DelayedAttack());
                }
            }
        }
        else
        {
            if (!isAttacking) Patrol();
        }
    }

    private IEnumerator DelayedAttack()
    {
        yield return new WaitForSeconds(attackDelay);

        float currentDistance = Vector2.Distance(transform.position, player.position);

        if (playerInRangeAtStart && currentDistance <= attackRange)
        {
            anim.SetTrigger("attackk");
            Attack();
        }

        isAttacking = false;
        attackTimer = 0f;
    }

    void Patrol()
    {
        Vector2 direction = (currentPoint.position - transform.position).normalized;
        #pragma warning disable CS0618
        rb.velocity = new Vector2(direction.x * speed, 0);
        #pragma warning restore CS0618

        if (direction.x > 0 && transform.localScale.x < 0)
            flip();
        else if (direction.x < 0 && transform.localScale.x > 0)
            flip();

        if (Mathf.Abs(transform.position.x - currentPoint.position.x) < 0.05f)
        {
            currentPoint = currentPoint == pointA.transform ? pointB.transform : pointA.transform;
        }
    }

    void Attack()
    {
        if(enemyTakeDamage.isDead) {

            return;
        }
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
