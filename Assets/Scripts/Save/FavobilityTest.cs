using UnityEngine;

public class FavobilityTest : MonoBehaviour
{
    public int favorability = 0;

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ȣ���� �ҷ�����
        favorability = DataManager.instance.nowPlayer.favorability;
    }

    // Update is called once per frame
    void Update()
    {
        DataManager.instance.nowPlayer.favorability = favorability;
    }

    public void favorbilityUp() // ȣ���� ���� �׽�Ʈ��. �Ű�X
    {
        favorability++; // ���� ȣ������ 1 ������Ŵ
        DataManager.instance.nowPlayer.favorability = favorability; // DM�� �ٷ� ����
        DataManager.instance.SaveData(); // DataManager�� �ҷ���� ��
        Debug.Log("ȣ���� : " + DataManager.instance.nowPlayer.favorability);
    }
}
