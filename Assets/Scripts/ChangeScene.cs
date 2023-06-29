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

        if (Input.GetKey(KeyCode.O))
        {
            SceneManager.LoadScene("ElectricityScene");
        }

        if (Input.GetKey(KeyCode.I))
        {
            SceneManager.LoadScene("HackingScene");
        }
    }
}
