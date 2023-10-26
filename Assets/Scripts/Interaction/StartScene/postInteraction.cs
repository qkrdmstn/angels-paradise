using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class postInteraction : Interaction
{
    public int condition;
    public override InteractionEvent GetEvent()
    {
        if (GameManager.Instance.progress1 == condition)
            return Events[0];
        else
            return null;
    }
}
