using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumables,
    Emotion,
    Etc
}

public enum EmotionType
{
    Joy,
    Anger,
    Sorrow,
    Pleasure
}

[System.Serializable]
public class Item
{
    [TextArea] // 여러 줄 가능해짐
    public string itemDesc; // 아이템 설명
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    public List<ItemEffect> efts;

    public bool Use() // 아이템 성공 여부 판단
    {
        bool isUsed = false;
        // 반복문으로 efts의 ExecuteRole을 실행
        foreach (ItemEffect eft in efts)
        {
            isUsed = eft.ExecuteRole();
        }
        return isUsed;
    }
}
