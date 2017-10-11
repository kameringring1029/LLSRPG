using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : MonoBehaviour {

    GameObject Camera;

    private void Start()
    {
        Camera = GameObject.Find("Main Camera");
        Camera.GetComponent<Camera>().orthographicSize = 3.5f;
    }

    public void pushZoom()
    {
        float nowCameraSize = Camera.GetComponent<Camera>().orthographicSize;

        if (nowCameraSize == 3.5f)
        {
            Camera.GetComponent<Camera>().orthographicSize = 7;
        }
        else
        {
            Camera.GetComponent<Camera>().orthographicSize = nowCameraSize-3.5f;
        }

    }
}
