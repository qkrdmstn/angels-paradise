using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroInteraction : Interaction
{

    public override InteractionEvent GetEvent()
    {
        if (GameManager.Instance.progress1 < 2)
            GameManager.Instance.progress1++;
        return Events[0];
    }
}