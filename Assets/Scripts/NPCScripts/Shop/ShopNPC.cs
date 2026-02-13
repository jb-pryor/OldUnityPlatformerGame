using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopNPC : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject shopItemPrefab;
    public Transform itemContainer;
    private ColorSwap_HeroKnight colorSwapper;


    public List<ShopItem> availableItems;

    private PlayerStats playerStats;

    public PlayerMovement playerMovement;

    private bool hasPopulated = false;

    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        colorSwapper = player.GetComponent<ColorSwap_HeroKnight>();
    }

    public void OpenShop()
    {
        if (!hasPopulated)
        {
            PopulateShop();
            hasPopulated = true;
        }
        shopUI.SetActive(true);
        GameManager.Instance.OpenShop(); // set IsShopOpen = true, disables movement
        playerMovement?.SetCanMove(false);
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        GameManager.Instance.CloseShop(); // set IsShopOpen = false, enables movement
        playerMovement?.SetCanMove(true);
    }

    private void PopulateShop()
    {
        foreach (Transform child in itemContainer)
            Destroy(child.gameObject);

        foreach (ShopItem item in availableItems)
        {
            GameObject newItem = Instantiate(shopItemPrefab, itemContainer);
            newItem.transform.localScale = Vector3.one;
            newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            ShopItemUI ui = newItem.GetComponent<ShopItemUI>();

            if (ui == null)
            {
                Debug.LogError("ShopItemUI component is missing.");
                return;
            }

            ui.Setup(item, this); // <- the important change!!
        }
    }



    public void AttemptPurchase(ShopItem item)
    {
        Debug.Log("AttemptPurchase called for: " + item.itemName);
        if (GameManager.Instance.HasEnoughTokens(item.cost))
        {
            GameManager.Instance.SpendCoins(item.cost);

            switch (item.itemType)
            {
                case ShopItemType.SpeedBoost:
                    playerStats.ApplySpeedBoost();
                    break;

                case ShopItemType.DamageBoost:
                    playerStats.ApplyDamageBoost();
                    break;

                case ShopItemType.SkinColor:
                    if (item.colorSwapColors == null || item.colorSwapColors.Length == 0)
                    {
                        Debug.LogError("colorSwapColors is null or empty for item: " + item.itemName);
                        return;
                    }
                    // Assuming item.colorSwapColors is a Color[] defined in ShopItem
                    colorSwapper.SwapColorsFromSet(item.colorSwapColors);
                    break;
                case ShopItemType.ResetColor:
                    colorSwapper.ResetAllSpritesColors();
                    break;
                
                case ShopItemType.HealthBoost: // <-- ADD THIS
                    playerStats.ApplyHealthBoost();
                    break;

            }

            Debug.Log($"Purchased {item.itemName}!");
        }
        else
        {
            Debug.Log("Not enough tokens!");
        }
    }
}


