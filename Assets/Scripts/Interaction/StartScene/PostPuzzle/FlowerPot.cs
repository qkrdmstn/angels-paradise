using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPot : Interaction
{
    public override InteractionEvent GetEvent()
    {
        if (GameManager.Instance.etcProgress[0] == 2)
            return Events[0];
        else
            return null;
    }
}
