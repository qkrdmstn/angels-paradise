using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class test : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + "A");
    }

    // Update is called once per frame
    void Update()
    {
        //outputText = inputText.Replace("\"\"", "\"");
    }
}