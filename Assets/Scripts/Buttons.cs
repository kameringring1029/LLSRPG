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
        GM.pushArrow(0, -1);
    }

    public void onClickDown()
    {
        GM.pushArrow(0, 1);
    }

    public void onClickRight()
    {
        GM.pushArrow(1, 0);
    }

    public void onClickLeft()
    {
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


    public void onClickStart()
    {
        Application.LoadLevel("Main"); // Reset
    }
}
