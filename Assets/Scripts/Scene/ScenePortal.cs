using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TransferCoroutine());
        }
    }

    IEnumerator TransferCoroutine()
    {
        Player.Instance.speed = 0;
        FadeManager.Instance.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
        FadeManager.Instance.FadeIn();
        Player.Instance.speed = 0.1f;
    }
}
