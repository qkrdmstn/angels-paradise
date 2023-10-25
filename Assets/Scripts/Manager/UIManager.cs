using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIType
{
    none,
    inventory,
    talk,
    image,
    textInput,
}

public class UIManager : MonoBehaviour //UI on/off 담당
{
    //ui 변수
    public GameObject inventoryUI;
    public GameObject dialogueUI;
    public GameObject imageUI;
    public GameObject textInputUI;
    public UIType currentUI;
    GameObject player;
    GameObject ui;

    //inventory variable
    private Inventory inven;
    public Slot[] slots; // 슬롯 확장
    public Transform slotHolder;
    private bool activeInventory = false;

    public void setActiveUI(UIType uiType)
    {
        if (uiType == UIType.inventory)
            ui = inventoryUI;
        else if (uiType == UIType.talk)
            ui = dialogueUI;
        else if (uiType == UIType.image)
            ui = imageUI;
        else if (uiType == UIType.textInput)
            ui = textInputUI;

        //if(uiType == UIType.ability || uiType == UIType.emotion) //follow Player
        //    ui.transform.position = Camera.main.WorldToScreenPoint(player.transform.position + new Vector3(0, 0.75f, 0));
        if(uiType == UIType.inventory)
            Time.timeScale = 0.001f;

        ui.gameObject.SetActive(true);
        currentUI = uiType;
        //Debug.Log(currentUI);
    }

    public void setInActiveUI()
    {
        inventoryUI.SetActive(false);
        textInputUI.SetActive(false);

        dialogueUI.SetActive(false);
        dialogueUI.GetComponent<DialogueUI>().IndexInit(); //UI 비활성화시, 대사 인덱스 초기화

        imageUI.SetActive(false);

        Time.timeScale = 1;
        currentUI = UIType.none;
        //StartCoroutine(UITypeChangeNone());
    }
    IEnumerator UITypeChangeNone() //스크립트 끝나고 바로 나가기 위한 코루틴
    {
        yield return new WaitForSeconds(0.1f);
        currentUI = UIType.none;
    }
    void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inven.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }

    public void AddSlot()
    {
        inven.SlotCnt++;
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        inventoryUI.SetActive(activeInventory);
    }

    // Update is called once per frame
    void Update()
    {

        //inventory
        if (Input.GetKeyDown(KeyCode.I) && currentUI == UIType.none)
        {
            setActiveUI(UIType.inventory);
        }

        if (Input.GetKey(KeyCode.Escape) && currentUI != UIType.none)
        {
            setInActiveUI();
        }
    }

}
