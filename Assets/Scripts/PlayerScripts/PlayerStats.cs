using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    public PlayerHealth playerHealth;

    public int healthBoostAmount = 1;
    public float speedBoostAmount = 2f; // Amount to increase speed
    public int damageBoostAmount = 5; // Amount to increase damage

    private bool isInPlayMode = false; // Flag to check if we are in Play mode

    private void Awake()
    {
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();

        if (playerAttack == null)
            playerAttack = GetComponent<PlayerAttack>();

        if (playerHealth == null)
            playerHealth = GetComponent<PlayerHealth>();

        // Reset stats when entering Play mode
        if (Application.isPlaying)
        {
            isInPlayMode = true;
            // Clear PlayerPrefs to reset stats to default values
            PlayerPrefs.DeleteKey("PlayerSpeed");
            PlayerPrefs.DeleteKey("PlayerDamage");
            PlayerPrefs.DeleteKey("PlayerMaxHealth");
            PlayerPrefs.Save();

            Debug.Log("PlayerPrefs cleared. Stats reset to default values.");

        }
        else
        {
            isInPlayMode = false;
        }

        // // Apply stored boosts ONLY if we are in Play mode and not exiting Play mode
        // if (isInPlayMode && PlayerPrefs.HasKey("PlayerSpeed"))
        // {
        //     playerMovement.Speed = PlayerPrefs.GetFloat("PlayerSpeed");
        //     Debug.Log($"Speed restored to: {playerMovement.Speed}");
        // }

        // if (isInPlayMode && PlayerPrefs.HasKey("PlayerDamage"))
        // {
        //     playerAttack.attackDamage = PlayerPrefs.GetInt("PlayerDamage");
        //     Debug.Log($"Damage restored to: {playerAttack.attackDamage}");
        // }

        // if (isInPlayMode && PlayerPrefs.HasKey("PlayerMaxHealth"))
        // {
        //     playerHealth.numOfHearts = PlayerPrefs.GetInt("PlayerMaxHealth");
        //     playerHealth.health = playerHealth.numOfHearts; // Full heal when loading
        //     Debug.Log($"Health restored to: {playerHealth.numOfHearts}");
        // }
    }

    public void ApplySpeedBoost()
    {
        // Apply the boost during the Play mode
        playerMovement.Speed *= speedBoostAmount;
        Debug.Log($"Permanent speed boosted to: {playerMovement.Speed}");

        // Save the speed boost to PlayerPrefs
        PlayerPrefs.SetFloat("PlayerSpeed", playerMovement.Speed);
        PlayerPrefs.Save();
    }

    public void ApplyDamageBoost()
    {
        if (playerAttack != null)
        {
            playerAttack.attackDamage += damageBoostAmount;
            Debug.Log($"Damage boosted to: {playerAttack.attackDamage}");

            // Save the damage boost to PlayerPrefs
            PlayerPrefs.SetInt("PlayerDamage", playerAttack.attackDamage);
            PlayerPrefs.Save();
        }
    }

    public void ApplyHealthBoost()
    {
        playerHealth.IncreaseMaxHealth(healthBoostAmount);
        Debug.Log($"Max health increased to: {playerHealth.numOfHearts}");
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            // Exiting Play mode, reset stats to default values (optional)
            ResetStats();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        // Only save the permanent stats when switching scenes while in Play mode
        if (isInPlayMode)
        {
            // Save speed and damage boosts if the scene has changed
            PlayerPrefs.SetFloat("PlayerSpeed", playerMovement.Speed);
            PlayerPrefs.SetInt("PlayerDamage", playerAttack.attackDamage);
            PlayerPrefs.SetInt("PlayerMaxHealth", playerHealth.numOfHearts);
            PlayerPrefs.Save();
        }
    }

    private void ResetStats()
    {
        // Reset stats to their default values (when exiting Play mode)
        playerMovement.Speed = 3f;  // Example default speed
        playerAttack.attackDamage = 1;  // Example default damage

        Debug.Log("Player stats reset to default values.");
    }
}


