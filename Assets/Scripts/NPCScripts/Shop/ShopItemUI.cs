using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text itemNameText;
    public TMP_Text itemCostText;
    public Image iconImage;
    public Button buyButton;

    private ShopItem assignedItem;
    private ShopNPC shopNPC;

    public void Setup(ShopItem item, ShopNPC npc)
    {
        assignedItem = item;
        shopNPC = npc;

        itemNameText.text = item.itemName;
        itemCostText.text = "Cost: " +item.cost.ToString();
        iconImage.sprite = item.icon;

        buyButton.onClick.AddListener(BuyItem);
    }

    private void BuyItem()
    {
        Debug.Log("BuyItem() clicked! ShopNPC reference is " + (shopNPC != null));
        shopNPC.AttemptPurchase(assignedItem);
        buyButton.interactable = false;
    }
}

