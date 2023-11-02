using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public Vector3 pos1, pos2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("asd");
        if(collision.tag == "Player")
        {
            collision.GetComponent<Transform>().position = pos1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("afa");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
