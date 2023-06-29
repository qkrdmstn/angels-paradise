using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmotion : MonoBehaviour
{
    //Player Emotion
    public enum playerEmotions
    {
        fine,
        glad,
        sad,
        joy,
        angry
    }
    private playerEmotions currentEmotion;

    public void SetPlayerEmotion(playerEmotions e)
    {
        currentEmotion = e;
        Debug.Log(currentEmotion);
    }
    public playerEmotions GetPlayerEmotion()
    {
        return currentEmotion;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
