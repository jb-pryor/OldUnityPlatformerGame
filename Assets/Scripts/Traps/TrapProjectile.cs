using UnityEngine;

public class TrapProjectile : TrapDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime; //deactivate object after certain period of time
    private float lifetime;

    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true); //activate arrow
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed,0,0); //move arrow
        lifetime += Time.deltaTime;
        if (lifetime > resetTime) //once its time is up
        {
            gameObject.SetActive(false); //deactivate arrow
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); //execute logic from parent script first
        gameObject.SetActive(false); //deactives arrow once it hits another object
    }
}
