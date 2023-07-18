using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int slotnum;
    public Item item;
    public Image itemIcon;
    private ItemDatabase itemDatabase;

    public void Start()
    {
        itemDatabase = FindObjectOfType<ItemDatabase>();
        if (itemDatabase == null)
        {
            Debug.LogError("ItemDatabase not found in the scene.");
        }
    }

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Slot에 있는 item.Use 메서드를 호출
        bool isUse = item.Use();

        // 아이템 사용에 성공하면 RemoveItem을 호출
        // RemoveItem은 Inventory의 items에서 알맞은 속성을 제거
        if (isUse)
        {
            // 설명창 숨기기
            itemDatabase.HideToolTip();

            // 아이템 제거
            Inventory.instance.RemoveItem(slotnum);
        }
    }

    // 마우스 커서가 슬롯에 들어갈 때 발동
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            itemDatabase.ShowToolTip(item, transform.position);
    }

    // 마우스 커서가 슬롯에서 나올 때 발동
    public void OnPointerExit(PointerEventData eventData)
    {
        itemDatabase.HideToolTip();
    }
}
