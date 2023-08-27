using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class test : MonoBehaviour
{
    public string[] inputText;
    public string[] outputText;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inputText.Length; i++)
        {
            int last = inputText[i].Length - 1;

            if (inputText[i][last] == '\"' && inputText[i][0] == '\"') //처음과 끝 시작이 ""라면 그거 제거 -> 2x+1=t, x=(t-1)/2
            {
                inputText[i] = inputText[i].Remove(last, 1);
                inputText[i] = inputText[i].Remove(0, 1);

            }
            outputText[i] = inputText[i].Replace("\"\"", "\""); //2배 된 거 제거
        }
    }

    // Update is called once per frame
    void Update()
    {
        //outputText = inputText.Replace("\"\"", "\"");
    }
}