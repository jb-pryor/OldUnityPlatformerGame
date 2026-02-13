using UnityEngine;
using UnityEngine.UIElements;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0;

        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<TrapProjectile>().ActivateProjectile(); 
    }

    //returns first not-active arrow in arrow list
    private int FindArrow()
    {
        for(int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime; //incrementing cooldownTimer on every frame
        
        if(cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
