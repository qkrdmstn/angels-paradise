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
        if (GameManager.Instance.progress >= 9)
            return Events[3];
        else if (GameManager.Instance.progress >= 8)
        {
            if (GameManager.Instance.progress < 9)
                GameManager.Instance.progress = 9;
            return Events[2];

        }
        else if (inventory.SearchInventory("인공 심장") != 0)
            return Events[1];
        else
            return Events[0];
    }
}
