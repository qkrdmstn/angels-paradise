using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : Interaction
{
    private UIManager uiManager;
    private Player player;
    public override InteractionEvent GetEvent()
    {
        return Events[0];

    }

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        player = this.GetComponent<Player>();

        GameStartEvent();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBound") && collision.name == "StorageFRoad2")
        {
            StorageFRoad2();
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

    
}
