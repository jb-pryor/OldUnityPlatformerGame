using UnityEngine;

[System.Serializable]
public class ShopItem
{
    public string itemName;
    public int cost;
    public Sprite icon;
    public ShopItemType itemType;

    // Only used if itemType == Skin
    public string skinName;

    // Only used if itemType == SkinColor
    public Color[] colorSwapColors;

}

public enum ShopItemType
{
    SpeedBoost,
    DamageBoost,
    Skin,
    SkinColor,
    ResetColor,
    HealthBoost 
}
