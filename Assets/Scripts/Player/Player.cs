using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static public Player Instance = null;
    //public GameManager gameManager;
    private UIManager uiManager;

    //Player Movment
    public float verticalInput, horizonInput;
    public float speed, runSpeed;
    public float autoSpeed;
    private Vector3 vector;
    public bool keyDown = false;
    int walkCount = 10;
    private Animator animator;
    private Rigidbody2D rigid;
    public float magnification;
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
    public float interactionDistance = 1.5f;
    public IEnumerator AxisMoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift)) //running
            {
                animator.SetBool("Running", true);
                runSpeed = speed * magnification;
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
                Vector3 nxtPos = new Vector3(clampedX, clampedY, transform.position.z);
                Vector3 temp = nxtPos - transform.transform.position;
                transform.transform.position = new Vector3(nxtPos.x, nxtPos.y, transform.position.z);

                yield return new WaitForSeconds(0.01f);
            }
        }

        animator.SetBool("Walking", false);
        animator.SetBool("Running", false);
        keyDown = false;
    }

    public IEnumerator AutoMoveCoroutine(Vector3 targetPos)
    {
        animator.SetBool("Walking", true);
        animator.SetBool("Running", false);
        keyDown = true;
        Vector3 temp = targetPos - this.transform.position;
        Vector3 n = temp.normalized;
        
        vector.Set(n.x * autoSpeed, n.y * autoSpeed, transform.position.z);
        Debug.Log(targetPos + " " + n + "pla" + this.transform.position);
        Debug.Log(vector);
        animator.SetFloat("DirX", vector.x);
        animator.SetFloat("DirY", vector.y);

        while ((this.transform.position - targetPos).magnitude > 0.5f)
        {
            Vector3 nxtPos = new Vector3(this.transform.position.x + vector.x, this.transform.position.y + vector.y, transform.position.z);
            transform.transform.position = new Vector3(nxtPos.x, nxtPos.y, transform.position.z);

            yield return new WaitForSeconds(0.01f);
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
                StartCoroutine(AxisMoveCoroutine());
            }
        }

        RaycastHit2D []rayHit = Physics2D.RaycastAll(rigid.position, vector, interactionDistance, LayerMask.GetMask("Object")); //레이에 닿는 모든 콜라이더 정보 저장
        Debug.DrawRay(rigid.position, vector.normalized * interactionDistance, Color.green);

        // 아이템 줍기
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (rayHit.Length != 0)
            {
                for (int i = 0; i < rayHit.Length; i++) //아래의 spacebar 처리와 동일
                {
                    if(rayHit[i].collider.CompareTag("FieldItem"))
                    {
                        FieldItems fieldItems = rayHit[i].collider.GetComponent<FieldItems>();
                        Interaction interaction = rayHit[i].collider.GetComponent<Interaction>();
                        if (inventory.AddItem(fieldItems.GetItem()))
                        {
                            fieldItems.DestroyItem();
                        }
                        if(interaction != null)
                        {
                            SetInteractionUI(rayHit[i]);
                        }
                        break;
                    }
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.Space) && uiManager.currentUI == UIType.none) // Space -> Ray 쏘기 -> 정보 저장 및 불러오기
        {
            Debug.Log("Key Space");
            for (int k = 0; k < rayHit.Length; k++)
                Debug.Log(rayHit[k]);
            if (rayHit.Length != 0)
            {
                for (int i = 0; i < rayHit.Length; i++) //닿은 콜라이더 반복문으로 조사
                {
                    if (rayHit[i].collider.CompareTag("NPC") || rayHit[i].collider.CompareTag("EventObj")) //NPC나 EventObj가 있다면,
                    {
                        SetInteractionUI(rayHit[i]);
                        Debug.Log("이벤트 상호작용");
                        break; //처리 후 반복문 종료하기
                    } //NPC, 이벤트 오브젝트 상호작용

                    if (rayHit[i].collider.CompareTag("SuperPowerObj") && playerAbility.GetPlayerAbility() == PlayerAbility.playerAbilities.superPower)
                    {
                        playerAbility.SuperPowerInteraction(rayHit[i]);
                        Interaction interaction = rayHit[i].collider.GetComponent<Interaction>(); //괴력 오브젝트에 상호작용이 있다면,,
                        if (interaction != null)
                        {
                            SetInteractionUI(rayHit[i]);
                        }
                        break;
                    } //괴력 상호작용
                    
                }
            }
            
        }

    }

    public void SetInteractionUI(RaycastHit2D hit)
    {
        InteractionEvent Event = hit.collider.GetComponent<Interaction>().GetEvent(); //상호작용 오브젝트의 이벤트 get
        if (Event == null)
            return;
        if (Event.eventType == InteractionType.Dialogue) //상호작용이 대화형이라면, 
        {
            uiManager.setActiveUI(UIType.talk); //UI 활성화
            uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent(Event); //UI로 event 전달     
        }
        else if (Event.eventType == InteractionType.Image)
        {
            uiManager.imageUI.GetComponent<ImageUI>().SetCurrentEvent(Event); //UI로 event 전달
            uiManager.setActiveUI(UIType.image); //UI 활성화
        }
        else if (Event.eventType == InteractionType.ImageDialogue)
        {
            uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent(Event); //UI로 event 전달
            uiManager.setActiveUI(UIType.talk); //UI 활성화
            uiManager.imageUI.GetComponent<ImageUI>().SetCurrentEvent(Event); //UI로 event 전달
            uiManager.setActiveUI(UIType.image); //UI 활성화
        }
        else if (Event.eventType == InteractionType.TextInput)
        {
            uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent(Event); //UI로 event 전달
            uiManager.setActiveUI(UIType.talk); //UI 활성화
            uiManager.textInputUI.GetComponent<TextInputUI>().SetCurrentEvent(Event); //UI로 event 전달
            uiManager.setActiveUI(UIType.textInput); //UI 활성화
        }
    }

    public void SetInteractionUI(InteractionEvent Event)
    {
        if (Event.eventType == InteractionType.Dialogue) //상호작용이 대화형이라면, 
        {
            uiManager.setActiveUI(UIType.talk); //UI 활성화
            uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent(Event); //UI로 event 전달
        }
        else if (Event.eventType == InteractionType.Image)
        {
            uiManager.imageUI.GetComponent<ImageUI>().SetCurrentEvent(Event); //UI로 event 전달
            uiManager.setActiveUI(UIType.image); //UI 활성화
        }
        else if (Event.eventType == InteractionType.ImageDialogue)
        {
            uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent(Event); //UI로 event 전달
            uiManager.setActiveUI(UIType.talk); //UI 활성화
            uiManager.imageUI.GetComponent<ImageUI>().SetCurrentEvent(Event); //UI로 event 전달
            uiManager.setActiveUI(UIType.image); //UI 활성화
        }
        else if (Event.eventType == InteractionType.TextInput)
        {
            uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent(Event); //UI로 event 전달
            uiManager.setActiveUI(UIType.talk); //UI 활성화
            uiManager.textInputUI.GetComponent<TextInputUI>().SetCurrentEvent(Event); //UI로 event 전달
            uiManager.setActiveUI(UIType.textInput); //UI 활성화
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CameraBound"))
        {
            theCamera.GetComponent<CameraManager>().SetCameraBound(collision.GetComponent<BoxCollider2D>());
        }
        
        if(collision.CompareTag("PlayerBound"))
        {
            SetPlayerBound(collision.GetComponent<BoxCollider2D>());
        }
   
    }

    public void SetPlayerBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
