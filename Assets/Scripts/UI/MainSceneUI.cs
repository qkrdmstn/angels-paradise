using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainSceneUI : MonoBehaviour
{
    public GameObject loadSlot;
    public Text[] slotText; // slot ��������� ���� ����
    bool[] saveFile = new bool[5]; // ���̺� ���� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        // ���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�
        for (int i=0; i < 5; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))
            {
                saveFile[i] = true; // ���Կ� �ִ��� üũ�ϰ�
                DataManager.instance.nowSlot = i; // �� ��° ��������
                DataManager.instance.LoadData(); // �� ���� ������ ������
                slotText[i].text = "�׽�Ʈ�Դϴ�";
            }
            else
            {
                slotText[i].text = "�������";
            }
        }
        DataManager.instance.DataClear();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            loadSlot.gameObject.SetActive(false);
        }
    }

    public void Slot(int number) // ���� ���� �Ű�����
    {
        DataManager.instance.nowSlot = number;

        if (saveFile[number]) // ����� �����Ͱ� �ִٸ�
        {
            // ���� ������ �Ѿ����
            DataManager.instance.LoadData();
            GoGame();
        }
        else // �����Ͱ� ���ٸ� �� ������ ���� ���� ����
        {
            // �ҷ����� â ��Ȱ��ȭ ����
            DataManager.instance.SaveData();
            saveFile[number] = true;
            slotText[number].text = "�����";
            GoGame();
            Debug.Log("����");
        }      
        
    }

    public void OpenLoadSlot()
    {
        loadSlot.gameObject.SetActive(true);
    }

    public void GoGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Save()
    {
        DataManager.instance.SaveData();
    }
}