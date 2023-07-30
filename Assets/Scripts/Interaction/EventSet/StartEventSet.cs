using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartEventSet: MonoBehaviour
{
    public Button[] optionButton;
    private UIManager uiManager;
    private GameObject player;

   
    public void SetOptionEvent(string eventName, int num)
    {
        InitOptionEvent(); //이전에 등록된 OnClickEvent 제거
        UnityAction[] actionNames = new UnityAction[num];
        switch (eventName)
        {
            case "창고 나가기":
                actionNames[0] = ExitStorage;
                actionNames[1] = UIClose;
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

    public void InitOptionEvent()
    {
        for (int i = 0; i < 5; i++)
        {
            optionButton[i].onClick.RemoveAllListeners(); //버튼에 등록된 함수 초기화
        }
    }

    public void UIClose()
    {
        uiManager.setInActiveUI();
        Debug.Log("UI 닫힘");
        
    }

    public void ExitStorage()
    {
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent("창고를 나서다"); //UI로 event 전달
        uiManager.setActiveUI(UIType.talk); //UI 활성화
        StartCoroutine(ExitStorageCoroutine());        
        Debug.Log("나갓다");

    }

    IEnumerator ExitStorageCoroutine()
    {
        yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));
        player.transform.position = new Vector3(-10, 15, 0);
    }

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player");

    }
}
