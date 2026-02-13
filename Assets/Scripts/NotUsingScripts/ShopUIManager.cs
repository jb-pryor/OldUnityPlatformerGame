using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public PlayerInventory inventory;
    public PlayerHealth playerHealth;
    public ColorSwap_HeroKnight colorSwap;

    public int potionCost = 5;
    public int skinCost = 10;

    public Button buyPotionButton;
    public Button buySkinButton;

    public Color[] newSkinColors; // Assign this in the inspector

    void Start()
    {
        buyPotionButton.onClick.AddListener(BuyHealthPotion);
        buySkinButton.onClick.AddListener(BuySkin);
    }

    public void BuyHealthPotion()
    {
        if (inventory.SpendCoins(potionCost))
        {
            playerHealth.AddHealth(1);
            Debug.Log("Bought a health potion!");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    public void BuySkin()
    {
        if (inventory.SpendCoins(skinCost))
        {
            inventory.unlockedSkins.Add(newSkinColors);
            colorSwap.SwapColorsFromSet(newSkinColors);
            Debug.Log("New skin purchased and applied!");
        }
        else
        {
            Debug.Log("Not enough coins for skin!");
        }
    }
}


