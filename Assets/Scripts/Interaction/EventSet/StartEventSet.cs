using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartEventSet: MonoBehaviour //스타트 씬의 선택지 관리
{
    public Button[] optionButton;
    private UIManager uiManager;
    private GameObject player;
    private FadeManager theFade;
    private Inventory inventory;

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
            case "거래":
                actionNames[0] = Transaction;
                actionNames[1] = UIClose;
                break;
            case "배터리 넣기":
                actionNames[0] = InsertBattery;
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

    public void ExitStorage() //창고 나가기
    {
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent("창고를 나서다"); //UI로 event 전달
        uiManager.setActiveUI(UIType.talk); //UI 활성화
        StartCoroutine(ExitStorageCoroutine());        
        Debug.Log("나갓다");

    }

    IEnumerator ExitStorageCoroutine() //스크립트 끝나고 바로 나가기 위한 코루틴
    {
        yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));
        theFade.FadeOut();
        yield return new WaitForSeconds(1f);
        player.transform.position = new Vector3(-10, 15, 0);
        theFade.FadeIn();
    }

    public void Transaction() //거래 선택지
    {
        Debug.Log("거래,응");
        inventory.RemoveItem("안젤라의 개발일지");
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent("거래2"); //UI로 event 전달
        uiManager.setActiveUI(UIType.talk); //UI 활성화
    }

    public void InsertBattery() //배터리 넣기 선택지
    {
        inventory.RemoveItem("배터리");
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent("몰리 재가동"); //UI로 event 전달
        uiManager.setActiveUI(UIType.talk); //UI 활성화
    }

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        theFade = FindObjectOfType<FadeManager>();
        inventory = FindObjectOfType<Inventory>();
    }
}
