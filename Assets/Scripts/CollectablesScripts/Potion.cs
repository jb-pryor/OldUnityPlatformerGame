using UnityEngine;

public class Potion : MonoBehaviour, IItem
{
    public string potionType = "Speed";
    public float duration = 50f; //we can change this to be longer

    public void Collect()
    {
        GameManager.Instance.ApplyPowerup(potionType, duration);
        Destroy(gameObject);
    }
}
