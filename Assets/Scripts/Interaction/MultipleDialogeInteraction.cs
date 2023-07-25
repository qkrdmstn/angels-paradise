using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleDialogeInteraction : DialogueInteraction //이벤트가 복수인 경우
{
    public string[] events;
    public bool condition = false;
    public float time;

    public override string GetEvent()
    {
        if (!condition)
            eventName = events[0];
        else if (condition)
            eventName = events[1];

        return eventName;

    }

}
