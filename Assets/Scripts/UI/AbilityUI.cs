using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    private PlayerAbility playerAbility;
    private UIManager uiManager;
    public GameObject superPowerUI;
    public GameObject magneticUI;
    public GameObject electricityUI;
    public GameObject hackingUI;
    // Start is called before the first frame update
    void Start()
    {
        playerAbility = FindObjectOfType<PlayerAbility>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.W))
        //{
        //    SetPlayerAbilitySuperPower();
        //}
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    SetPlayerAbilityElectricity();
        //}
        //else if (Input.GetKey(KeyCode.S))
        //{
        //    SetPlayerAbilityMagnetic();
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    SetPlayerAbilityHacking();
        //}

    }
    public void SetPlayerAbilitySuperPower()
    {
        playerAbility.SetPlayerAbility(PlayerAbility.playerAbilities.superPower);
        StartCoroutine(InactiveUIcoroutine());
        //능력 UI 디폴트 포커스 설정하기
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

    IEnumerator InactiveUIcoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        uiManager.setInActiveUI();

    }
}
