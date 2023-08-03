using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    Camera mirrorcam;
    Texture2D t2d;
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        mirrorcam = GetComponent<Camera>();
        // 렌더 텍스쳐의 크기만큼 선언
        t2d = new Texture2D(mirrorcam.targetTexture.width, mirrorcam.targetTexture.height, textureFormat: TextureFormat.ARGB32, false);
        StartCoroutine(camrender());
    }

    WaitForEndOfFrame WaitForEnd = new WaitForEndOfFrame();
    // WaitForEndOfFrame : 모든 카메라가 렌더링을 완료할 때까지 기다림
    IEnumerator camrender()
    {
        while (true)
        {
            yield return WaitForEnd;
            RenderTexture.active = mirrorcam.targetTexture; // 카메라에 추가될 렌더 텍스쳐 활성화
            mirrorcam.Render(); // 카메라 읽어줌
            // ReadPixels 함수를 통해 현재의 렌더텍스쳐의 내용을 가져올 수 있음
            t2d.ReadPixels(new Rect(0, 0, mirrorcam.targetTexture.width, mirrorcam.targetTexture.height), 0, 0);
            t2d.Apply(); // 적용

            // 스프라이트에 t2d의 내용을 적용
            sr.sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f), t2d.width);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
