using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : Interaction
{
    private UIManager uiManager;
    private Player player;
    private PlayerAbility playerAbility;
    Inventory inventory;
    bool tempFlag = false;
    public override InteractionEvent GetEvent()
    {
        return Events[0];

    }

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        player = this.GetComponent<Player>();
        playerAbility = this.GetComponent<PlayerAbility>();
        inventory = GameObject.FindObjectOfType<Inventory>();
        tempFlag = false;

        GameStartEvent();
    }

    private void Update()
    {
        if (playerAbility.currentAbility == PlayerAbility.playerAbilities.superPower && !tempFlag) //tempFlag 변수 나중에 스크립트 번호로 바꾸기
        {
            Tutorial_Use_SuperPower();
            tempFlag = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBound") && collision.name == "StorageFRoad2")
        {
            StorageFRoad2();
        }
        if (collision.CompareTag("Portal") && collision.name == "BasementEntrance")
        {
            StartCoroutine(BaseMentEntrance(collision));
        }
        if (collision.CompareTag("PlayerBound") && collision.name == "BaseMent")
        {
            StartCoroutine(BaseMent());
        }
        if (collision.CompareTag("PlayerBound") && collision.name == "Vacant")
        {
            Tutorial_Use_E();
        }
        if (collision.CompareTag("PlayerBound") && collision.name == "BaseMentFRoad" && inventory.SearchInventory("인공 심장") != 0)
        {
            Molly_Tutorial_End();
        }
    }

    public void GameStartEvent()
    {
        player.SetInteractionUI(Events[0]);
    }

    public void StorageFRoad2()
    {
        player.SetInteractionUI(Events[1]);
    }

    IEnumerator BaseMentEntrance(Collider2D collision)
    {
        player.SetInteractionUI(Events[2]);
        yield return new WaitUntil(() => uiManager.currentUI == UIType.none);
        collision.GetComponent<Portal>().flag = true;
    }

    IEnumerator BaseMent()
    {
        player.SetInteractionUI(Events[3]);
        yield return new WaitUntil(() => uiManager.currentUI == UIType.none);
        Debug.Log("거래 긋");
        
    }

    public void Tutorial_Use_E()
    {
        player.SetInteractionUI(Events[4]);
    }

    public void Tutorial_Use_SuperPower()
    {
        player.SetInteractionUI(Events[6]);
    }

    public void Molly_Tutorial_End()
    {
        player.SetInteractionUI(Events[7]);
    }
}
