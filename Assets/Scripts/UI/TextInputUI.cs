using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInputUI : MonoBehaviour
{
    public InputField inputField;
    public UIManager uiManager;
    private string currentEvent; //현재 이벤트 상태
    private string gladEmotionPassword = "E311y";

    public void SetCurrentEvent(InteractionEvent _event)
    {
        currentEvent = _event.eventName;
        inputField.onSubmit.RemoveAllListeners(); //입력 이벤트 초기화

        switch (_event.eventName)
        {
            case "기쁨 감정 받기 암호":
                inputField.onSubmit.AddListener(delegate { Password(gladEmotionPassword); });
                break;
        }
    }

    public void UIClose()
    {
        uiManager.setInActiveUI();
        Debug.Log("UI 닫힘");

    }

    public void Password(string password)
    {
        if(inputField.text == gladEmotionPassword)
        {
            LoadNewDialogue("기쁨 감정 받기");
            uiManager.textInputUI.SetActive(false);
        }
        else
        {
            LoadNewDialogue("암호 틀림");
            uiManager.textInputUI.SetActive(false);
        }
    }

    public void LoadNewDialogue(string eventName)
    {
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent(eventName); //UI로 event 전달
        uiManager.setActiveUI(UIType.talk); //UI 활성화
    }

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
}

