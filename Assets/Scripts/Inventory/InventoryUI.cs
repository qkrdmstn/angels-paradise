using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inven;
    public GameObject inventoryPanel;
    bool activeInventory = false;
    public Slot[] slots; // 슬롯 확장
    public Transform slotHolder;
    // 슬롯 확장, UI 끄고 켜기
    void Start()
    {
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        inventoryPanel.SetActive(activeInventory);
    }

    void SlotChange(int val)
    {
        for (int i=0; i<slots.Length; i++)
        {
            if (i < inven.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            inventoryPanel.SetActive(true);
            Time.timeScale=0f;
        }
        
        if (Input.GetKey(KeyCode.Escape))
        {
            inventoryPanel.SetActive(false);
            Time.timeScale=1;
        }
    }

    public void AddSlot()
    {
        inven.SlotCnt++;
    }

    void RedrawSlotUI()
    {
        for (int i=0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i=0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
