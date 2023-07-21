using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class test : MonoBehaviour
{
    public string inputText;
    public string outputText;
    // Start is called before the first frame update
    void Start()
    {

        //inputText.Replace("\n", "<br>");

        outputText = inputText.Replace("\\n", "\n");
        this.GetComponent<Text>().text = inputText;
        //Debug.Log(this.GetComponent<Text>().text);
        //ttext = this.GetComponent<Text>().text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
