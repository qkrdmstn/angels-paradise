using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour //단일 이벤트 오브젝트 상호작용
{
    public string eventName;
 
    public virtual string GetEvent()
    {

        return eventName;
    }

}
