using UnityEngine;

public class Gem : MonoBehaviour, IItem
{
    //private bool isCollected = false;
    public void Collect()
    {
        // if (isCollected) return;
        
        // isCollected = true;
        // Destroy(gameObject);
        // GameManager.Instance.AddToken();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddToken();
        }
        else
        {
            Debug.LogError("GameManager.Instance is null in Collect()");
        }

        Destroy(gameObject);
    }
}

