using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    TalkData[] a;
    // Start is called before the first frame update
    void Start()
    {
        a = DialogueData.GetDialogue("¸ô¸® Ã³À½");
        for(int i=0; i<a.Length; i++)
        {
            Debug.Log(a[i].name);
            foreach (string context in a[i].constexts)
                Debug.Log(context);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
