using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointUI : MonoBehaviour
{
    Slider pointSlider;
    GameObject fillArea;
    // Start is called before the first frame update
    void Awake()
    {
        pointSlider = gameObject.GetComponent<Slider>();
        fillArea = transform.Find("Fill Area").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //pointSlider.value = GameManager.Instance.point;
        if(pointSlider.value <= -100)
        {
            if(fillArea.activeSelf)
            {
                Debug.Log("asd");
                transform.Find("Fill Area").gameObject.SetActive(false);

            }
        }
        else
        {
            if (!fillArea.activeSelf)
            {
                Debug.Log("asd");
                transform.Find("Fill Area").gameObject.SetActive(true);

            }
        }
    }
}
