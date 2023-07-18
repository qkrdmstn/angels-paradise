using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Base; // 전체적인 툴팁 패널 UI

    [SerializeField]
    private Text txt_ItemName;
    [SerializeField]
    private Text txt_ItemDesc;
    [SerializeField]
    private Text txt_ItemHowtoUsed;

    // Slot에서 MouseEnter 이벤트 발생 시 호출하기 위해 public
    public void ShowToolTip(Item item, Vector3 pos) // 아이템 슬롯의 위치에 활성화해주기 위해 Vector3 인수 추가
    {
        go_Base.SetActive(true); // 툴팁 패널 활성화
        // 인수로 아이템 슬롯의 위치가 들어오면, 이 위치보다 옆으로 Base_Outer패널 너비의 절반,
        // 아래로 Base_Outer 패널 높이의 절반만큼 더해진 위치에 활성화
        pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width * 1.5f,
                            -go_Base.GetComponent<RectTransform>().rect.height * 1.5f,
                            0);
        go_Base.transform.position = pos; // Base_Outer 툴팁 패널의 위치 설정

        // 인수로 들어온 아이템의 이름과 설명으로 텍스트 설정
        txt_ItemName.text = item.itemName;
        txt_ItemDesc.text = item.itemDesc;

        // 아이템 사용법 텍스트는 장비/소모품/아무것도 아닐 경우
        /*if (_item.itemType == Item.ItemType.Equipment)
            txt_ItemHowtoUsed.text = "우 클릭 - 장착";
        else if (_item.itemType == Item.ItemType.Used)
            txt_ItemHowtoUsed.text = "우 클릭 - 먹기";
        else*/
        txt_ItemHowtoUsed.text = "좌 클릭으로 사용";
    }

    // Slot.cs에서 MouseExit 이벤트가 발생했을 때 호출될 것이라 public
    public void HideToolTip()
    {
        go_Base.SetActive(false); // 툴팁 패널 비활성화
    }
}