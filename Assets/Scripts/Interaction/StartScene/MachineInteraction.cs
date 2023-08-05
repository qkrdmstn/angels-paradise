using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineInteraction : Interaction
{
    Inventory inventory;
    private void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
    }

    public override InteractionEvent GetEvent()
    {
        if (inventory.SearchInventory("인공 심장") == 0)
            return Events[0];
        else
            return Events[1];

    }
}
