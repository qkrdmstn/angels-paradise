using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    private void Awake()
    {
        instance = this;
    }
    public List<Item> itemDB = new List<Item>();
    [Space(20)]
    public GameObject filedItemPrefab;
    public Vector3[] pos; // 아이템을 생성할 위치

    [SerializeField]
    public SlotToolTip theSlotToolTip;

    private void Start()
    {
        for (int i=0; i<6; i++) // 랜덤으로 생성할 아이템의 개수
        {
            // 생성된 FieldItem의 Item을 itemDB중 한 개로 초기화
            GameObject go = Instantiate(filedItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0,3)]);
        }
    }    

    // SlotToolTip.cs
    public void ShowToolTip(Item item, Vector3 pos)
    {
        theSlotToolTip.ShowToolTip(item, pos);
    }

    // SlotToolTip.cs
    public void HideToolTip()
    {
        theSlotToolTip.HideToolTip();
    }
}
