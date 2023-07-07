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
    Inventory inventory;

    //Camera Setting
    Camera theCamera;
    public bool cameraSetting;

    private PlayerAbility playerAbility;

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
        inventory = GetComponent<Inventory>();
        playerAbility = GameObject.Find("Player").GetComponent<PlayerAbility>();
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

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, vector, 3f, LayerMask.GetMask("Object"));
        Debug.DrawRay(rigid.position, vector * 3f, Color.green);

        // 아이템 줍기
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (rayHit.collider != null && rayHit.collider.CompareTag("FieldItem"))
            {
                FieldItems fieldItems = rayHit.collider.GetComponent<FieldItems>();
                if (inventory.AddItem(fieldItems.GetItem()))
                {
                    fieldItems.DestroyItem();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Space -> Ray 쏘기 -> 정보 저장 및 불러오기
        {
            // NPC 상호작용
            if (rayHit.collider != null && rayHit.collider.CompareTag("NPC"))
            {
                Debug.Log("NPC 스페이스바");
            }

            if (playerAbility.GetPlayerAbility() == PlayerAbility.playerAbilities.superPower)
                playerAbility.SuperPowerInteraction(rayHit);
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
