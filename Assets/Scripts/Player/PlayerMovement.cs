using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("이동 관련")]
    public Rigidbody2D rb; // portal t/f 때문에 public으로 선언
    public bool isMoving = false;
    private Vector3 moveDirection;
    public float moveSpeed = 5.0f;
    public float interactionDistance = 1.5f;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Input.GetAxis를 사용하여 방향을 감지합니다.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 방향키 또는 WASD 키 중 하나라도 입력되면 이동합니다.
        if (!isMoving && (horizontalInput != 0 || verticalInput != 0))
        {
            
            moveDirection = new Vector3(horizontalInput, verticalInput,0);
            isMoving = true;
        }

        // 스페이스바로 버튼 누를 경우 (Istrigger X)
        /*RaycastHit2D rayHit = Physics2D.Raycast(rb.position, moveDirection, interactionDistance, LayerMask.GetMask("Object"));
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
        }*/
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.velocity = moveDirection.normalized * moveSpeed;
            animator.SetFloat("DirX", moveDirection.x);
            animator.SetFloat("DirY", moveDirection.y);
        }
    }

  hgaohgao  void OnCollisionEnter2D(Collision2D collision)
    {
        // 이동 중에 충돌 발생 시 이동을 멈춥니다.
        isMoving = false;
        rb.velocity = Vector3.zero;
    }
}
