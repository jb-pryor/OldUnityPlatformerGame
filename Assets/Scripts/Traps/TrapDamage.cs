using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] protected int damage; //how much the trap hurts the player (set in unity inspector)
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") //if trap collides with player, player takes damage
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
