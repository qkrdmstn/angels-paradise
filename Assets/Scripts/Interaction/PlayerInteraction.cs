using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : Interaction
{
    private UIManager uiManager;
    private Player player;
    private PlayerAbility playerAbility;
    private Inventory inventory;
    private FadeManager theFade;


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
        theFade = FindObjectOfType<FadeManager>();

    }

    private void Update()
    {
        if (playerAbility.currentAbility == PlayerAbility.playerAbilities.superPower && uiManager.currentUI == UIType.none) //tempFlag 변수 나중에 스크립트 번호로 바꾸기
        {
            if (GameManager.Instance.progress >= 5 && GameManager.Instance.progress < 6)
                Tutorial_Use_SuperPower();      
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBound") && collision.name == "StorageFRoad2")
        {
            if (GameManager.Instance.progress < 3)
                StorageFRoad2();
        }
        if (collision.CompareTag("Portal") && collision.name == "BaseMentEntrance")
        {
            StartCoroutine(BaseMentEntrance(collision));
        }
        if (collision.CompareTag("PlayerBound") && collision.name == "BaseMent")
        {
            if (GameManager.Instance.progress < 4)
                StartCoroutine(BaseMent());
        }
        if (collision.CompareTag("PlayerBound") && collision.name == "Vacant")
        {
            if (GameManager.Instance.progress < 6)
                Tutorial_Use_E();
        }
        if (collision.CompareTag("Portal") && collision.name == "VacantEntrance")
        {
            StartCoroutine(VacantEntrance(collision));
        }
        if (collision.CompareTag("Portal") && collision.name == "VacantExit")
        {
            StartCoroutine(VacantExit(collision));
        }
        if (collision.CompareTag("PlayerBound") && collision.name == "BaseMentFRoad" && inventory.SearchInventory("인공 심장") != 0 && GameManager.Instance.progress < 7)
        {
            Molly_Tutorial_End();
        }
        if (collision.CompareTag("Portal") && collision.name == "BaseMentExit")
        {
            StartCoroutine(BaseMentExit(collision));
        }
    }

    public void GameStartEvent()
    {
        player.SetInteractionUI(Events[0]);

        GameManager.Instance.progress = 1;
    }

    public void StorageFRoad2()
    {
        player.SetInteractionUI(Events[1]);
        GameManager.Instance.progress = 3;
    }

    IEnumerator BaseMentEntrance(Collider2D collision)
    {
        if(GameManager.Instance.progress < 4)
        {
            player.SetInteractionUI(Events[2]);
            yield return new WaitUntil(() => uiManager.currentUI == UIType.none);
        }

        theFade.FadeOut();
        yield return new WaitForSeconds(1f);
        player.transform.position = collision.transform.GetChild(0).position; //portal 자식에 있는 Target의 Pos로 이동
        theFade.FadeIn();
    }

    IEnumerator BaseMent()
    {
        //기계 앞까지 걷기
        Vector3 targetPos = new Vector3(54.42257f, 108.1249f, transform.position.z);
        Player.Instance.StartCoroutine(Player.Instance.AutoMoveCoroutine(targetPos));
        yield return new WaitUntil(() => (this.transform.position - targetPos).magnitude < 0.5f);
        GameManager.Instance.progress = 4;
        player.SetInteractionUI(Events[3]);
        yield return new WaitUntil(() => uiManager.currentUI == UIType.none);
        
    }
    IEnumerator VacantEntrance(Collider2D collision)
    {
        if (GameManager.Instance.progress >= 5)
        {
            theFade.FadeOut();
            yield return new WaitForSeconds(1f);
            player.transform.position = collision.transform.GetChild(0).position; //portal 자식에 있는 Target의 Pos로 이동
            theFade.FadeIn();
        }
    }

    IEnumerator VacantExit(Collider2D collision)
    {
        
        if (inventory.SearchInventory("인공 심장") != 0 || GameManager.Instance.progress >= 7)
        {
            theFade.FadeOut();
            yield return new WaitForSeconds(1f);
            player.transform.position = collision.transform.GetChild(0).position; //portal 자식에 있는 Target의 Pos로 이동
            theFade.FadeIn();
        }
        else
        {
            player.SetInteractionUI(Events[5]);
        }
    }

    IEnumerator BaseMentExit(Collider2D collision)
    {
        if (GameManager.Instance.progress >= 8 && GameManager.Instance.progress < 10)
        {
            player.SetInteractionUI(Events[8]); //잊은 것
        }
        else
        {
            theFade.FadeOut();
            yield return new WaitForSeconds(1f);
            player.transform.position = collision.transform.GetChild(0).position; //portal 자식에 있는 Target의 Pos로 이동
            theFade.FadeIn();
        }
    }

    public void Tutorial_Use_E()
    {
        
        player.SetInteractionUI(Events[4]);
    }

    public void Tutorial_Use_SuperPower()
    {
        if (GameManager.Instance.progress < 6)
            GameManager.Instance.progress = 6;
        player.SetInteractionUI(Events[6]);
       
    }

    public void Molly_Tutorial_End()
    {
        GameManager.Instance.progress = 7;
        player.SetInteractionUI(Events[7]);
    }
}
