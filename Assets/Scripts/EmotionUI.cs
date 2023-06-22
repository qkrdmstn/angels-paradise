using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionUI : MonoBehaviour
{
    private Player player;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            SetPlayerEmotionGlad();
        else if (Input.GetKey(KeyCode.D))
            SetPlayerEmotionSad();
        else if (Input.GetKey(KeyCode.S))
            SetPlayerEmotionJoy();
        else if (Input.GetKey(KeyCode.A))
            SetPlayerEmotionAngry();


    }
    public void SetPlayerEmotionGlad()
    {
        player.SetPlayerEmotion(Player.PlayerEmotion.glad);
        StartCoroutine(InactiveUIcoroutine());
    }
    public void SetPlayerEmotionSad()
    {
        player.SetPlayerEmotion(Player.PlayerEmotion.sad);
        StartCoroutine(InactiveUIcoroutine());
    }
    public void SetPlayerEmotionJoy()
    {
        player.SetPlayerEmotion(Player.PlayerEmotion.joy);
        StartCoroutine(InactiveUIcoroutine());
    }
    public void SetPlayerEmotionAngry()
    {
        player.SetPlayerEmotion(Player.PlayerEmotion.angry);
        StartCoroutine(InactiveUIcoroutine());
    }


    IEnumerator InactiveUIcoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        uiManager.isActiveUI = false;
        gameObject.SetActive(false);
    }
}
