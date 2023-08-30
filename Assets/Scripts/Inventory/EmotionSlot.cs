using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionSlot : Inventory
{
    public List<EmotionType> allowedEmotionTypes = new List<EmotionType>(); // 허용할 Emotion 타입 리스트

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
