using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageUI : MonoBehaviour
{
    private string currentEvent; //현재 이벤트 상태
    public Sprite [] sprites;
    public Image image;
    public PlayerAbility ability;

    public void SetCurrentEvent(InteractionEvent _event)
    {
        currentEvent = _event.eventName;
        image.sprite = GetImage(currentEvent);
    }

    public Sprite GetImage(string eventName)
    {

        if (eventName == "거울")
        {

            if (ability.GetPlayerAbility() == PlayerAbility.playerAbilities.superPower)
            {
                return sprites[1];
            }
            else if (ability.GetPlayerAbility() == PlayerAbility.playerAbilities.magnetic)
            {
                return sprites[2];
            }
            else if (ability.GetPlayerAbility() == PlayerAbility.playerAbilities.electricity)
            {
                return sprites[3];
            }
            else if (ability.GetPlayerAbility() == PlayerAbility.playerAbilities.hacking)
            {
                return sprites[4];
            }
            else
            {
                return sprites[0];
            }

        }
        else
            return sprites[0];
    }
}
