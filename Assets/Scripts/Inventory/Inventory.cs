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
    
    public List<Item> items = new List<Item>(); // 획득한 아이템 담기 ->리스트 탐색하면 될 듯 
    
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
        SlotCnt = 4;
    }

    public bool AddItem(Item _item) // 아이템창 개수만큼 아이템 먹을 수 있도록
    {
        if (items.Count < SlotCnt)
        {
            items.Add(_item);
            if (onChangeItem != null)
                onChangeItem.Invoke();
            return true; // 아이템 획득에 성공하면 true, 아니면 false
        }
        return false;
    }

    public void RemoveItem(int _index)
    {
        // index에 맞는 items의 속성 제거
        items.RemoveAt(_index);
        // OnChangeItem을 호출해서 화면을 다시 그림
        onChangeItem.Invoke();
    }

    public int SearchInventory(string _itemName) //인벤토리 탐색
    {
        int num = 0;
        foreach (Item item in items)
        {
            if (item.itemName == _itemName)
                num++;
        }
        return num; //개수 반환
    }

    private void Update()
    {
    } 



}
