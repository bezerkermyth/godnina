using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item:ScriptableObject
{
    public Sprite artwork;

    public string itemName;

    public string type;

    public int itemId;
    public int buyValue;
    public int sellValue;
    public int maxStack;

}
