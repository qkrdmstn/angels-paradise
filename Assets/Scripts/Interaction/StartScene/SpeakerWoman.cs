using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerWoman : Interaction
{
    public override InteractionEvent GetEvent()
    {
        if (GameManager.Instance.etcProgress[1] == 0)
        {
            return Events[0];
        }
        else
            return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
