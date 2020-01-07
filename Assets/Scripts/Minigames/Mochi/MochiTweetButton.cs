using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MochiTweetButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick() {
        Debug.Log("tweeeeeet" + GameObject.FindGameObjectsWithTag("mochimikan").Length);
        Application.ExternalCall("tweet", GameObject.FindGameObjectsWithTag("mochimikan").Length-1);
    }
}
