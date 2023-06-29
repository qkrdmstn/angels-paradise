using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject abilityUI;
    public GameObject emotionUI;
    GameObject player;
    public bool isActiveUI = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //abilityUI
        if(Input.GetKey(KeyCode.E) && !isActiveUI)
        {
            isActiveUI = true;
            abilityUI.transform.position = Camera.main.WorldToScreenPoint(player.transform.position + new Vector3(0, 0.75f, 0));
            abilityUI.SetActive(true);

            
        }

        //emotionUI
        if (Input.GetKey(KeyCode.F) && !isActiveUI)
        {
            isActiveUI = true;
            emotionUI.transform.position = Camera.main.WorldToScreenPoint(player.transform.position + new Vector3(0, 0.75f, 0));
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
