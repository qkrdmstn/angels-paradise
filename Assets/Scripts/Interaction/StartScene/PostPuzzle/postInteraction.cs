using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class postInteraction : Interaction
{
    public int condition;
    public GameObject shinyPrefab;
    private bool flag = false;
    private GameObject shinyObj;
    public override InteractionEvent GetEvent()
    {
        if (GameManager.Instance.etcProgress[0] == condition)
            return Events[0];
        else
            return null;
    }

    public void Update()
    {
        if (GameManager.Instance.etcProgress[0] == condition && !flag)
        {
            shinyObj = Instantiate(shinyPrefab, this.gameObject.transform);
            flag = true;
        }
        else if(GameManager.Instance.etcProgress[0] != condition && shinyObj != null )
        {
            flag = false;
            Destroy(shinyObj);
            if(condition == 4)
                Destroy(this.gameObject);
        } 
    }
}
