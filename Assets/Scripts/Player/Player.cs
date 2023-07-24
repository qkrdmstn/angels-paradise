using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static public Player Instance;
    //public GameManager gameManager;
    private UIManager uiManager;

    //Player Movment
    public float verticalInput, horizonInput;
    public float speed, runSpeed;
    private Vector3 vector;
    private bool keyDown = false;
    int walkCount = 10;
    private Animator animator;
    private Rigidbody2D rigid;

    //inventory
    Inventory inventory;

    //Player Bound
    public BoxCollider2D bound;
    private Vector3 minBound;
    private Vector3 maxBound;
    private float playerHalfHeight = 1.0f;
    private float playerHalfWidth = 0.5f;

    //Camera Setting
    Camera theCamera;
    private PlayerAbility playerAbility;

    //Interaction
    private float interactionDistance = 1.5f;
    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift)) //running
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

            //if (vector.x != 0)  //대각선 이동 방지
            //    vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            for (int i = 0; i < walkCount; i++)
            {
                //Player Bound Set
                float clampedX = Mathf.Clamp(this.transform.position.x + vector.x, minBound.x + playerHalfWidth, maxBound.x - playerHalfWidth);
                float clampedY = Mathf.Clamp(this.transform.position.y + vector.y, minBound.y + playerHalfHeight, maxBound.y - playerHalfHeight);
                transform.transform.position = new Vector3(clampedX, clampedY, transform.position.z);
                yield return new WaitForSeconds(0.01f);
            }
        }

        animator.SetBool("Walking", false);
        animator.SetBool("Running", false);
        keyDown = false;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        uiManager = FindObjectOfType<UIManager>();
        theCamera = FindObjectOfType<Camera>();
        inventory = GetComponent<Inventory>();
        playerAbility = GameObject.Find("Player").GetComponent<PlayerAbility>();
        uiManager.currentUI = UIType.none;

        string eventName = this.GetComponent<DialogueInteraction>().GetEvent(); //상호작용 오브젝트의 이벤트 get
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent(eventName); //UI로 event 전달
        uiManager.setActiveUI(UIType.talk); //UI 활성화
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(inventory.SearchInventory("item0"));
        }

        if (!keyDown && uiManager.currentUI == UIType.none) //Player 이동
        {

            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                animator.SetBool("Walking", true);
                keyDown = true;
                StartCoroutine(MoveCoroutine());
            }
        }

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, vector, interactionDistance, LayerMask.GetMask("Object"));
        Debug.DrawRay(rigid.position, vector.normalized * interactionDistance, Color.green);

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
            if (rayHit.collider != null && (rayHit.collider.CompareTag("NPC") || rayHit.collider.CompareTag("EventObj")) && uiManager.currentUI == UIType.none) //이벤트 Obj이거나 NPC일 때 && UI가 비활성화일 때
            {
                string eventName = rayHit.collider.GetComponent<DialogueInteraction>().GetEvent(); //상호작용 오브젝트의 이벤트 get
                
                uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent(eventName); //UI로 event 전달
                uiManager.setActiveUI(UIType.talk); //UI 활성화
            }

            if (playerAbility.GetPlayerAbility() == PlayerAbility.playerAbilities.superPower)
                playerAbility.SuperPowerInteraction(rayHit);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MapBound"))
        {
            SetBound(collision.GetComponent<BoxCollider2D>());
        }
   
    }

    public void SetBound(BoxCollider2D newBound)
    {
        theCamera.GetComponent<CameraManager>().SetCameraBound(newBound);
        SetPlayerBound(newBound);
    }

    public void SetPlayerBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
