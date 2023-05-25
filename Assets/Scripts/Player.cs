using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed, verticalInput, horizonInput;
    private Vector3 vector;
    private bool keyDown = false;
    int walkCount = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator MoveCoroutine()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizonInput = Input.GetAxisRaw("Horizontal");
        vector.Set(horizonInput * speed, verticalInput * speed, transform.position.z);

        for(int i=0; i<walkCount; i++)
        {
            transform.Translate(vector);
            yield return new WaitForSeconds(0.01f);
        }
            
        keyDown = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(!keyDown)
        {
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                keyDown = true;
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
