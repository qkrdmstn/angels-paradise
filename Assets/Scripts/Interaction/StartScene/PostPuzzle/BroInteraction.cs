using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroInteraction : Interaction
{

    public override InteractionEvent GetEvent()
    {
        if (GameManager.Instance.etcProgress[0] < 2)
            GameManager.Instance.etcProgress[0]++;
        return Events[0];
    }
}