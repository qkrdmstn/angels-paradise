using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainSceneUI : MonoBehaviour
{
    public GameObject loadSlot;
    public Text[] slotText; // slot 비어있음을 쓰기 위해
    bool[] saveFile = new bool[5]; // 세이브 파일 존재 유무

    // Start is called before the first frame update
    void Start()
    {
        // 슬롯별로 저장된 데이터가 존재하는지 판단
        for (int i=0; i < 5; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))
            {
                saveFile[i] = true; // 슬롯에 있는지 체크하고
                DataManager.instance.nowSlot = i; // 몇 번째 슬롯인지
                DataManager.instance.LoadData(); // 그 슬롯 데이터 가져옴
                slotText[i].text = "테스트입니다";
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }
        DataManager.instance.DataClear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slot(int number) // 슬롯 숫자 매개변수
    {
        DataManager.instance.nowSlot = number;

        if (saveFile[number]) // 저장된 데이터가 있다면
        {
            // 게임 씬으로 넘어가도록
            DataManager.instance.LoadData();
            GoGame();
        }
        else
        {
            // 불러오기 창 비활성화 상태
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
}
