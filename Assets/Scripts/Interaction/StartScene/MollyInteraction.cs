using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MollyInteraction :NPCInteraction
{
    Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
    }

    public override InteractionEvent GetEvent()
    {
        if (inventory.SearchInventory("배터리") == 0 && GameManager.Instance.progress < 5)
            return Events[0];
        else if (inventory.SearchInventory("배터리") != 0 && GameManager.Instance.progress < 5)
            return Events[1];
        else if (GameManager.Instance.progress >= 5)
            return Events[2];
        else
            return null;

    }
}
