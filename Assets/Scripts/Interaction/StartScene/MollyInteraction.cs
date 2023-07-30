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
        if (inventory.SearchInventory("배터리") == 0)
            return Events[0];
        else if (inventory.SearchInventory("배터리") != 0)
            return Events[1];
        else
            return null;

    }
}
