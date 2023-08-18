using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMentComputer : Interaction
{
    public override InteractionEvent GetEvent()
    {
        if (GameManager.Instance.progress >= 8 && GameManager.Instance.progress < 10)
            return Events[1];
        else
            return Events[0];
    }
}
