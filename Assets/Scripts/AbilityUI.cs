using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    private PlayerAbility playerAbility;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        playerAbility = FindObjectOfType<PlayerAbility>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            SetPlayerAbilitySuperPower();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            SetPlayerAbilityElectricity();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            SetPlayerAbilityMagnetic();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            SetPlayerAbilityHacking();
        }

    }

    public void SetPlayerAbilitySuperPower()
    {
        playerAbility.SetPlayerAbility(PlayerAbility.playerAbilities.superPower);
        StartCoroutine(InactiveUIcoroutine());
    }
    public void SetPlayerAbilityElectricity()
    {
        playerAbility.SetPlayerAbility(PlayerAbility.playerAbilities.electricity);
        StartCoroutine(InactiveUIcoroutine());
    }
    public void SetPlayerAbilityMagnetic()
    {
        playerAbility.SetPlayerAbility(PlayerAbility.playerAbilities.magnetic);
        StartCoroutine(InactiveUIcoroutine());
    }
    public void SetPlayerAbilityHacking()
    {
        playerAbility.SetPlayerAbility(PlayerAbility.playerAbilities.hacking);
        StartCoroutine(InactiveUIcoroutine());
    }


    IEnumerator InactiveUIcoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        uiManager.isActiveUI = false;
        gameObject.SetActive(false);
    }
}
