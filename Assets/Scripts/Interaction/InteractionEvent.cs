using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType
{
    Dialogue,
    Image,
    Ability
}

[System.Serializable]
public class InteractionEvent
{
    public InteractionType eventType;
    public string eventName;

}
