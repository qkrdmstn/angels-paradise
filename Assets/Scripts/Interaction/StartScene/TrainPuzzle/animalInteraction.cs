using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalInteraction : Interaction
{
    public trainPuzzleManager puzzleManager;

    public override InteractionEvent GetEvent()
    {
        Debug.Log(GameManager.Instance.etcProgress[2]);
        if (GameManager.Instance.etcProgress[2] != 1) //기찻길 퍼즐이 진행 중 X
        {
            return null;
        }

        InteractionEvent _event = new InteractionEvent();
        _event.eventType = InteractionType.Dialogue;
        string eName = "건너가기";

        if (puzzleManager.state[3] == puzzleManager.state[0]) //player와 상태가 같으면 1, 아니면 0
            eName += "1";
        else
            eName += "0";

        if (puzzleManager.state[3] == puzzleManager.state[1])
            eName += "1";
        else
            eName += "0";

        if (puzzleManager.state[3] == puzzleManager.state[2])
            eName += "1";
        else
            eName += "0";

        _event.eventName = eName;
        Debug.Log(_event);
        Debug.Log(_event.eventName);
        Debug.Log(_event.eventType);
        return _event;
    }

    // Start is called before the first frame update
    void Start()
    {
        puzzleManager = FindObjectOfType<trainPuzzleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
