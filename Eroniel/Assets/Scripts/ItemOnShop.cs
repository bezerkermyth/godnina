using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemOnShop 
{
    public Item itemShop;
    public bool showInShop;
    [Header("Buy Price")]
    public int SpringBuy;
    public int SummerBuy;
    public int AutumBuy;
    public int WinterBuy;
    [Header("Sell Price")]
    public int SpringSell;
    public int SummerSell;
    public int AutumSell;
    public int WinterSell;

}
