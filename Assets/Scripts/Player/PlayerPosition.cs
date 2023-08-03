using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    // 플레이어 위치 저장, 불러오기
    private Vector3 playerPosition = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 위치 불러오기
        playerPosition = DataManager.instance.nowPlayer.playerPosition;
        transform.position = playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어 위치 저장
        Vector3 playerPosition = transform.position;
        DataManager.instance.nowPlayer.playerPosition = playerPosition;        
    }
}
