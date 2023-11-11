using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCollision2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (player != null && playerMovement != null)
            {
                // Player ������Ʈ�� Ȱ��ȭ�մϴ�.
                player.enabled = true;
                playerMovement.isMoving = false;
                // PlayerMovement ������Ʈ�� ��Ȱ��ȭ.
                playerMovement.isMoving = false;
                playerMovement.moveSpeed = 0;
                playerMovement.rb.velocity = Vector2.zero;
                //playerMovement.enabled = false;
            }
        }
    }
}
