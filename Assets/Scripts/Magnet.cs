using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private GameObject player;
    private PlayerAbility playerAbility;
    public float attractionDistance = 3.0f;
    public float moveSpeed = 1.0f;

    void Start()
    {
        player = GameObject.Find("Player");
        playerAbility = GameObject.Find("Player").GetComponent<PlayerAbility>();
    }

    private void Update()
    {
        if (playerAbility.GetPlayerAbility() == PlayerAbility.playerAbilities.magnetic)
        {
            float distanceToTarget = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToTarget <= attractionDistance)
            {
                Vector2 relativePos = player.transform.position - transform.position;
                float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle - 90);
                transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MagnetObj")) // MagnetObj 태그를 가진 애들끼리 충돌하면
        {
            GameObject[] obstacleObjects = GameObject.FindGameObjectsWithTag("Obstacle");

            foreach (GameObject obstacleObject in obstacleObjects)
            {
                Destroy(obstacleObject); // Obstacle 태그를 가진 오브젝트 삭제
            }
        }
    }
}
