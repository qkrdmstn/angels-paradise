using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (player != null && playerMovement != null)
            {
                // Player ������Ʈ�� ��Ȱ��ȭ�մϴ�.
                player.enabled = false;
                playerMovement.isMoving = false;
                // PlayerMovement ������Ʈ�� Ȱ��ȭ�ϰ� �̵��� ����ϴ�.
                playerMovement.moveSpeed = 5;
                playerMovement.enabled = true;
            }
        }
    }
}
