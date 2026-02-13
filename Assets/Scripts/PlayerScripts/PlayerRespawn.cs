using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint; //stores the last checkpoint 
    private PlayerHealth playerHealth;
    private Collider2D oldCollision;

    public void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void Respawn()
    {
        transform.position = currentCheckpoint.position; //move player to checkpoint position
        playerHealth.Respawn(); //restore player health and reset animation
    }

    //activate checkpoints
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint" && currentCheckpoint != collision.transform)
        {
            //reset old checkpoint
            if (oldCollision != null)
            {
                oldCollision.GetComponent<Collider2D>().enabled = true; //activate old checkpoint collider
                oldCollision.GetComponent<Animator>().ResetTrigger("Appear"); //reset appear
                oldCollision.GetComponent<Animator>().SetTrigger("Disappear"); //trigger checkpoint disappear animation
            }

            //set new checkpoint
            currentCheckpoint = collision.transform; //store the checkpoint that we activated as current checkpoint
            oldCollision = collision;
            collision.GetComponent<Collider2D>().enabled = false; //deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("Appear"); //trigger checkpoint animation
            collision.GetComponent<Animator>().ResetTrigger("Disappear");
        }
    }
}
