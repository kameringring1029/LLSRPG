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
        if(GM.gameTurn == GameMgr.TURN.ALLY)
            GM.pushArrow(0, -1);
    }

    public void onClickDown()
    {
        if (GM.gameTurn == GameMgr.TURN.ALLY)
            GM.pushArrow(0, 1);
    }

    public void onClickRight()
    {
        if (GM.gameTurn == GameMgr.TURN.ALLY)
            GM.pushArrow(1, 0);
    }

    public void onClickLeft()
    {
        if (GM.gameTurn == GameMgr.TURN.ALLY)
            GM.pushArrow(-1, 0);
    }
    
    public void onClickA()
    {
        if (GM.gameTurn == GameMgr.TURN.ALLY)
            GM.pushA();
    }

    public void onClickB()
    {
        if (GM.gameTurn == GameMgr.TURN.ALLY)
            GM.pushB();
    }


    public void onClickStart()
    {
        Application.LoadLevel("Main"); // Reset
    }
}
