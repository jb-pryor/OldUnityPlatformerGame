using UnityEngine;
using Pathfinding;

public class EnemyChaseTrigger : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 20f;

    private AIPath aiPath;
    private AIDestinationSetter destinationSetter;
    private Seeker seeker;
    private EnemyTakeDamage enemyTakeDamage;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        seeker = GetComponent<Seeker>();
        enemyTakeDamage = GetComponent<EnemyTakeDamage>();
    }

    void Update()
    {
        if (player == null || enemyTakeDamage == null || enemyTakeDamage.isDead)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseDistance)
        {
            aiPath.enabled = true;
            destinationSetter.enabled = true;
            seeker.enabled = true;
        }
        else
        {
            aiPath.enabled = false;
            destinationSetter.enabled = false;
            seeker.enabled = false;
        }
    }
}

