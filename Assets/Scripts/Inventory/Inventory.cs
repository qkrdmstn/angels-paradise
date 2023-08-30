using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnChangeItem(); // 아이템이 추가되면 슬롯UI에도 추가되도록
    public OnChangeItem onChangeItem;
    
    public List<Item> items = new List<Item>(); // 획득한 아이템 담기
    
    public delegate void OnSlotCountChange(int val); // 대리자 정의. 
    public OnSlotCountChange onSlotCountChange; // 대리자 인스턴스화
    
    private int slotCnt;

    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }

    void Start()
    {
        SlotCnt = 16;
        items = DataManager.instance.nowPlayer.items; // 아이템 불러오기
        onChangeItem.Invoke(); // 불러온 아이템 인벤토리에 그리기
    }

    public virtual bool AddItem(Item item) // 아이템창 개수만큼 아이템 먹을 수 있도록
    {
        if (items.Count < SlotCnt)
        {
            items.Add(item);
            if (onChangeItem != null)
                onChangeItem.Invoke();                
            return true; // 아이템 획득에 성공하면 true, 아니면 false
        }
        /*else if (item.itemType == ItemType.Emotion) // Emotion 아이템은 특정 슬롯에 추가
        {
            if (items.Count < SlotCnt)
            {
                items.Add(item);
                if (onChangeItem != null)
                    onChangeItem.Invoke();
                return true; // 아이템 획득에 성공하면 true, 아니면 false
            }
        }*/
        return false;
    }

    public void RemoveItem(int index)
    {
        // index에 맞는 items의 속성 제거
        items.RemoveAt(index);
        // OnChangeItem을 호출해서 화면을 다시 그림
        onChangeItem.Invoke();
    }

    private void Update()
    {
        // 아이템 저장 및 인벤토리 화면 저장
        DataManager.instance.nowPlayer.items = items;
    }
}
