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
                // Player 컴포넌트를 활성화합니다.
                player.enabled = true;
                playerMovement.isMoving = false;
                // PlayerMovement 컴포넌트를 비활성화.
                playerMovement.isMoving = false;
                playerMovement.moveSpeed = 0;
                playerMovement.rb.velocity = Vector2.zero;
                //playerMovement.enabled = false;
            }
        }
    }
}
