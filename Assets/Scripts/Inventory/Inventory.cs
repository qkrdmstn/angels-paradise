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

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;
    public List<Item> items = new List<Item>(); // 획득한 아이템 담기
    public delegate void OnSlotCountChange(int val); // 대리자 정의
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
        SlotCnt = 4;
    }

    public bool AddItem(Item _item) // 아이템창 개수만큼 아이템 먹을 수 있도록
    {
        if (items.Count < SlotCnt)
        {
            items.Add(_item);
            if (onChangeItem != null)
                onChangeItem.Invoke();
            return true;
        }
        return false;
    }

    private void Update()
    {
    } 



}
