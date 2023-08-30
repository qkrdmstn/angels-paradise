using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionInven : Inventory
{
    /*public override bool AddItem(Item item)
    {
        if (item.itemType == ItemType.Emotion) // Emotion 아이템은 특정 슬롯에 추가
        {
            if (items.Count < SlotCnt)
            {
                items.Add(item);
                if (onChangeItem != null)
                    onChangeItem.Invoke();
                return true; // 아이템 획득에 성공하면 true, 아니면 false
            }
        }
        return false;
    }*/
}
