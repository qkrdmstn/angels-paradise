
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// �����ϴ� ���
// 1. ������ �����Ͱ� ����
// 2. �����͸� ���̽����� ��ȯ
// 3. ���̽��� �ܺο� ���� -> IO����

// �ҷ����� ���
// 1. �ܺο� ����� ���̽��� ������
// 2. ���̽��� ���������·� ��ȯ
// 3. �ҷ��� �����͸� ���

// ������ ������ : �÷��̾� ��ġ, �κ��丮 ������, ȣ����
[System.Serializable]
public class PlayerData
{
    public Vector3 playerPosition; // �÷��̾� ��ġ
    public List<Item> items; // �κ��丮 �� ������
    public int favorability; // ȣ����
    public int story; // ���丮 ���൵
}
public class DataManager : MonoBehaviour
{
    public PlayerData nowPlayer = new PlayerData(); // �÷��̾� ������ ����

    public static DataManager instance; // �̱���
    public string path; // ���� ���
    public int nowSlot; // ���� ��ȣ
    private void Awake()
    {
        /*#region �̱���
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion*/

        path = Application.persistentDataPath + "/save"; // ����Ƽ���� �������ִ� ���, /�� filename ������ ���� �� �ֱ� ������ �ۼ�
        // �Ƹ� ���� ���� �ɰ���
        // C:\Users\�����\AppData\LocalLow\DefaultCompany
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer); // Json�� string���̶�� ��
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data); // json(str) -> data
    }
    public void DataClear() // �ҷ��� ������ �������ִ� �Լ�
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
