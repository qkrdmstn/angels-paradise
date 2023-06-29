using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerAbility : MonoBehaviour
{
    //Change Character
    private SpriteLibrary spriteLibrary;
    public SpriteLibraryAsset[] abilitySkin;
    public Sprite[] standSkin;

    //Player Ability
    public GameObject hackingObjParent;
    public GameObject hackingObj;

    public enum playerAbilities
    {
        normal,
        superPower,
        electricity,
        magnetic,
        hacking
    }

    private playerAbilities currentAbility;
    public void SetPlayerAbility(playerAbilities a)
    {
        currentAbility = a;
        Debug.Log(currentAbility);
        if (currentAbility == playerAbilities.normal)
        {
            spriteLibrary.spriteLibraryAsset = abilitySkin[0];
            gameObject.GetComponent<SpriteRenderer>().sprite = standSkin[0];
        }
        else if (currentAbility == playerAbilities.superPower)
        {
            spriteLibrary.spriteLibraryAsset = abilitySkin[1];
            gameObject.GetComponent<SpriteRenderer>().sprite = standSkin[1];
        }
        else if (currentAbility == playerAbilities.electricity)
        {
            spriteLibrary.spriteLibraryAsset = abilitySkin[2];
            gameObject.GetComponent<SpriteRenderer>().sprite = standSkin[2];
        }
        else if (currentAbility == playerAbilities.magnetic)
        {
            spriteLibrary.spriteLibraryAsset = abilitySkin[3];
            gameObject.GetComponent<SpriteRenderer>().sprite = standSkin[3];
        }
        else if (currentAbility == playerAbilities.hacking)
        {
            spriteLibrary.spriteLibraryAsset = abilitySkin[4];
            gameObject.GetComponent<SpriteRenderer>().sprite = standSkin[4];

            hackingObjParent = GameObject.FindGameObjectWithTag("HackingObj");
            if (hackingObjParent != null)
            {
                hackingObj = hackingObjParent.transform.GetChild(0).gameObject;
                hackingObj.SetActive(true);
            }
        }

        if(currentAbility != playerAbilities.hacking)
        {
            if (hackingObjParent != null)
            {
                hackingObj.SetActive(false);
            }
        }

    }
    public playerAbilities GetPlayerAbility()
    {
        return currentAbility;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteLibrary = GetComponent<UnityEngine.U2D.Animation.SpriteLibrary>();
        SetPlayerAbility(playerAbilities.normal);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && currentAbility != playerAbilities.normal)
            SetPlayerAbility(playerAbilities.normal);

        if (Input.GetKey(KeyCode.Alpha1))
            SetPlayerAbility(playerAbilities.superPower);
        else if (Input.GetKey(KeyCode.Alpha2))
            SetPlayerAbility(playerAbilities.electricity);
        else if (Input.GetKey(KeyCode.Alpha3))
            SetPlayerAbility(playerAbilities.magnetic);
        else if (Input.GetKey(KeyCode.Alpha4))
            SetPlayerAbility(playerAbilities.hacking);


    }
}
