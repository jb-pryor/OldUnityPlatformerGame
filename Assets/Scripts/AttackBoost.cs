using UnityEngine;

public class AttackBoost : MonoBehaviour, IItem
{
    public float duration = 5f;

    public void Collect()
    {
        GameManager.Instance.ApplyPowerup("Attack", duration);
        Destroy(gameObject);
    }
}
