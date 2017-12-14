using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using General;

/*
 * SRPGゲーム中のコントローラボタンにアタッチされて処理する用のやつ
 */


public class ControllerButtons : MonoBehaviour {

    WholeMgr WM;
    GameMgr GM;
    RoomMgr RM;

    // Use this for initialization
    void Start ()
    {
        WM = GameObject.Find("Main Camera").GetComponent<WholeMgr>();
        GM = GameObject.Find("Main Camera").GetComponent<GameMgr>();
        RM = GameObject.Find("Main Camera").GetComponent<RoomMgr>();
    }
	
	public void onClickUp()
    {
        
        if (RM.enabled == true)
            RM.pushArrow(0, -1);
        else if (GM.enabled == true && GM.gameTurn == CAMP.ALLY)
            GM.pushArrow(0, -1);
        else if(RM.enabled == false && GM.enabled == false)
            WM.pushArrow(0, -1);

    }

    public void onClickDown()
    {
        if (RM.enabled == true)
            RM.pushArrow(0, 1);
        else if (GM.enabled == true && GM.gameTurn == CAMP.ALLY)
            GM.pushArrow(0, 1);
        else if (RM.enabled == false && GM.enabled == false)
            WM.pushArrow(0, 1);
    }

    public void onClickRight()
    {
        if (RM.enabled == true)
            RM.pushArrow(1, 0);
        else if (GM.enabled == true && GM.gameTurn == CAMP.ALLY)
            GM.pushArrow(1, 0);
        else if (RM.enabled == false && GM.enabled == false)
            WM.pushArrow(1, 0);
    }

    public void onClickLeft()
    {
        if (RM.enabled == true)
            RM.pushArrow(-1, 0);
        else if (GM.enabled == true && GM.gameTurn == CAMP.ALLY)
            GM.pushArrow(-1, 0);
        else if (RM.enabled == false && GM.enabled == false)
            WM.pushArrow(-1, 0);
    }
    
    public void onClickA()
    {
        if (RM.enabled == true)
            RM.pushA();
        else if (GM.enabled == true && GM.gameTurn == CAMP.ALLY)
            GM.pushA();
        else if (RM.enabled == false && GM.enabled == false)
            WM.pushA();
    }

    public void onClickB()
    {
        if (RM.enabled == true)
            RM.pushB();
        else if (GM.enabled == true && GM.gameTurn == CAMP.ALLY)
            GM.pushB();
        else if (RM.enabled == false && GM.enabled == false)
            WM.pushB();
    }


    public void onClickR()
    {
        if (RM.enabled == true)
            RM.pushR();
        else if (GM.enabled == true && GM.gameTurn == CAMP.ALLY)
            GM.pushR();
        else if (RM.enabled == false && GM.enabled == false)
            WM.pushR();
    }

    public void onClickL()
    {
        if (RM.enabled == true)
            RM.pushL();
        else if (GM.enabled == true && GM.gameTurn == CAMP.ALLY)
            GM.pushL();
        else if (RM.enabled == false && GM.enabled == false)
            WM.pushL();
    }

    public void onClickStart()
    {

        Application.LoadLevel("Main"); // Reset
    }
}
