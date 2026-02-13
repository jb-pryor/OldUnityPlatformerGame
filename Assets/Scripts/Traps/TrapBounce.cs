using System.Collections;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

//Bounces the player up when they collide with the object (for traps)

public class TrapBounce : MonoBehaviour
{
    public float bounceForce = 10f;
    private Animator anim = null;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") //if trap collides with player, player takes damage
        {   
            try
            {
                anim = GetComponent<Animator>();
            }
            catch(MissingComponentException){}

            HandlePlayerBounce(collision.gameObject);
        }
    }

    //makes player bounce up
    private void HandlePlayerBounce(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb)
        {
            //reset player velocity
            rb.linearVelocity = new Vector2(rb.linearVelocityX,0f);
            //apply bounce force
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            //bounce animation
            if (anim != null)
            {
                StartCoroutine(ActivateBounce());
            }
        }
    }

    //activates and deactivates animation for bouncepads
    private IEnumerator ActivateBounce()
    {
        anim.SetBool("Activated", true); //activate animation
        yield return new WaitForSeconds(0.3f); //wait for animation
        anim.SetBool("Activated", false); //dectivate animation
    }
}
