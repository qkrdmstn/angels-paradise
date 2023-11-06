using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public int progress { get; set; } //무시

    public int [] etcProgress { get; set; } //기타 퍼즐 진행도 / 0: 쪽지, 1: 수다쟁이 부인, 2: 기찻길
    public int [] progress1 { get; set; } //구역 1 퍼즐 진행도 / 크기: 3
    public int [] progress2 { get; set; } //구역 2 퍼즐 진행도 / 진행도 크기: 5
    public int [] progress3 { get; set; } //구역 3 퍼즐 진행도 / 진행도 크기: 7

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
      
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Initialized();
    }
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void Initialized()
    {
        etcProgress = new int[3];
        progress1 = new int[3];
        progress2 = new int[5];
        progress3 = new int[7];

    }
}
