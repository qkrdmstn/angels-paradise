using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : Interaction
{
    public override InteractionEvent GetEvent()
    {
        return Events[0];

    }
}
