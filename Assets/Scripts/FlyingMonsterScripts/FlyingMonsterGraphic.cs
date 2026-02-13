using UnityEngine;
using Pathfinding;

public class FlyingMonsterGraphic : MonoBehaviour
{
    public AIPath aiPath;

    // Update is called once per frame
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f) {
            transform.localScale = new Vector3(1f, 1f, 1f); //this script just flips the sprite whether the enemy is moving left or right
        }
        else if(aiPath.desiredVelocity.x <= -0.01f){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
