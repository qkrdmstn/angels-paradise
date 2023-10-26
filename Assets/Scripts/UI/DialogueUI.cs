using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class DialogueUI : MonoBehaviour //대화 UI
{
    //대화 UI (TalkUI)
    public GameObject nameBox;
    public Text speaker;
    public Text context;
    private UIManager uiManager;
    public bool isKeyDown = false;
    //타이핑 효과
    public float typingDelay = 0.015f;
    public bool isTyping;


    //Image
    public GameObject nextImage;
    public GameObject faceImage;

    //선택지
    private int numOption = 5;
    public GameObject optionsParent;
    public GameObject[] optionButtons;
    private Text[] buttonText;
    public StartEventSet eventData1;
    bool haveOption;

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

    public void SetCurrentEvent(InteractionEvent _event)
    {
        if (_event == null)
            return;
        IndexInit();
        currentEvent = _event.eventName;
        talkData = DialogueData.GetDialogue(currentEvent); //대화 데이터 로드
        SetSentence(index1, index2);

        //if(GameManager.progress < _event.scriptNumber)
        //    GameManager.progress = _event.scriptNumber;
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
        if (talkData[index1].name.Trim() == ".")
        {
            nameBox.SetActive(false);
        }
        else
        {
            nameBox.SetActive(true);
        }
        speaker.text = talkData[index1].name;
        //context.text = talkData[index1].constexts[index2]; //이름, 내용을 텍스트로 설정
        StartCoroutine(SetContext(talkData[index1].constexts[index2]));

        SetOption(talkData[index1].options[index2]);
        //SetImage(talkData[index1].images[index2]);
        
        if (index1 + 1 >= talkData.Length && index2 + 1 >= talkData[index1].constexts.Length)
            nextImage.SetActive(false);
        else
            nextImage.SetActive(true);
    }

   IEnumerator SetContext(string a)
    {
        isTyping = true;
        context.text = string.Empty;
        for(int i=0; i<a.Length; i++)
        {
            context.text += a[i];
            yield return new WaitForSeconds(typingDelay);
        }
        isTyping = false;

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
            haveOption = true;
        }
        else
        {
            optionsParent.SetActive(false); //선택지 없으면 부모 비활성화
            haveOption = false;
        }
            
    }

    public void SetImage(string imageName)
    {
        if(imageName.Trim()!="")
        {
            faceImage.SetActive(true);
            string PATH = "Sprites/" + imageName.Trim();
            faceImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(PATH);
            context.GetComponent<RectTransform>().anchoredPosition = new Vector3(300, 0, 0);
            context.GetComponent<RectTransform>().sizeDelta = new Vector2(1500, 200);
        }
        else
        {
            faceImage.SetActive(false);
            context.GetComponent<RectTransform>().anchoredPosition = new Vector3(20, 0, 0);
            context.GetComponent<RectTransform>().sizeDelta = new Vector2(1800, 200);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && uiManager.currentUI == UIType.talk && !haveOption && !isTyping)
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
            else if(!haveOption)//대사 끝 && 선택지가 없다면
            {
                uiManager.setInActiveUI(); //UI 비활성화
            }
            //Debug.Log(index1 + " " + index2);
        }
        else
            isKeyDown = false;
    }
}
