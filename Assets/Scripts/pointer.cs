using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointer : MonoBehaviour {

    GameObject Pointer;
    GameObject Camera;

    // Use this for initialization
    void Start () {
        Pointer=GameObject.Find("cursor");
        Camera = GameObject.Find("Main Camera");
    }
	
	public void onClickUp()
    {
        Pointer.GetComponent<cursor>().moveCursor(0, -1);
    }

    public void onClickDown()
    {
        Pointer.GetComponent<cursor>().moveCursor(0, 1);
    }

    public void onClickRight()
    {
        Pointer.GetComponent<cursor>().moveCursor(1, 0);
    }

    public void onClickLeft()
    {
        Pointer.GetComponent<cursor>().moveCursor(-1, 0);
    }
    
    public void onClickA()
    {
        Camera.GetComponent<GameMgr>().pushA();
    }
}
