using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainSceneUI : MonoBehaviour
{
    public GameObject loadSlot;
    public GameObject loadBtn;
    public Text[] slotText; // slot 비어있음을 쓰기 위해
    bool[] saveFile = new bool[5]; // 세이브 파일 존재 유무
    string[] slotTextInfo = new string[5];

    // Start is called before the first frame update
    void Start()
    {
        SlotSaveData();
    }

    void SlotSaveData()
    {
        // 슬롯별로 저장된 데이터가 존재하는지 판단
        for (int i = 0; i < 5; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))
            {
                saveFile[i] = true; // 슬롯에 있는지 체크하고
                DataManager.instance.nowSlot = i; // 몇 번째 슬롯인지
                DataManager.instance.LoadData(); // 그 슬롯 데이터 가져옴
                slotText[i].text = i + "저장 테스트";
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }
        //DataManager.instance.DataClear();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            loadSlot.gameObject.SetActive(false);
        }
    }

    public void LoadSlot(int number) // 슬롯 숫자 매개변수
    {
        DataManager.instance.nowSlot = number;

        if (saveFile[number]) // 저장된 데이터가 있다면
        {
            // 게임 씬으로 넘어가도록
            DataManager.instance.LoadData();
            GoGame();
            Debug.Log(number + "로드함");
        }
    }

    public void SaveSlot(int number)
    {
        DataManager.instance.nowSlot = number;

        if (!saveFile[DataManager.instance.nowSlot]) // 현재 슬롯 번호의 데이터가 없다면
        {
            DataManager.instance.SaveData(); // 현재 정보를 저장함
            Debug.Log(number + "저장함");
        }
    }

    public void OpenLoadSlot()
    {
        loadSlot.gameObject.SetActive(true);
    }

    public void OpenLoadBtn()
    {
        loadBtn.gameObject.SetActive(true);
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
