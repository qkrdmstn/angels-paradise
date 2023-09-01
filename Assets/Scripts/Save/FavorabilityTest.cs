using UnityEngine;

public class FavorabilityTest : MonoBehaviour
{
    public int favorability = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 호감도 불러오기
        favorability = DataManager.instance.nowPlayer.favorability;
    }

    // Update is called once per frame
    void Update()
    {
        DataManager.instance.nowPlayer.favorability = favorability;
    }

    public void favorbilityUp() // 호감도 저장 테스트용. 신경X
    {
        favorability++; // 현재 호감도를 1 증가시킴
        DataManager.instance.nowPlayer.favorability = favorability; // DM에 바로 저장
        DataManager.instance.SaveData(); // DataManager를 불러줘야 함
        Debug.Log("호감도 : " + DataManager.instance.nowPlayer.favorability);
    }
}
