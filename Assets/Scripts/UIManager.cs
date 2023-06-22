using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject abilityUI;
    public GameObject emotionUI;

    public bool isActiveUI = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //abilityUI
        if(Input.GetKey(KeyCode.E) && !isActiveUI)
        {
            isActiveUI = true;
            abilityUI.SetActive(true);
        }

        //emotionUI
        if (Input.GetKey(KeyCode.F) && !isActiveUI)
        {
            isActiveUI = true;
            emotionUI.SetActive(true);
        }

        if(Input.GetKey(KeyCode.Escape) && isActiveUI)
        {
            abilityUI.SetActive(false);
            emotionUI.SetActive(false);

            isActiveUI = false;
        }
    }

}
