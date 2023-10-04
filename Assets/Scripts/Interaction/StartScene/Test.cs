using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : NPCInteraction
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
