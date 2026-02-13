using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public int coins = 0;
    public int healthPotions = 0;

    // Optional: store purchased skins as color sets
    public List<Color[]> unlockedSkins = new List<Color[]>();

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            return true;
        }
        return false;
    }

    public void AddCoins(int amount) => coins += amount;
}


