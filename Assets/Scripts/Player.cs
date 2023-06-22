using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
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

    //Change Character
    private SpriteLibrary spriteLibrary;
    public SpriteLibraryAsset [] abilitySkin;

    //Player Ability
    public enum PlayerAbility
    {
        normal,
        superPower,
        electricity,
        magnetic,
        hacking
    }
    private PlayerAbility currentAbility;
    public void SetPlayerAbility(PlayerAbility a)
    {
        currentAbility = a;
        Debug.Log(currentAbility);
        if(currentAbility == PlayerAbility.normal)
            spriteLibrary.spriteLibraryAsset = abilitySkin[0];
        else if (currentAbility == PlayerAbility.superPower)
            spriteLibrary.spriteLibraryAsset = abilitySkin[1];
        else if (currentAbility == PlayerAbility.electricity)
            spriteLibrary.spriteLibraryAsset = abilitySkin[2];
        else if (currentAbility == PlayerAbility.magnetic)
            spriteLibrary.spriteLibraryAsset = abilitySkin[3];
        else if (currentAbility == PlayerAbility.hacking)
            spriteLibrary.spriteLibraryAsset = abilitySkin[4];
    }
    public PlayerAbility GetPlayerAbility()
    {
        return currentAbility;
    }

    //Player Emotion
    public enum PlayerEmotion
    {
        fine,
        glad,
        sad,
        joy,
        angry
    }
    private PlayerEmotion currentEmotion;

    public void SetPlayerEmotion(PlayerEmotion e)
    {
        currentEmotion = e;
        Debug.Log(currentEmotion);
    }
    public PlayerEmotion GetPlayerEmotion()
    {
        return currentEmotion;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        uiManager = FindObjectOfType<UIManager>();
        spriteLibrary = GetComponent<SpriteLibrary>();
        SetPlayerAbility(PlayerAbility.normal);
    }

    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift)) //달리기
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
                transform.Translate(vector);
                yield return new WaitForSeconds(0.01f);
            }
        }

        animator.SetBool("Walking", false);
        animator.SetBool("Running", false);
        keyDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!keyDown && !uiManager.isActiveUI)
        {

            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                animator.SetBool("Walking", true);
                keyDown = true;
                StartCoroutine(MoveCoroutine());
            }
        }

        if (Input.GetKey(KeyCode.Escape) && currentAbility != PlayerAbility.normal)
            SetPlayerAbility(PlayerAbility.normal);

        if (Input.GetKey(KeyCode.Alpha1))
            SetPlayerAbility(PlayerAbility.superPower);
        else if (Input.GetKey(KeyCode.Alpha2))
            SetPlayerAbility(PlayerAbility.electricity);
        else if (Input.GetKey(KeyCode.Alpha3))
            SetPlayerAbility(PlayerAbility.magnetic);
        else if (Input.GetKey(KeyCode.Alpha4))
            SetPlayerAbility(PlayerAbility.hacking);

        if (Input.GetKeyDown(KeyCode.Space))
            manager.Action();

        // Direction
        if (vector.y > 0)
            vector = Vector3.up * (speed + runSpeed);
        else if (vector.y < 0)
            vector = Vector3.down * (speed + runSpeed);
        else if (vector.x < 0)
            vector = Vector3.left * (speed + runSpeed);
        else if (vector.x > 0)
            vector = Vector3.right * (speed + runSpeed);


        //Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            Debug.Log(scanObject.name);
        }
    }

    void FixedUpdate()
{
    // Ray (아이템, NPC 조사)
    RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, vector.normalized, 2f, LayerMask.GetMask("Object"));
    Debug.DrawRay(rigid.position, vector.normalized * 2f, Color.green);

    if (rayHit.collider != null)
    {
        scanObject = rayHit.collider.gameObject;
        // Raycast된 오브젝트를 변수로 저장하여 활용
    }
    else
    {
        scanObject = null;
    }
}

}
