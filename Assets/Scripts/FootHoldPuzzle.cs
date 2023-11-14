using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootHoldPuzzle : MonoBehaviour
{
    [Header("발판 퍼즐 관련")]
    private bool isRedActive = true;  // 초기에는 빨강이 활성화
    private int interactionCount = 0;  // FootHold와 상호작용한 횟수를 추적
    private GameObject[] redObstacles;  // 빨간색 오브젝트 리스트
    private GameObject[] yellowObstacles;  // 노란색 오브젝트 리스트

    // Start is called before the first frame update
    void Start()
    {
        redObstacles = GameObject.FindGameObjectsWithTag("RedObstacle");
        yellowObstacles = GameObject.FindGameObjectsWithTag("YellowObstacle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Player와 Foothold가 충돌할 경우 토글
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트의 태그를 확인
        if (other.CompareTag("Player"))
        {
            // player 태그를 가진 오브젝트와 foothold 태그를 가진 오브젝트가 충돌했을 때 실행할 코드 작성
            ToggleObstacles();
            // 예를 들어, 플레이어가 발판에 올라갔을 때의 동작을 추가할 수 있습니다.
            Debug.Log("발판 밟음!");
        }
    }

    void ToggleObstacles()
    {
        // 현재 상태에 따라 빨간색 또는 노란색을 활성화합니다.
        foreach (GameObject redObstacle in redObstacles)
        {
            redObstacle.SetActive(!isRedActive);
        }

        foreach (GameObject yellowObstacle in yellowObstacles)
        {
            yellowObstacle.SetActive(isRedActive);
        }

        // 다음에 토글할 턴을 업데이트합니다.
        interactionCount++;

        // interactionCount가 2의 배수일 때마다 isRedActive를 반전시킵니다.
        isRedActive = interactionCount % 2 == 0;
    }
}
