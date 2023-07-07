using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject abilityUI;
    public GameObject emotionUI;
    public GameObject currentDispenserUI;
    public GameObject inventoryUI;
    GameObject player;
    public bool isActiveUI = false;

    //inventory variable
    private Inventory inven;
    public Slot[] slots; // ΩΩ∑‘ »Æ¿Â
    public Transform slotHolder;
    private bool activeInventory = false;

    public enum UIType
    {
        ability,
        emotion,
        inventory,
        currentDispenser
    }

    public void setActiveUI(UIType uiType)
    {
        GameObject ui = new GameObject();
        if (uiType == UIType.ability)
            ui = abilityUI;
        else if (uiType == UIType.emotion)
            ui = emotionUI;
        else if (uiType == UIType.inventory)
            ui = inventoryUI;
        else if (uiType == UIType.currentDispenser)
            ui = currentDispenserUI;

        if(uiType == UIType.ability || uiType == UIType.emotion)
            ui.transform.position = Camera.main.WorldToScreenPoint(player.transform.position + new Vector3(0, 0.75f, 0));
        if(uiType == UIType.inventory)
            Time.timeScale = 0.001f;

        isActiveUI = true;
        ui.gameObject.SetActive(true);
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
        //abilityUI
        if(Input.GetKey(KeyCode.E) && !isActiveUI)
        {
            setActiveUI(UIType.ability);    
        }

        //emotionUI
        if (Input.GetKey(KeyCode.F) && !isActiveUI)
        {
            setActiveUI(UIType.emotion);   
        }

        //inventory
        if (Input.GetKeyDown(KeyCode.Q) && !isActiveUI)
        {
            setActiveUI(UIType.inventory);
        }

        if (Input.GetKey(KeyCode.Escape) && isActiveUI)
        {
            abilityUI.SetActive(false);
            emotionUI.SetActive(false);
            currentDispenserUI.SetActive(false);
            inventoryUI.SetActive(false);

            Time.timeScale = 1;
            isActiveUI = false;
        }
    }

}
