using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    private Player player;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
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
        player.SetPlayerAbility(Player.PlayerAbility.superPower);
        StartCoroutine(InactiveUIcoroutine());
    }
    public void SetPlayerAbilityElectricity()
    {
        player.SetPlayerAbility(Player.PlayerAbility.electricity);
        StartCoroutine(InactiveUIcoroutine());
    }
    public void SetPlayerAbilityMagnetic()
    {
        player.SetPlayerAbility(Player.PlayerAbility.magnetic);
        StartCoroutine(InactiveUIcoroutine());
    }
    public void SetPlayerAbilityHacking()
    {
        player.SetPlayerAbility(Player.PlayerAbility.hacking);
        StartCoroutine(InactiveUIcoroutine());
    }


    IEnumerator InactiveUIcoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        uiManager.isActiveUI = false;
        gameObject.SetActive(false);
    }
}
