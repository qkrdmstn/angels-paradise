using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5.0f;
    private bool isMoving = false;
    private Vector2 moveDirection = Vector2.zero;
    public float interactionDistance = 1.5f;
    private Animator animator;
    private bool isRedActive = true;  // 초기에는 빨강이 활성화
    private int interactionCount = 0;  // FootHold와 상호작용한 횟수를 추적
    private GameObject[] redObstacles;  // 빨간색 오브젝트 리스트
    private GameObject[] yellowObstacles;  // 노란색 오브젝트 리스트

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        redObstacles = GameObject.FindGameObjectsWithTag("RedObstacle");
        yellowObstacles = GameObject.FindGameObjectsWithTag("YellowObstacle");
    }

    void Update()
    {
        // Input.GetAxis를 사용하여 방향을 감지합니다.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 방향키 또는 WASD 키 중 하나라도 입력되면 이동합니다.
        if (!isMoving && (horizontalInput != 0 || verticalInput != 0))
        {
            moveDirection = new Vector2(horizontalInput, verticalInput);
            isMoving = true;
        }

        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, moveDirection, interactionDistance, LayerMask.GetMask("Object"));
        Debug.DrawRay(rb.position, moveDirection.normalized * interactionDistance, Color.green);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rayHit.collider != null && rayHit.collider.CompareTag("FootHold"))
            {
                // foothold와 상호작용을 하면 빨강 -> 노랑 -> 빨강 .... 순으로 onoff
                Debug.Log(rayHit.collider);  // 확인용 로그 추가
                ToggleObstacles();
                Debug.Log("확인용");
            }
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

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.velocity = moveDirection.normalized * moveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 이동 중에 충돌 발생 시 이동을 멈춥니다.
        isMoving = false;
        rb.velocity = Vector2.zero;
    }
}
