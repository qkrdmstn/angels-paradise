using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartEventSet: MonoBehaviour
{
    public Button[] optionButton;
    private UIManager uiManager;
    //UnityAction[] actionNames;
    public void SetOptionEvent(string eventName, int num)
    {
        InitOptionEvent(); //이전에 등록된 OnClickEvent 제거
        UnityAction[] actionNames = new UnityAction[num];
        switch (eventName)
        {
            case "나가는 문1":
                actionNames[0] = ExitDoor1;
                actionNames[1] = UIClose;
                break;
            case "창문":
                actionNames[0] = UIClose;
                actionNames[1] = ExitDoor1;
                actionNames[2] = ExitDoor1;
                actionNames[3] = ExitDoor1;
                break;
            default:
                for (int i = 0; i < num; i++)
                    actionNames[i] = UIClose;
                Debug.Log("해당 이벤트 없음");
                break;
        }

        for(int i=0; i<num; i++)
        {
            optionButton[i].onClick.AddListener(actionNames[num - i - 1]); //버튼 순서 -> 아래에서 위, 이벤트 순서 -> 위에서 아래
        }
    }

    public void UIClose()
    {
        uiManager.setInActiveUI();
        Debug.Log("UI 닫힘");
        
    }

    public void ExitDoor1()
    {
        Debug.Log("나갔다");
    }

    public void InitOptionEvent()
    {
        for (int i = 0; i < 5; i++)
        {
            optionButton[i].onClick.RemoveAllListeners(); //버튼에 등록된 함수 초기화
        }
    }
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();

    }
}
