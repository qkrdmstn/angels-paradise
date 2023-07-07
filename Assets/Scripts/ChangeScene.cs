using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    Player thePlayer;
    private void Start()
    {
        thePlayer = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            SceneManager.LoadScene("SuperPowerScene");
        }

        if (Input.GetKey(KeyCode.B))
        {
            SceneManager.LoadScene("MagnetScene");
        }

        if (Input.GetKey(KeyCode.N))
        {
            SceneManager.LoadScene("1. StartScene");
        }

        if (Input.GetKey(KeyCode.O))
        {
            SceneManager.LoadScene("ElectricityScene");
        }

        if (Input.GetKey(KeyCode.I))
        {
            SceneManager.LoadScene("HackingScene");
            thePlayer.cameraSetting = false;
        }
    }
}
