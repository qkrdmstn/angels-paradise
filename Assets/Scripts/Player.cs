using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager manager;
    public float verticalInput, horizonInput;
    public float speed, runSpeed;
    private Vector3 vector;
    private bool keyDown = false;
    int walkCount = 10;
    private Animator animator;
    private Rigidbody2D rigid;
    GameObject scanObject;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift)) //달리기
                runSpeed = speed * 0.5f;
            else
                runSpeed = 0;

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
        keyDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!keyDown)
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
