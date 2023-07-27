using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct TalkData
{
    public string name;
    public string[] constexts;
    public string[] options;
}

[System.Serializable]
public class DialogueData : MonoBehaviour
{
    ////대화 이벤트 이름
    //[SerializeField] string eventName;
    //위에서 선언한 TalkData 배열
    TalkData[] talkDatas;
    public static Dictionary<string, TalkData[]> DialogueDictionary = new Dictionary<string, TalkData[]>();
    public TextAsset[] csvFile = null;
    public string tempText;
    public string csvText;
    public string[] rows;

    // Start is called before the first frame update
    void Awake()
    {
        SetDialogueDictionary();      
    }

    public void SetDialogueDictionary()
    {
        
        for(int i=0; i<csvFile.Length; i++)
        {
            csvText = csvFile[i].text.Substring(0, csvFile[i].text.Length - 1); //마지막 빈 줄 제거
            if (i > 0)
                tempText += '\n';
            tempText += csvText;
        }
        csvText = tempText;

        rows = csvText.Split(new char[] { '\n' }); //줄 단위로 나누기
        
        for(int i=0; i<rows.Length; i++)
        {
            rows[i] = rows[i].Replace("\\n", "\n");
            rows[i] = rows[i].Replace("\"\"\"", "\"");
        }

        for (int i = 1; i < rows.Length; i++)
        {
            string[] rowValues = rows[i].Split(new char[] { ',' });
            if (rowValues[0].Trim() == "" || rowValues[0].Trim() == "end" || rowValues[0].Trim() == "EventName") //유효 이벤트 이름이 아닐 경우 continue;
                continue;

            List<TalkData> talkDataList = new List<TalkData>();
            string eventName = rowValues[0]; //유효 이벤트 이름일 경우 저장

            while (rowValues[0].Trim() != "end")
            {
                List<string> contextList = new List<string>();
                List<string> optionList = new List<string>();

                TalkData talkData;
                talkData.name = rowValues[1]; //구조체에 이름 저장

                do
                {
                    contextList.Add(rowValues[2].ToString());  //대사 저장
                    optionList.Add(rowValues[3].ToString());
                    if (rowValues[3].Trim() != "") //선택지
                        Debug.Log("선택지");

                    if (++i < rows.Length)
                    {
                        rowValues = rows[i].Split(new char[] { ',' });  //다음 대사도 나누기
                        for (int j=0; j<rowValues.Length; j++)
                        {
                            rowValues[j] = rowValues[j].Replace("@", ",");
                        }
                    }  
                    else break;

                } while (rowValues[1] == "" && rowValues[0] != "end"); //같은 인물이 말하는 동안 반복

                talkData.constexts = contextList.ToArray(); 
                talkData.options = optionList.ToArray(); //이름, 대사, 선택지로 묶어서 talkData 구조체 완성
                talkDataList.Add(talkData); //하나의 이벤트에 해당하는 대사들 저장
            }
            DialogueDictionary.Add(eventName, talkDataList.ToArray()); //이벤트 이름 - 대사들 로 key,value 묶어서 딕셔너리 추가
        }
    }

    public static TalkData[] GetDialogue(string eventName)
    {
        return DialogueDictionary[eventName];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
