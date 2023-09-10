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
    public string[] images;
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

    //public List<string> optionList = new List<string>();
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
        }

        for (int i = 1; i < rows.Length; i++)
        {
            string[] rowValues = rows[i].Split(new char[] { ',' });
            if (rowValues[0].Trim() == "" || rowValues[0].Trim() == "end" || rowValues[0].Trim() == "EventName") //유효 이벤트 이름이 아닐 경우 continue;
                continue;

            List<TalkData> talkDataList = new List<TalkData>();
            string eventName = rowValues[0]; //유효 이벤트 이름일 경우 저장
            contextModify(rowValues);

            while (rowValues[0].Trim() != "end")
            {
                List<string> contextList = new List<string>();
                List<string> optionList = new List<string>();
                List<string> imageList = new List<string>();

                TalkData talkData;
                talkData.name = rowValues[1]; //구조체에 이름 저장

                do
                {
                    contextList.Add(rowValues[2].ToString());  //대사 저장
                    optionList.Add(rowValues[3].ToString()); //선택지 저장
                    imageList.Add(rowValues[4].ToString());
                    //Debug.Log(i); 에러 찾기

                    if (++i < rows.Length)
                    {
                        rowValues = rows[i].Split(new char[] { ',' });  //다음 대사도 나누기

                        contextModify(rowValues);


                    }
                    else break;

                } while (rowValues[1] == "" && rowValues[0] != "end"); //같은 인물이 말하는 동안 반복

                talkData.constexts = contextList.ToArray(); 
                talkData.options = optionList.ToArray(); 
                talkData.images = imageList.ToArray(); //이름, 대사, 선택지, 이미지로 묶어서 talkData 구조체 완성
                talkDataList.Add(talkData); //하나의 이벤트에 해당하는 대사들 저장
            }
            DialogueDictionary.Add(eventName, talkDataList.ToArray()); //이벤트 이름 - 대사들 로 key,value 묶어서 딕셔너리 추가
        }
    }

    public static TalkData[] GetDialogue(string eventName)
    {
        if(DialogueDictionary.ContainsKey(eventName))
            return DialogueDictionary[eventName];
        else
        {
            Debug.LogWarning("유효하지 않은 이벤트 이름 :" + eventName);
            return null;
        }
    }

    public void contextModify(string[] rowValues) //CSV 파일 예외처리 ex) 따옴표, 쉼표 처리 등
    {
        int lastIndex = rowValues[2].Length - 1;
        if (lastIndex < 0)
            return;
        if (rowValues[2][0] == '\"' && rowValues[2][lastIndex] == '\"')//처음과 끝 시작이 ""라면 그거 제거 -> 2x+1=t, x=(t-1)/2
        {
            //Debug.Log(i);
            rowValues[2] = rowValues[2].Remove(lastIndex, 1);
            rowValues[2] = rowValues[2].Remove(0, 1);
        }
        rowValues[2] = rowValues[2].Replace("\"\"", "\""); //2배 된 거 제거
        rowValues[2] = rowValues[2].Replace("@", ","); //대사의 골뱅이를 쉼표로 변환
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
