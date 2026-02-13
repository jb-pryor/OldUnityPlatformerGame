using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject playerObject;

    public bool IsShopOpen { get; private set; }

    // this is the number of tokens the player has
    // it is initialized to 0 and can be modified by the game
    private int coinCounter = 0;
    public TMP_Text counterText;

    //death counter
    private int deathCounter = 0;
    public TMP_Text deathCounterText;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ResetInstance()
    {
        Instance = null; // Reset the static instance when entering play mode
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This keeps the GameManager alive between scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    public void AddToken()
    {
        coinCounter+=1;
        if (counterText != null)
        {
            counterText.text = "Tokens " + coinCounter;
        }
        else
        {
            Debug.LogWarning("CoinText UI not found in the scene!");
        }
        Debug.Log("Token added! Current count: " + coinCounter);
        
    }

    public int GetTokenCount() => coinCounter;

    private void UpdateUI()
    {
        
    }

    public void AddDeath()
    {
        deathCounter += 1;
        if (deathCounterText != null)
        {
            deathCounterText.text = "Deaths " + deathCounter;
        }
        else
        {
            Debug.LogWarning("DeathCounter UI not found in the scene!");
        }
        Debug.Log("Death added! Current count: " + deathCounter);
    }
    public int GetDeathCount() => deathCounter;

    public void ApplyPowerup(string type, float duration)
    {
        Debug.Log($"Applying power-up: {type} for {duration} seconds");

        if (type == "Speed")
        {
            PlayerMovement pm = playerObject.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.BoostSpeed(duration);
            }
            else
            {
                Debug.LogWarning("PlayerMovement not found on playerObject!");
            }
        }
        else if (type == "Attack")
        {
            PlayerAttack pa = playerObject.GetComponent<PlayerAttack>();
            if (pa != null)
            {
                pa.BoostAttack(duration);
            }
        }

    }

    public bool HasEnoughTokens(int amount) => coinCounter >= amount;

    public bool SpendCoins(int amount)
    {
        if (coinCounter >= amount)
        {
            coinCounter -= amount;
            counterText.text = "Tokens " + coinCounter;
            return true;
        }
        else
        {
            Debug.Log("Not enough coins!");
            return false;
        }
    }

    public void OpenShop()
    {
        IsShopOpen = true;
        // You can also pause player movement here if needed
    }

    public void CloseShop()
    {
        IsShopOpen = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-find the player and UI in the new scene
        counterText = GameObject.FindWithTag("TokenText")?.GetComponent<TMP_Text>();
        deathCounterText = GameObject.FindWithTag("DeathText")?.GetComponent<TMP_Text>();
        playerObject = GameObject.FindWithTag("Player");

        // Update UI with current coin count
        if (counterText != null)
            counterText.text = "Tokens " + coinCounter;
        if (counterText == null)
            Debug.LogWarning("CoinText UI not found in the scene!");

        // Update UI with current death count
        if (deathCounterText != null)
            deathCounterText.text = "Deaths " + deathCounter;
        if (deathCounterText == null)
            Debug.LogWarning("DeathCounter UI not found in the scene!");

    }

}

