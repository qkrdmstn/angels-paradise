using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public int progress { get; set; }
    public int progress1 { get; set; } //备开 1 柳青档
    public int progress2 { get; set; } //备开 2 柳青档
    public int progress3 { get; set; } //备开 3 柳青档

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
        progress = 0;
    }
}
