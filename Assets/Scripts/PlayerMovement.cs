using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5.0f;
    private Vector2 moveDirection = Vector2.zero;
    private bool isMoving = false;
    private Vector3 vector;
    public float interactionDistance = 1.5f;
    private PlayerAbility playerAbility;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAbility = FindObjectOfType<PlayerAbility>();
    }

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveDirection = Vector2.left;
                isMoving = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveDirection = Vector2.right;
                isMoving = true;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveDirection = Vector2.up;
                isMoving = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveDirection = Vector2.down;
                isMoving = true;
            }
        }
        vector = moveDirection;
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, vector, interactionDistance, LayerMask.GetMask("Object")); //레이에 닿는 모든 콜라이더 정보 저장
        Debug.DrawRay(rb.position, vector.normalized * interactionDistance, Color.green);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rayHit.collider.CompareTag("SuperPowerObj")/* && playerAbility.GetPlayerAbility() == PlayerAbility.playerAbilities.superPower*/)
            {
                playerAbility.SuperPowerInteraction(rayHit);
            }

        }
    }
    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.velocity = moveDirection * moveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 이동 중에 충돌 발생 시 이동을 멈춥니다.
        isMoving = false;
        rb.velocity = Vector2.zero;
    }

}