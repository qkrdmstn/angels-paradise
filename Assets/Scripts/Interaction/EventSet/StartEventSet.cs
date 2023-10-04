using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StartEventSet : MonoBehaviour //��ŸƮ ���� ������ ����
{
    public Button[] optionButton;
    private UIManager uiManager;
    private GameObject player;
    private FadeManager theFade;
    private Inventory inventory;

    public void SetOptionEvent(string eventName, int num)
    {
        InitOptionEvent(); //������ ��ϵ� OnClickEvent ����
        UnityAction[] actionNames = new UnityAction[num];
        switch (eventName)
        {
            case "���ӽ���1":
                actionNames[0] = () => LoadNewDialogue("���ӽ���3"); //��
                actionNames[1] = () => LoadNewDialogue("���ӽ���2"); //�ƴ�
                break;
            case "���ӽ���2":
                actionNames[0] = () => LoadNewDialogue("���ӽ���3"); //��
                actionNames[1] = () => LoadNewDialogue("���ӽ���2"); //�ƴ�
                break;
            case "�������� ���� ����1":
                actionNames[0] = () => LoadNewDialogue("�������� ���� ����2");
                break;
            case "�������� ���� ����2":
                actionNames[0] = () => LoadNewDialogue("�������� ���� ����3"); //������ ������ �ߵ������� �� ����
                actionNames[1] = () => LoadNewDialogue("�������� ���� ����4"); //�����󿡰� ���� ���� ���� �� ����
                break;
            case "â�� ������":
                actionNames[0] = ExitStorage; //��
                actionNames[1] = UIClose; //�ƴ�
                break;
            case "�ŷ�":
                actionNames[0] = Transaction; //��
                break;
            case "���͸� �ֱ�":
                actionNames[0] = InsertBattery; //��
                break;
            case "���� �ǳ��ֱ�":
                actionNames[0] = GiveHeart; //��
                break;
            case "��� ���� �ޱ�":
                actionNames[0] = gladEmotionComplete; //��
                actionNames[1] = UIClose; //�ƴ�
                break;
            case "��� ��ȭ 1":
                actionNames[0] = () => LoadNewDialogue("��� ��ȭ 2");
                actionNames[1] = () => LoadNewDialogue("��� ��ȭ 3");
                actionNames[2] = () => LoadNewDialogue("��� ��ȭ 4");
                actionNames[3] = () => LoadNewDialogue("��� ��ȭ 5");
                break;
            case "��� ��ȭ 1.5":
                actionNames[0] = () => LoadNewDialogue("��� ��ȭ 2");
                actionNames[1] = () => LoadNewDialogue("��� ��ȭ 3");
                actionNames[2] = () => LoadNewDialogue("��� ��ȭ 4");
                actionNames[3] = () => LoadNewDialogue("��� ��ȭ 5");
                break;
            case "�������� ���� 0":
                actionNames[0] = () => LoadNewDialogue("�������� ���� 1-1");
                actionNames[1] = () => LoadNewDialogue("�������� ���� 1-2");
                actionNames[2] = () => LoadNewDialogue("�������� ���� 1-3");
                break;
            case "�������� ���� 1-2":
                actionNames[0] = () => LoadNewDialogue("�������� ���� 2-1");
                actionNames[1] = () => LoadNewDialogue("�������� ���� 2-2");
                actionNames[2] = () => LoadNewDialogue("�������� ���� 2-3");
                break;
            case "�������� ���� 2-2":
                actionNames[0] = () => LoadNewDialogue("�������� ���� 3-1");
                actionNames[1] = () => LoadNewDialogue("�������� ���� 3-2");
                actionNames[2] = () => LoadNewDialogue("�������� ���� 3-3");
                break;
            default:
                for (int i = 0; i < num; i++)
                    actionNames[i] = UIClose;
                Debug.Log("�ش� �̺�Ʈ ����");
                break;
        }

        EventSystem.current.SetSelectedGameObject(optionButton[num - 1].gameObject); //UI ��Ŀ�� ����
        for (int i = 0; i < num; i++)
        {
            optionButton[i].onClick.AddListener(actionNames[num - i - 1]); //��ư ���� -> �Ʒ����� ��, �̺�Ʈ ���� -> ������ �Ʒ�
        }
    }

    public void InitOptionEvent()
    {
        for (int i = 0; i < 5; i++)
        {
            optionButton[i].onClick.RemoveAllListeners(); //��ư�� ��ϵ� �Լ� �ʱ�ȭ
        }
    }

    public void UIClose()
    {
        uiManager.setInActiveUI();
        Debug.Log("UI ����");

    }

    public void LoadNewDialogue(string eventName)
    {
        UIClose();
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent(eventName); //UI�� event ����
        uiManager.setActiveUI(UIType.talk); //UI Ȱ��ȭ
    }

    public void ExitStorage() //â�� ������
    {
        if (GameManager.Instance.progress < 2)
        {
            LoadNewDialogue("â�� ������");           
        }
        StartCoroutine(ExitStorageCoroutine());    
    }

    IEnumerator ExitStorageCoroutine() //��ũ��Ʈ ������ �ٷ� ������ ���� �ڷ�ƾ
    {
        if (GameManager.Instance.progress < 2)
        {
            yield return new WaitUntil(() => (uiManager.currentUI == UIType.none)); //��ũ��Ʈ ���� ������ ��ٸ���
            GameManager.Instance.progress = 2;
        }
        UIClose();
        theFade.FadeOut();
        yield return new WaitForSeconds(1f);
        player.transform.position = new Vector3(-15, 15, 0);
        theFade.FadeIn();
    }

    public void Transaction() //�ŷ� ������
    {
        Debug.Log("�ŷ�,��");
        inventory.RemoveItem("�������� ��������");
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent("�ŷ�2"); //UI�� event ����
        uiManager.setActiveUI(UIType.talk); //UI Ȱ��ȭ
    }

    public void InsertBattery() //���͸� �ֱ� ������
    {
        inventory.RemoveItem("���͸�");
        if (GameManager.Instance.progress < 5)
            GameManager.Instance.progress = 5;
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent("���� �簡��"); //UI�� event ����
        uiManager.setActiveUI(UIType.talk); //UI Ȱ��ȭ
    }

    public void GiveHeart() //���� �ǳ��ֱ� ������
    {
        inventory.RemoveItem("�ΰ� ����");
        if (GameManager.Instance.progress < 8)
            GameManager.Instance.progress = 8;
        UIClose();
        //���� ��� ���� ���� �÷����� �ִϸ��̼� �߰�
    }

    public void gladEmotionComplete()
    {
        LoadNewDialogue("��� ���� �ޱ� �Ϸ�");
        //���⿡�� ���� �߰�
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
