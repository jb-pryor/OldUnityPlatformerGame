using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance; 
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if (movingLeft) //moving left
        {
            if (transform.position.x > leftEdge) //if object hasn't reached left edge yet
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else //if at left edge, turn around (go right)
            {
                movingLeft = false;
            }
        }
        else //moving right
        {
            if (transform.position.x < rightEdge) //if object hasn't reached right edge yet
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else //if at right edge, turn around (go left)
            {
                movingLeft = true;
            }
        }
    }

    //if player hits a saw, hurt the player
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
