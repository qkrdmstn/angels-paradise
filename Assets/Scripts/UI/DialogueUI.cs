using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class DialogueUI : MonoBehaviour //대화 UI
{
    //대화 UI (TalkUI)
    public Text speaker;
    public Text context;
    private UIManager uiManager;
    public bool isKeyDown = false;

    //선택지
    private int numOption = 5;
    public GameObject optionsParent;
    public GameObject[] optionButtons;
    private Text[] buttonText;
    public StartEventSet eventData1;

    //대화 data
    [SerializeField] TalkData[] talkData; //이름, 대사 배열 로 이루어진 구조체
    private string currentEvent; //현재 이벤트 상태
    private int index1 = 0;
    private int index2 = 0;

    public void IndexInit() //인덱스 초기화
    {
        index1 = 0;
        index2 = 0;
    }

    public void SetCurrentEvent(string eventName)
    {
        IndexInit();
        currentEvent = eventName;
        talkData = DialogueData.GetDialogue(currentEvent); //대화 데이터 로드
        SetSentence(index1, index2);

    }

    public void SetSentence(int index1, int index2)
    {
        speaker.text = talkData[index1].name + ": ";
        context.text = talkData[index1].constexts[index2]; //이름, 내용을 텍스트로 설정

        SetOption(talkData[index1].options[index2]);
    }

    public void SetOption(string options)
    {
        if (options.Trim() != "") //선택지가 유효하면,
        {
            optionsParent.SetActive(true); //부모 활성화
            string[] option = options.Split("/"); //옵션 나누기
            for (int i = 0; i < option.Length; i++) //나눠진 옵션 개수만큼 버튼 활성화
            {
                optionButtons[i].GetComponentInChildren<Text>().text = option[option.Length - i - 1]; //텍스트는 위부터 적용
                optionButtons[i].SetActive(true);
                //버튼 이벤트 설정
            }
            for (int i = numOption - 1; i > option.Length - 1; i--) //나머지 버튼 비활성화
            {
                optionButtons[i] = optionsParent.transform.GetChild(i).gameObject;
                optionButtons[i].SetActive(false);
            }
            //버튼 이벤트 설정
            eventData1.SetOptionEvent(currentEvent, option.Length);
        }
        else
            optionsParent.SetActive(false); //선택지 없으면 부모 비활성화
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && uiManager.currentUI == UIType.talk)
        {
            isKeyDown = true;
            if (index1 < talkData.Length && index2 + 1 < talkData[index1].constexts.Length) //대사 업데이트
            {
                index2++;
                SetSentence(index1, index2);
            }
            else if (index1 + 1 < talkData.Length) //다음 사람 대사
            {
                index1++;
                index2 = 0;
                SetSentence(index1, index2);
            }
            else //대사 끝
            {
                uiManager.setInActiveUI(); //UI 비활성화
            }

        }
        else
            isKeyDown = false;
    }
}
