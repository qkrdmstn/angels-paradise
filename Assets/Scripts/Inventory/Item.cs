using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumables,
    Etc
}

[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;

    public bool Use() // 아이템 성공 여부 판단
    {
        return false;
    }
}
