using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHearts;
    public Sprite emptyHearts;

    private Animator animator;
    public bool isDead = false;

    //private PlayerBlock playerBlock;
    private PlayerMovement playerMovement;

    void Awake()
    {
        numOfHearts = 3; // or your default
        health = numOfHearts;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        //playerBlock = GetComponent<PlayerBlock>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Clamp health
        if (health > numOfHearts) health = numOfHearts;

        // Update hearts UI
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = (i < health) ? fullHearts : emptyHearts;
            hearts[i].enabled = (i < numOfHearts);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        
        //if the player is already dead or if they are blocking, they do not take damage
        if (isDead || playerMovement.isBlocking())
            return;

        //take damage
        health -= damageAmount;

        if (health <= 0) //if player dies
        {
            health = 0;
            Die();
        }
        else //if player gets hurt
        {
            animator.SetTrigger("Hurt");
        }
    }

    private void Die()
    {
        isDead = true;
        
        //set player velocity to 0,0
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f,0f);
        
        //activate death animation
        animator.SetTrigger("Death");
        
        // Optional: disable movement, play sound, etc.
        GetComponent<PlayerMovement>().enabled = false;

        GameManager.Instance.AddDeath();
    }

    public void AddHealth(int healAmount)
    {
        health += healAmount;
    }

    public void Respawn()
    {
        isDead = false; //set player to alive
        AddHealth(numOfHearts); //reset health
        animator.ResetTrigger("Death"); //reset death animation
        animator.Play("Idle"); //set player to idle animation
        playerMovement.SetCanMove(true); //let player move
        playerMovement.enabled = true; 
    }

    public void IncreaseMaxHealth(int amount)
    {
        numOfHearts += amount;
        health += amount; // Heal up new heart slots too

        if (health > numOfHearts)
            health = numOfHearts;
    }

}
