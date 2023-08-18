using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StartEventSet : MonoBehaviour //스타트 씬의 선택지 관리
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
            case "게임시작1":
                actionNames[0] = () => LoadNewDialogue("게임시작3"); //응
                actionNames[1] = () => LoadNewDialogue("게임시작2"); //아니
                break;
            case "게임시작2":
                actionNames[0] = () => LoadNewDialogue("게임시작3"); //응
                actionNames[1] = () => LoadNewDialogue("게임시작2"); //아니
                break;
            case "안젤라의 개발 일지1":
                actionNames[0] = () => LoadNewDialogue("안젤라의 개발 일지2");
                break;
            case "안젤라의 개발 일지2":
                actionNames[0] = () => LoadNewDialogue("안젤라의 개발 일지3"); //안젤라가 개발을 중도포기한 것 같다
                actionNames[1] = () => LoadNewDialogue("안젤라의 개발 일지4"); //안젤라에게 무슨 일이 생긴 것 같다
                break;
            case "창고 나가기":
                actionNames[0] = ExitStorage; //응
                actionNames[1] = UIClose; //아니
                break;
            case "거래":
                actionNames[0] = Transaction; //응
                break;
            case "배터리 넣기":
                actionNames[0] = InsertBattery; //응
                break;
            case "심장 건네주기":
                actionNames[0] = GiveHeart; //응
                break;
            case "기쁨 감정 받기":
                actionNames[0] = gladEmotionComplete; //응
                actionNames[1] = UIClose; //아니
                break;
            case "노아 대화 1":
                actionNames[0] = () => LoadNewDialogue("노아 대화 2");
                actionNames[1] = () => LoadNewDialogue("노아 대화 3");
                actionNames[2] = () => LoadNewDialogue("노아 대화 4");
                actionNames[3] = () => LoadNewDialogue("노아 대화 5");
                break;
            case "노아 대화 1.5":
                actionNames[0] = () => LoadNewDialogue("노아 대화 2");
                actionNames[1] = () => LoadNewDialogue("노아 대화 3");
                actionNames[2] = () => LoadNewDialogue("노아 대화 4");
                actionNames[3] = () => LoadNewDialogue("노아 대화 5");
                break;
            default:
                for (int i = 0; i < num; i++)
                    actionNames[i] = UIClose;
                Debug.Log("해당 이벤트 없음");
                break;
        }

        EventSystem.current.SetSelectedGameObject(optionButton[num - 1].gameObject); //UI 포커스 설정
        for (int i = 0; i < num; i++)
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

    public void LoadNewDialogue(string eventName)
    {
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent(eventName); //UI로 event 전달
        uiManager.setActiveUI(UIType.talk); //UI 활성화
    }

    public void ExitStorage() //창고 나가기
    {
        if (GameManager.Instance.progress < 2)
        {
            LoadNewDialogue("창고를 나서다");           
        }     
  
        StartCoroutine(ExitStorageCoroutine());    
    }

    IEnumerator ExitStorageCoroutine() //스크립트 끝나고 바로 나가기 위한 코루틴
    {
        if (GameManager.Instance.progress < 2)
        {
            yield return new WaitUntil(() => (uiManager.currentUI == UIType.none)); //스크립트 끝날 때까지 기다리기
            GameManager.Instance.progress = 2;
        }          
        else
            UIClose();
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
        if (GameManager.Instance.progress < 5)
            GameManager.Instance.progress = 5;
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent("몰리 재가동"); //UI로 event 전달
        uiManager.setActiveUI(UIType.talk); //UI 활성화
    }

    public void GiveHeart() //심장 건네주기 선택지
    {
        inventory.RemoveItem("인공 심장");
        if (GameManager.Instance.progress < 8)
            GameManager.Instance.progress = 8;
        UIClose();
        //수술 기계 위에 심장 올려놓는 애니메이션 추가
    }

    public void gladEmotionComplete()
    {
        LoadNewDialogue("기쁨 감정 받기 완료");
        if(GameManager.Instance.progress < 10)
            GameManager.Instance.progress = 10;
    }

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        theFade = FindObjectOfType<FadeManager>();
        inventory = FindObjectOfType<Inventory>();
    }
}
