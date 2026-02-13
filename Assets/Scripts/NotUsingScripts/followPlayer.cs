using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    
    [SerializeField] public Transform player;
    public float speed = 2f;
    public float detectionRange = 10f;
    // Update is called once per frame
    void Update()
    {
        
        if (player == null) return; // Ensure we have a player reference

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange) // Check if the player is within range
        {
            if (player.position.x < transform.position.x) // Player is to the left
            {
                MoveLeft();
            }
            else if (player.position.x > transform.position.x) // Player is to the right
            {
                MoveRight();
            }
        }
    }

    void MoveLeft()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        transform.localScale = new Vector3(-0.58f, 0.5f, 1); // Flip sprite left
    }

    void MoveRight()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.localScale = new Vector3(0.58f, 0.5f, 1); // Flip sprite right
    }
}
