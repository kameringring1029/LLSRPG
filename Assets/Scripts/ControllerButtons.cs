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
    EditMapMgr EM;


    float vertical, horizonal;

    // Use this for initialization
    void Start ()
    {
        WM = GameObject.Find("Main Camera").GetComponent<WholeMgr>();
        GM = GameObject.Find("Main Camera").GetComponent<GameMgr>();
        RM = GameObject.Find("Main Camera").GetComponent<RoomMgr>();
        EM = GameObject.Find("Main Camera").GetComponent<EditMapMgr>();
    }


    // 未使用　どうしよう
    /*
    private MonoBehaviour getNowManager()
    {
        switch (WM.wholemode)
        {
            case WHOLEMODE.SELECTMODE:
                return WM;
            case WHOLEMODE.SELECTUNIT:
                return WM.unitSelect;
            case WHOLEMODE.GAME:
                if(GameObject.Find("Main Camera").GetComponent<GameMgr>().gameTurn == CAMP.ALLY)
                {
                    return GameObject.Find("Main Camera").GetComponent<GameMgr>();
                }
                else
                {
                    return null;

                }
            case WHOLEMODE.ROOM:
                return GameObject.Find("Main Camera").GetComponent<RoomMgr>();

        }

        return null;
    }
    */


    public void onClickUp()
    {

        if (WM.wholemode == WHOLEMODE.ROOM)
            RM.pushArrow(0, -1);
        else if (WM.wholemode == WHOLEMODE.GAME && GM.gameTurn == CAMP.ALLY)
            GM.pushArrow(0, -1);
        else if (WM.wholemode == WHOLEMODE.SELECTMODE)
            WM.pushArrow(0, -1);
        else if (WM.wholemode == WHOLEMODE.SELECTUNIT)
            WM.unitSelect.pushArrow(0, -1);
        else if (WM.wholemode == WHOLEMODE.OTHER)
            EM.pushArrow(0, -1);

    }

    public void onClickDown()
    {
        if (WM.wholemode == WHOLEMODE.ROOM)
            RM.pushArrow(0, 1);
        else if (WM.wholemode == WHOLEMODE.GAME && GM.gameTurn == CAMP.ALLY)
            GM.pushArrow(0, 1);
        else if(WM.wholemode == WHOLEMODE.SELECTMODE)
            WM.pushArrow(0, 1);
        else if(WM.wholemode == WHOLEMODE.SELECTUNIT)
            WM.unitSelect.pushArrow(0, 1);
        else if (WM.wholemode == WHOLEMODE.OTHER)
            EM.pushArrow(0, 1);
    }

    public void onClickRight()
    {
        if (WM.wholemode == WHOLEMODE.ROOM)
            RM.pushArrow(1, 0);
        else if (WM.wholemode == WHOLEMODE.GAME && GM.gameTurn == CAMP.ALLY)
            GM.pushArrow(1, 0);
        else if(WM.wholemode == WHOLEMODE.SELECTMODE)
            WM.pushArrow(1, 0);
        else if(WM.wholemode == WHOLEMODE.SELECTUNIT)
            WM.unitSelect.pushArrow(1, 0);
        else if (WM.wholemode == WHOLEMODE.OTHER)
            EM.pushArrow(1, 0);
    }

    public void onClickLeft()
    {
        if (WM.wholemode == WHOLEMODE.ROOM)
            RM.pushArrow(-1, 0);
        else if (WM.wholemode == WHOLEMODE.GAME && GM.gameTurn == CAMP.ALLY)
            GM.pushArrow(-1, 0);
        else if(WM.wholemode == WHOLEMODE.SELECTMODE)
            WM.pushArrow(-1, 0);
        else if(WM.wholemode == WHOLEMODE.SELECTUNIT)
            WM.unitSelect.pushArrow(-1, 0);
        else if (WM.wholemode == WHOLEMODE.OTHER)
            EM.pushArrow(-1, 0);
    }
    
    public void onClickA()
    {
        if (WM.wholemode == WHOLEMODE.ROOM)
            RM.pushA();
        else if (WM.wholemode == WHOLEMODE.GAME && GM.gameTurn == CAMP.ALLY)
            GM.pushA();
        else if(WM.wholemode == WHOLEMODE.SELECTMODE)
            WM.pushA();
        else if(WM.wholemode == WHOLEMODE.SELECTUNIT)
            WM.unitSelect.pushA();
        else if (WM.wholemode == WHOLEMODE.OTHER)
            EM.pushA();
    }

    public void onClickB()
    {
        if (WM.wholemode == WHOLEMODE.ROOM)
            RM.pushB();
        else if (WM.wholemode == WHOLEMODE.GAME && GM.gameTurn == CAMP.ALLY)
            GM.pushB();
        else if(WM.wholemode == WHOLEMODE.SELECTMODE)
            WM.pushB();
        else if(WM.wholemode == WHOLEMODE.SELECTUNIT)
            WM.unitSelect.pushB();
        else if (WM.wholemode == WHOLEMODE.OTHER)
            EM.pushB();
    }


    public void onClickR()
    {
        if (WM.wholemode == WHOLEMODE.ROOM)
            RM.pushR();
        else if (WM.wholemode == WHOLEMODE.GAME && GM.gameTurn == CAMP.ALLY)
            GM.pushR();
        else if(WM.wholemode == WHOLEMODE.SELECTMODE)
            WM.pushR();
        else if(WM.wholemode == WHOLEMODE.SELECTUNIT)
            WM.unitSelect.pushR();
        else if (WM.wholemode == WHOLEMODE.OTHER)
            EM.pushR();
    }

    public void onClickL()
    {
        if (WM.wholemode == WHOLEMODE.ROOM)
            RM.pushL();
        else if (WM.wholemode == WHOLEMODE.GAME && GM.gameTurn == CAMP.ALLY)
            GM.pushL();
        else if(WM.wholemode == WHOLEMODE.SELECTMODE)
            WM.pushL();
        else if(WM.wholemode == WHOLEMODE.SELECTUNIT)
            WM.unitSelect.pushL();
        else if (WM.wholemode == WHOLEMODE.OTHER)
            EM.pushL();
    }

    public void onClickStart()
    {

        Application.LoadLevel("Main"); // Reset
    }
}
