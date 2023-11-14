using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootHoldPuzzle : MonoBehaviour
{
    [Header("���� ���� ����")]
    private bool isRedActive = true;  // �ʱ⿡�� ������ Ȱ��ȭ
    private int interactionCount = 0;  // FootHold�� ��ȣ�ۿ��� Ƚ���� ����
    private GameObject[] redObstacles;  // ������ ������Ʈ ����Ʈ
    private GameObject[] yellowObstacles;  // ����� ������Ʈ ����Ʈ

    // Start is called before the first frame update
    void Start()
    {
        redObstacles = GameObject.FindGameObjectsWithTag("RedObstacle");
        yellowObstacles = GameObject.FindGameObjectsWithTag("YellowObstacle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Player�� Foothold�� �浹�� ��� ���
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �浹�� ������Ʈ�� �±׸� Ȯ��
        if (other.CompareTag("Player"))
        {
            // player �±׸� ���� ������Ʈ�� foothold �±׸� ���� ������Ʈ�� �浹���� �� ������ �ڵ� �ۼ�
            ToggleObstacles();
            // ���� ���, �÷��̾ ���ǿ� �ö��� ���� ������ �߰��� �� �ֽ��ϴ�.
            Debug.Log("���� ����!");
        }
    }

    void ToggleObstacles()
    {
        // ���� ���¿� ���� ������ �Ǵ� ������� Ȱ��ȭ�մϴ�.
        foreach (GameObject redObstacle in redObstacles)
        {
            redObstacle.SetActive(!isRedActive);
        }

        foreach (GameObject yellowObstacle in yellowObstacles)
        {
            yellowObstacle.SetActive(isRedActive);
        }

        // ������ ����� ���� ������Ʈ�մϴ�.
        interactionCount++;

        // interactionCount�� 2�� ����� ������ isRedActive�� ������ŵ�ϴ�.
        isRedActive = interactionCount % 2 == 0;
    }
}
