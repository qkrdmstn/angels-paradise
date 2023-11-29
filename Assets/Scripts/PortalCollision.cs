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
                // Player 컴포넌트를 비활성화합니다.
                player.enabled = false;
                playerMovement.isMoving = false;
                // PlayerMovement 컴포넌트를 활성화하고 이동을 멈춥니다.
                playerMovement.moveSpeed = 5;
                playerMovement.enabled = true;
            }
        }
    }
}
