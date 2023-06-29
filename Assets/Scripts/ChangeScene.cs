using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            SceneManager.LoadScene("SuperPowerScene");
        }

        if (Input.GetKey(KeyCode.N))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
