using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionSlot : Inventory
{
    public List<EmotionType> allowedEmotionTypes = new List<EmotionType>(); // ����� Emotion Ÿ�� ����Ʈ

    public override bool AddItem(Item item)
    {
        EmotionItem emotionItem = item as EmotionItem;
        if (emotionItem != null)
        {
            return allowedEmotionTypes.Contains(emotionItem.emotionType);
        }
        return false;
    }
}
