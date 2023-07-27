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
    public GameObject scanObject;
    private UIManager uiManager;

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

        if (talkData[index1].options[index2].Trim() != "") //선택지 있으면 디버그로 출력
            Debug.Log(talkData[index1].options[index2]);
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
    }
}
