
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// 저장하는 방법
// 1. 저장할 데이터가 존재
// 2. 데이터를 제이슨으로 변환
// 3. 제이슨을 외부에 저장 -> IO선언

// 불러오는 방법
// 1. 외부에 저장된 제이슨을 가져옴
// 2. 제이슨을 데이터형태로 변환
// 3. 불러온 데이터를 사용

// 저장할 데이터 : 플레이어 위치, 인벤토리 아이템, 호감도
[System.Serializable]
public class PlayerData
{
    public Vector3 playerPosition; // 플레이어 위치
    public List<Item> items; // 인벤토리 안 아이템
    public int favorability; // 호감도
    public int story; // 스토리 진행도
}
public class DataManager : MonoBehaviour
{
    public PlayerData nowPlayer = new PlayerData(); // 플레이어 데이터 생성

    public static DataManager instance; // 싱글톤
    public string path; // 저장 경로
    public int nowSlot; // 슬롯 번호
    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/save"; // 유니티에서 지정해주는 경로, /는 filename 오류가 있을 수 있기 때문에 작성
        // 아마 여기 저장 될거임
        // C:\Users\사용자\AppData\LocalLow\DefaultCompany
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer); // Json은 string형이라고 함
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data); // json(str) -> data
    }
    public void DataClear() // 불러온 값들을 리셋해주는 함수
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
