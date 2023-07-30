using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform targetPos;
    private GameObject player;
    private FadeManager theFade;
    private Player playerScript;
    public BoxCollider2D Bound;
    public bool flag;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        targetPos = transform.GetChild(0);
        theFade = FindObjectOfType<FadeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && flag)
        {
            StartCoroutine(TransferCoroutine());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && flag)
        {
            StartCoroutine(TransferCoroutine());
        }
    }

    IEnumerator TransferCoroutine()
    {
        playerScript.speed = 0;
        theFade.FadeOut();
        yield return new WaitForSeconds(1f);
        player.transform.position = targetPos.position;
        theFade.FadeIn();
        playerScript.speed = 0.1f;
    }
}
