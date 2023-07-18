using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ItemEffect 상속받음
[CreateAssetMenu(menuName ="ItemEft/Consumable/Quest")]
public class ItemTestEft : ItemEffect
{
    public int QuestPoint = 0;

    public override bool ExecuteRole()
    {
        Debug.Log("플레이어 아이템 사용 확인" + QuestPoint);        
        return true;
    }    
}
