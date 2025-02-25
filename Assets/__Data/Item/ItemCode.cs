using System;
using UnityEngine;

public enum ItemCode
{
    NoItem = 0,

    Coin = 1,
    BagCoin = 2,
    Life = 3,
    Shield = 4,
}

public class ItemCodeParser
{
    public static ItemCode FromString(string itemName)
    {
        try
        {
            return (ItemCode)Enum.Parse(typeof(ItemCode),itemName);
        }
        catch (ArgumentException e)
        {
            Debug.LogError(e.ToString());
            return ItemCode.NoItem;
        }
    }
}