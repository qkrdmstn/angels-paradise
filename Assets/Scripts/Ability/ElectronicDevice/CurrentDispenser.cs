using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentDispenser : ElctronicObject
{
    UIManager uiManager;
    protected override void ElectricalInteraction()
    {
        Debug.Log("Elec");
    }

    protected override void NormalInteraction()
    {
    }

    public void RotateClockwise() //userInteractive로 반환받은 콜라이더를 회전시키기 // Rotate the collider returned by userInteractive
    {
        transform.Rotate(new Vector3(0, 0, -45.0f));
    }

    public void RotateAntiClockwise()
    {
        transform.Rotate(new Vector3(0, 0, 45.0f));
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAbility = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbility>();
        uiManager = GameObject.FindObjectOfType<UIManager>(); //performance issue
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)) //user interaction
        {
            if (playerAbility.GetPlayerAbility() == PlayerAbility.playerAbilities.electricity) 
                ElectricalInteraction();
            else
                NormalInteraction();
        }
    }
}
