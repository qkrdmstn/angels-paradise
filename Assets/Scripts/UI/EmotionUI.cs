using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionUI : MonoBehaviour
{
    private PlayerEmotion playerEmotion;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        playerEmotion = FindObjectOfType<PlayerEmotion>();
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
        playerEmotion.SetPlayerEmotion(PlayerEmotion.playerEmotions.glad);
        StartCoroutine(InactiveUIcoroutine());
    }
    public void SetPlayerEmotionSad()
    {
        playerEmotion.SetPlayerEmotion(PlayerEmotion.playerEmotions.sad);
        StartCoroutine(InactiveUIcoroutine());
    }
    public void SetPlayerEmotionJoy()
    {
        playerEmotion.SetPlayerEmotion(PlayerEmotion.playerEmotions.joy);
        StartCoroutine(InactiveUIcoroutine());
    }
    public void SetPlayerEmotionAngry()
    {
        playerEmotion.SetPlayerEmotion(PlayerEmotion.playerEmotions.angry);
        StartCoroutine(InactiveUIcoroutine());
    }


    IEnumerator InactiveUIcoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        uiManager.currentUI = UIType.none;
        gameObject.SetActive(false);
    }
}
