﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using General;

/*
 * SRPGゲーム中のコントローラボタンにアタッチされて処理する用のやつ
 */


public class ControllerButtons : MonoBehaviour {
    
    GameMgr GM;

    // Use this for initialization
    void Start () {
        GM = GameObject.Find("Main Camera").GetComponent<GameMgr>();
    }
	
	public void onClickUp()
    {
        if(GM.gameTurn == CAMP.ALLY)
            GM.pushArrow(0, -1);
    }

    public void onClickDown()
    {
        if (GM.gameTurn == CAMP.ALLY)
            GM.pushArrow(0, 1);
    }

    public void onClickRight()
    {
        if (GM.gameTurn == CAMP.ALLY)
            GM.pushArrow(1, 0);
    }

    public void onClickLeft()
    {
        if (GM.gameTurn == CAMP.ALLY)
            GM.pushArrow(-1, 0);
    }
    
    public void onClickA()
    {
        if (GM.gameTurn == CAMP.ALLY)
            GM.pushA();
    }

    public void onClickB()
    {
        if (GM.gameTurn == CAMP.ALLY)
            GM.pushB();
    }


    public void onClickStart()
    {

        Application.LoadLevel("Main"); // Reset
    }
}
