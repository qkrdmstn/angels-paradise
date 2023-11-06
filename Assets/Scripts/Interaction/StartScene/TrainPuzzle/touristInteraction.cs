using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touristInteraction : Interaction
{
    public override InteractionEvent GetEvent()
    {
        if (GameManager.Instance.etcProgress[2] == 0) //train 퍼즐 시작 전이라면,
        {
            return Events[0]; //여행객 등장
        }
        else if(GameManager.Instance.etcProgress[2] == 1) //train 퍼즐 진행 중
        {
            return Events[1]; //여행객
        }
        else //train 퍼즐 클리어 후
        {
            return null;
        }
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
