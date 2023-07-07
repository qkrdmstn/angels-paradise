using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text talkText;
    public GameObject scanObject;

    public void Action()
    {
        //scanObj = scanObj;
        talkText.text = "안녕하세요";
    }
}
