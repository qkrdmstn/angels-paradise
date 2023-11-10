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
    private bool isRedActive = true;  // �ʱ⿡�� ������ Ȱ��ȭ
    private int interactionCount = 0;  // FootHold�� ��ȣ�ۿ��� Ƚ���� ����
    private GameObject[] redObstacles;  // ������ ������Ʈ ����Ʈ
    private GameObject[] yellowObstacles;  // ����� ������Ʈ ����Ʈ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        redObstacles = GameObject.FindGameObjectsWithTag("RedObstacle");
        yellowObstacles = GameObject.FindGameObjectsWithTag("YellowObstacle");
    }

    void Update()
    {
        // Input.GetAxis�� ����Ͽ� ������ �����մϴ�.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // ����Ű �Ǵ� WASD Ű �� �ϳ��� �ԷµǸ� �̵��մϴ�.
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
                // foothold�� ��ȣ�ۿ��� �ϸ� ���� -> ��� -> ���� .... ������ onoff
                Debug.Log(rayHit.collider);  // Ȯ�ο� �α� �߰�
                ToggleObstacles();
                Debug.Log("Ȯ�ο�");
            }
        }
    }

    void ToggleObstacles()
    {
        // ���� ���¿� ���� ������ �Ǵ� ������� Ȱ��ȭ�մϴ�.
        foreach (GameObject redObstacle in redObstacles)
        {
            redObstacle.SetActive(!isRedActive);
        }

        foreach (GameObject yellowObstacle in yellowObstacles)
        {
            yellowObstacle.SetActive(isRedActive);
        }

        // ������ ����� ���� ������Ʈ�մϴ�.
        interactionCount++;

        // interactionCount�� 2�� ����� ������ isRedActive�� ������ŵ�ϴ�.
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
        // �̵� �߿� �浹 �߻� �� �̵��� ����ϴ�.
        isMoving = false;
        rb.velocity = Vector2.zero;
    }
}
