/*
This will detect if the player has touched a falling platform and make that platform fall
after a short delay. Once it is gone, the platform will respawn in its original position
after some time.
*/

using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb2d;
    Vector2 defaultPos;
    [SerializeField] private float fallDelay, respawnTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultPos = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("PlatformDrop");
        }
    }

    IEnumerator PlatformDrop()
    {
        yield return new WaitForSeconds(fallDelay);
        rb2d.bodyType = RigidbodyType2D.Dynamic; //makes the platform have gravity for a moment
        yield return new WaitForSeconds(respawnTime);
        Reset();
    }

    //resets the platform to original position with no gravity
    private void Reset()
    {
        rb2d.bodyType = RigidbodyType2D.Static;
        transform.position = defaultPos;
    }
}
