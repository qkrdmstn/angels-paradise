using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class test : MonoBehaviour
{
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            GameManager.Instance.etcProgress[0] = 2;
            gameObject.transform.position = new Vector3(3, 17, 0);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameManager.Instance.etcProgress[0] = 5;
            gameObject.transform.position = new Vector3(260, 54, 0);
        }
    }
}