using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerAbility : MonoBehaviour
{
    private Player player;

    //Change Character
    private SpriteLibrary spriteLibrary;
    public SpriteLibraryAsset[] abilitySkin;

    //Player Ability
    private GameObject hackingObj;
    private GameObject superPowerObj;
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
            hackingObj = GameObject.FindGameObjectWithTag("HackingObj");
        }
        else if (currentAbility == playerAbilities.superPower)
        {
            spriteLibrary.spriteLibraryAsset = abilitySkin[1];
            superPowerObj = GameObject.FindGameObjectWithTag("SuperPowerObj");
            SuperPowerAbility();
        }
        else if (currentAbility == playerAbilities.electricity)
        {
            spriteLibrary.spriteLibraryAsset = abilitySkin[2];
        }
        else if (currentAbility == playerAbilities.magnetic)
        {
            spriteLibrary.spriteLibraryAsset = abilitySkin[3];
        }
        else if (currentAbility == playerAbilities.hacking)
        {
            spriteLibrary.spriteLibraryAsset = abilitySkin[4];
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

    void SuperPowerAbility()
    {
        /* 스페이스바 눌러서 SuperPowerObj를 부수기 */
        // if (Input.GetKeyDown(KeyCode.Space)) // Space -> Ray 쏘기 -> 정보 저장 및 불러오기
        // {
        //     RaycastHit2D rayHit = Physics2D.Raycast(player.rigid.position, player.vector, 3f, LayerMask.GetMask("Object"));
        //     Debug.DrawRay(player.rigid.position, player.vector * 3f, Color.green);

        //     if (rayHit.collider != null && rayHit.collider.CompareTag("SuperPowerObj"))
        //     {
        //         Destroy(gameObject);
        //     }
        // }
    }
}
