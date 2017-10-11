using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {
    
    GameMgr GM;

    // Use this for initialization
    void Start () {
        GM = GameObject.Find("Main Camera").GetComponent<GameMgr>();
    }
	
	public void onClickUp()
    {
        //Pointer.GetComponent<cursor>().moveCursor(0, -1);
        GM.pushArrow(0, -1);

    }

    public void onClickDown()
    {
        //Pointer.GetComponent<cursor>().moveCursor(0, 1);
        GM.pushArrow(0, 1);
    }

    public void onClickRight()
    {
        //Pointer.GetComponent<cursor>().moveCursor(1, 0);
        GM.pushArrow(1, 0);
    }

    public void onClickLeft()
    {
        //Pointer.GetComponent<cursor>().moveCursor(-1, 0);
        GM.pushArrow(-1, 0);
    }
    
    public void onClickA()
    {
        GM.pushA();
    }

    public void onClickB()
    {
        GM.pushB();
    }
}
