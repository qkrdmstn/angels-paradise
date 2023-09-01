using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform targetPos;
    private GameObject player;
    public BoxCollider2D Bound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        targetPos = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(TransferCoroutine());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TransferCoroutine());
        }
    }

    IEnumerator TransferCoroutine()
    {
        Player.Instance.speed = 0;
        FadeManager.Instance.FadeOut();
        yield return new WaitForSeconds(1f);
        player.transform.position = targetPos.position;
        FadeManager.Instance.FadeIn();
        Player.Instance.speed = 0.1f;
    }
}
