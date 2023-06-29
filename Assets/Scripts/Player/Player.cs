using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager manager;
    private UIManager uiManager;
    //Player Movment
    public float verticalInput, horizonInput;
    public float speed, runSpeed;
    private Vector3 vector;
    private bool keyDown = false;
    int walkCount = 10;
    private Animator animator;
    private Rigidbody2D rigid;
    GameObject scanObject;

    //Camera Setting
    Camera theCamera;
    public bool cameraSetting;

    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift)) //�޸���
            {
                animator.SetBool("Running", true);
                runSpeed = speed * 0.5f;
            }
            else
            {
                runSpeed = 0;
                animator.SetBool("Running", false);
            }

            verticalInput = Input.GetAxisRaw("Vertical");
            horizonInput = Input.GetAxisRaw("Horizontal");
            vector.Set(horizonInput * (speed + runSpeed), verticalInput * (speed + runSpeed), transform.position.z);

            //if (vector.x != 0)  //�밢�� �̵� ����
            //    vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            for (int i = 0; i < walkCount; i++)
            {
                transform.Translate(vector);
                yield return new WaitForSeconds(0.01f);
            }
        }

        animator.SetBool("Walking", false);
        animator.SetBool("Running", false);
        keyDown = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        uiManager = FindObjectOfType<UIManager>();
        theCamera = FindObjectOfType<Camera>();
        cameraSetting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!keyDown && !uiManager.isActiveUI)
        {

            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                animator.SetBool("Walking", true);
                keyDown = true;
                StartCoroutine(MoveCoroutine());
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
            manager.Action();

        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 확인
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("NPC"))
            {
                Debug.Log("NPC 마우스클릭");
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("테스트트");
        }

    }

    void FixedUpdate()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, vector.normalized, 2f);
        Debug.DrawRay(rigid.position, vector.normalized * 2f, Color.green);

        if (rayHit.collider != null && rayHit.collider.CompareTag("NPC"))
        {
            Debug.Log("NPC 스페이스바");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!cameraSetting && collision.CompareTag("CameraBound"))
        {
            theCamera.GetComponent<CameraManager>().SetBound(collision.GetComponent<BoxCollider2D>());
        }

    }
}