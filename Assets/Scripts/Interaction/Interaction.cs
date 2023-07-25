using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public List<InteractionEvent> Events = new List<InteractionEvent>();

    public virtual InteractionEvent GetEvent()
    {
        return Events[0];
            
    }
}
