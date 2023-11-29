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
    private Inventory inventory;
    private trainPuzzleManager tPuzzle;

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
            case "거실 화분":
                actionNames[0] = FlowerPot;
                break;
            case "집 나온 맥스":
                actionNames[0] = GoOutMax;
                break;
            case "두번째 꼬부랑길":
                actionNames[0] = PostGet;
                break;
            case "수다쟁이 부인 0":
                actionNames[0] = () => LoadNewDialogue("수다쟁이 부인 1-1");
                actionNames[1] = () => LoadNewDialogue("수다쟁이 부인 1-2");
                actionNames[2] = () => LoadNewDialogue("수다쟁이 부인 1-3");
                break;
            case "수다쟁이 부인 1-2":
                actionNames[0] = () => LoadNewDialogue("수다쟁이 부인 2-1");
                actionNames[1] = () => LoadNewDialogue("수다쟁이 부인 2-2");
                actionNames[2] = () => LoadNewDialogue("수다쟁이 부인 2-3");
                break;
            case "수다쟁이 부인 2-2":
                actionNames[0] = () => LoadNewDialogue("수다쟁이 부인 3-1");
                actionNames[1] = () => LoadNewDialogue("수다쟁이 부인 3-2");
                actionNames[2] = () => LoadNewDialogue("수다쟁이 부인 3-3");
                break;
            case "여행객 등장":
                actionNames[0] = TrainRoadPuzzleStart;
                actionNames[1] = () => LoadNewDialogue("철로 퍼즐 다음에");
                break;
            case "건너가기001":
                actionNames[0] = () => TrainPuzzleToggle(2);
                actionNames[1] = () => TrainPuzzleToggle(3);
                break;
            case "건너가기010":
                actionNames[0] = () => TrainPuzzleToggle(1);
                actionNames[1] = () => TrainPuzzleToggle(3);
                break;
            case "건너가기011":
                actionNames[0] = () => TrainPuzzleToggle(1);
                actionNames[1] = () => TrainPuzzleToggle(2);
                actionNames[2] = () => TrainPuzzleToggle(3);
                break;
            case "건너가기100":
                actionNames[0] = () => TrainPuzzleToggle(0);
                actionNames[1] = () => TrainPuzzleToggle(3);
                break;
            case "건너가기101":
                actionNames[0] = () => TrainPuzzleToggle(0);
                actionNames[1] = () => TrainPuzzleToggle(2);
                actionNames[2] = () => TrainPuzzleToggle(3);
                break;
            case "건너가기110":
                actionNames[0] = () => TrainPuzzleToggle(0);
                actionNames[1] = () => TrainPuzzleToggle(1);
                actionNames[2] = () => TrainPuzzleToggle(3);
                break;
            case "건너가기111":
                actionNames[0] = () => TrainPuzzleToggle(0);
                actionNames[1] = () => TrainPuzzleToggle(1);
                actionNames[2] = () => TrainPuzzleToggle(2);
                actionNames[3] = () => TrainPuzzleToggle(3);
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
        StartCoroutine(LoadNewDialogueCoroutine(eventName));
    }

    IEnumerator LoadNewDialogueCoroutine(string eventName)
    {
        yield return new WaitUntil(()=>!uiManager.dialogueUI.GetComponent<DialogueUI>().isTyping);
        //UIClose();
        uiManager.setActiveUI(UIType.talk); //UI 활성화
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent(eventName); //UI로 event 전달
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
        UIClose();
        FadeManager.Instance.FadeOut();
        yield return new WaitForSeconds(1f);
        Player.Instance.transform.position = new Vector3(-12, 24, 0);
        FadeManager.Instance.FadeIn();
    }

    public void FlowerPot()
    {
        LoadNewDialogue("파헤쳐진 화분");
        if(GameManager.Instance.etcProgress[0] < 3)
            GameManager.Instance.etcProgress[0]++; //구역1 진행률 3로
    }

    public void GoOutMax()
    {
        StartCoroutine(GoOutMaxCoroutine());
        if (GameManager.Instance.etcProgress[0] < 4)
            GameManager.Instance.etcProgress[0]++; //구역1 진행률 4로
        
        //Destroy(dog);
    }

    IEnumerator GoOutMaxCoroutine()
    {
        FadeManager.Instance.FadeOut();

        postInteraction[] asd = FindObjectsOfType<postInteraction>();
        GameObject dog = null;
        for (int i = 0; i < asd.Length; i++)
        {
            if (asd[i].condition == 3)
            {
                dog = asd[i].gameObject;
                break;
            }
        }
        if (dog == null)
            Debug.LogWarning("Dog is NULL");
        dog.transform.position = new Vector3(10, 18, 0);

        yield return new WaitForSeconds(0.5f);
        FadeManager.Instance.FadeIn();

        //yield return new WaitUntil(() => (FadeManager.Instance.isFade));
        LoadNewDialogue("맥스 아래 쪽지");
    }

    public void PostGet()
    {
        LoadNewDialogue("지폐 획득");
        GameManager.Instance.etcProgress[0]++; //구역1 진행률 5로
    }

    public void TrainRoadPuzzleStart()
    {
        LoadNewDialogue("철로 퍼즐 시작");
        GameManager.Instance.etcProgress[2]++; //etcProgress[2]를 1로
        
    }    

    public void TrainPuzzleToggle(int index)
    {
        UIClose();
        tPuzzle.toggleState(index);
        StartCoroutine(TrainRoadToCross());

        if (tPuzzle.state[0] == tPuzzle.state[1] && tPuzzle.state[0] != tPuzzle.state[3]) //개 & 고양이만 남고, 플레이어 X
        {
            StartCoroutine(RollBackTrainPuzzle());
        }
        else if (tPuzzle.state[1] == tPuzzle.state[2] && tPuzzle.state[2] != tPuzzle.state[3]) //고양이 & 새만 남고, 플레이어 X
        {
            StartCoroutine(RollBackTrainPuzzle());
        }

    }

    IEnumerator TrainRoadToCross()
    {
        FadeManager.Instance.FadeOut();
        yield return new WaitForSeconds(0.5f);
        tPuzzle.MoveAnimal();
        yield return new WaitForSeconds(0.5f);
        FadeManager.Instance.FadeIn();

        if (tPuzzle.state[0] && tPuzzle.state[1] && tPuzzle.state[2])
        {
            Debug.Log("Clear");
            LoadNewDialogue("철로 퍼즐 완료");
            GameManager.Instance.etcProgress[2]++; //2로 설정
        }
    }

    IEnumerator RollBackTrainPuzzle()
    {
        yield return new WaitForSeconds(2f);
        tPuzzle.InitState();
        StartCoroutine(TrainRoadToCross());
    }

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        inventory = FindObjectOfType<Inventory>();
        tPuzzle = FindObjectOfType<trainPuzzleManager>();
    }
}
