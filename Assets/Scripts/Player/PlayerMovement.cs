using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("�̵� ����")]
    public Rigidbody2D rb; // portal t/f ������ public���� ����
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
        // Input.GetAxis�� ����Ͽ� ������ �����մϴ�.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // ����Ű �Ǵ� WASD Ű �� �ϳ��� �ԷµǸ� �̵��մϴ�.
        if (!isMoving && (horizontalInput != 0 || verticalInput != 0))
        {
            
            moveDirection = new Vector3(horizontalInput, verticalInput,0);
            isMoving = true;
        }

        // �����̽��ٷ� ��ư ���� ��� (Istrigger X)
        /*RaycastHit2D rayHit = Physics2D.Raycast(rb.position, moveDirection, interactionDistance, LayerMask.GetMask("Object"));
        Debug.DrawRay(rb.position, moveDirection.normalized * interactionDistance, Color.green);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rayHit.collider != null && rayHit.collider.CompareTag("FootHold"))
            {
                // foothold�� ��ȣ�ۿ��� �ϸ� ���� -> ��� -> ���� .... ������ onoff
                Debug.Log(rayHit.collider);  // Ȯ�ο� �α� �߰�
                ToggleObstacles();
                Debug.Log("Ȯ�ο�");
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �̵� �߿� �浹 �߻� �� �̵��� ����ϴ�.
        isMoving = false;
        rb.velocity = Vector3.zero;
    }
}
